using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public TextMeshProUGUI die; // 플레이어가 죽었을 때 표시되는 텍스트
    private PlayerController playerController;
    private float originalMoveSpeed;// 원래의 이동 속도
    private bool isDied = false;// 플레이어가 죽었는지 여부
    private bool isHealing = false;// 회복 중인지 여부
    private bool isDashing = false; // 대시 중인지 여부
    public bool invincible = false; // 무적 상태 여부
    public float invincibleDuration = 2f;// 무적 지속 시간

    Condition health { get { return uiCondition.health; } }
    Condition dash { get { return uiCondition.dash; } }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on this object!");
        }
        originalMoveSpeed = playerController.moveSpeed; // 원래의 이동 속도 저장
    }

    private void Update()
    {
        if (health.curValue <= 0f) // 체력이 0 이하이면
        {
            Die(); // 죽음 처리
            isDied = true; // 죽었음 상태 설정
        }

        // 대시 중일 때
        if (isDashing)
        {
            DashOverTime(20f); // 대시 중이면 에너지 차감
        }
        else
        {
            RechargeEnergyOverTime(5f); // 대시 중이 아니면 에너지 회복
        }
    }

    // 회복 아이템을 사용했을 때 일정시간마다 체력을 회복하는 함수
    public void HealOverTime(float totalAmount, float duration)
    {
        if (!isHealing)
        {
            StartCoroutine(HealCoroutine(totalAmount, duration));
        }
    }

    IEnumerator HealCoroutine(float totalAmount, float duration)
    {
        isHealing = true;

        float elapsedTime = 0f;
        float healPerSecond = totalAmount;

        while (elapsedTime < duration)
        {
            health.Add(healPerSecond);
            elapsedTime += 1f;
            yield return new WaitForSeconds(1f); // 1초마다 회복
        }

        isHealing = false;
    }

    // 대시 시작
    public void DashStart()
    {
        isDashing = true;
        playerController.moveSpeed = originalMoveSpeed + 5f;
    }

    // 대시 종료
    public void DashStop()
    {
        isDashing = false;
        playerController.moveSpeed = originalMoveSpeed;
    }

    // 대시 중에 에너지를 차감하는 함수
    public void DashOverTime(float amount)
    {
        if (playerController != null && dash.curValue > 0)
        {
            dash.Subtract(amount * Time.deltaTime);
        }
        else
        {
            DashStop();
        }
    }

    // 일정시간마다 에너지를 회복하는 함수
    public void RechargeEnergyOverTime(float amount)
    {
        if (dash.curValue < dash.maxValue) 
        {
            dash.Add(amount * Time.deltaTime);  
        }
    }

    // 플레이어가 죽었을 때 호출되는 함수
    public void Die()
    {
        die.gameObject.SetActive(true);
        Time.timeScale = 0f;

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ToggleCursor(true);
        }
    }

    // 피해를 입을 때 호출되는 함수
    public void TakeDamage(int damageAmount)
    {
        if (invincible) return;
        health.Subtract(damageAmount);
        StartInvincible();
    }

    // 무적 상태 시작
    public void StartInvincible()
    {
        if (invincible) return; 

        invincible = true;
        StartCoroutine(InvincibleTimer());
    }
    IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(invincibleDuration);
        invincible = false; 
    }

}

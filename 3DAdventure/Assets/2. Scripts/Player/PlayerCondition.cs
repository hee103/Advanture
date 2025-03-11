using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public TextMeshProUGUI die;
    private PlayerController playerController;
    private float originalMoveSpeed;
    private bool isDied = false;
    private bool isHealing = false;
    private bool isDashing = false;
    public bool invincible = false; 
    public float invincibleDuration = 2f;

    Condition health { get { return uiCondition.health; } }
    Condition dash { get { return uiCondition.dash; } }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on this object!");
        }
        originalMoveSpeed = playerController.moveSpeed;
    }

    private void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
            isDied = true;
        }
        if (isDashing)
        {
            DashOverTime(20f);  
        }
        else
        {
            RechargeEnergyOverTime(5f); 
        }
    }

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

    public void DashStart()
    {
        isDashing = true;
        playerController.moveSpeed = originalMoveSpeed + 5f;
    }

    public void DashStop()
    {
        isDashing = false;
        playerController.moveSpeed = originalMoveSpeed;
    }

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


    public void RechargeEnergyOverTime(float amount)
    {
        if (dash.curValue < dash.maxValue) 
        {
            dash.Add(amount * Time.deltaTime);  
        }
    }

    public void Die()
    {
        //Animator animator = GetComponent<Animator>();

        //if (animator != null)
        //{
        //    animator.SetTrigger("DieTrigger"); // 트리거로 Die 애니메이션 실행
        //}
        //else
        //{
        //    Debug.LogWarning("Animator component not found on this object.");
        //}

        die.gameObject.SetActive(true);
        Time.timeScale = 0f;

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ToggleCursor(true);
        }
    }


    public void TakeDamage(int damageAmount)
    {
        if (invincible) return;
        health.Subtract(damageAmount);
        StartInvincible();
    }

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

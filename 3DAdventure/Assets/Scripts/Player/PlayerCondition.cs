using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public TextMeshProUGUI die;
    private bool isDied = false;
    private bool isHealing = false; // 중복 실행 방지
    Condition health { get { return uiCondition.health; } }

    private void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
            isDied = true;
        }
    }

    // 기존 단일 힐 함수 → 지속 힐 기능으로 변경
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

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ToggleCursor(true);
        }
    }




    public void TakeDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
    }
}

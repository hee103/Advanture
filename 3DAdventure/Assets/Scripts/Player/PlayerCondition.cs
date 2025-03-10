using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private bool isHealing = false; // 중복 실행 방지

    Condition health { get { return uiCondition.health; } }

    private void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
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
        float healPerSecond = totalAmount / duration;

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
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakeDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
    }
}

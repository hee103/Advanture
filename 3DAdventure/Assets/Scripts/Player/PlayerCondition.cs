using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private bool isHealing = false; // �ߺ� ���� ����

    Condition health { get { return uiCondition.health; } }

    private void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    // ���� ���� �� �Լ� �� ���� �� ������� ����
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
            yield return new WaitForSeconds(1f); // 1�ʸ��� ȸ��
        }

        isHealing = false;
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    public void TakeDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
    }
}

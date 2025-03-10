using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public TextMeshProUGUI die;
    private bool isDied = false;
    private bool isHealing = false; // �ߺ� ���� ����
    Condition health { get { return uiCondition.health; } }

    private void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
            isDied = true;
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
        float healPerSecond = totalAmount;

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
        //Animator animator = GetComponent<Animator>();

        //if (animator != null)
        //{
        //    animator.SetTrigger("DieTrigger"); // Ʈ���ŷ� Die �ִϸ��̼� ����
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

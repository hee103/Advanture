using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }


    private void Update()
    {

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");

    }

    public void TakeDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        //onTakeDamage?.Invoke();
    }
}
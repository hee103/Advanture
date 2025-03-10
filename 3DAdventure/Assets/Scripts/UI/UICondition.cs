using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition dash;
    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}

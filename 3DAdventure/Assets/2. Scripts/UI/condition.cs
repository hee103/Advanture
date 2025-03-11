using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; // ���� ��
    public float maxValue; // �ִ� ��
    public float startValue; // ���� ��
    public Image uiBar;
    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    // �־��� �縸ŭ ���� ������Ű�� �ִ밪�� ���� �ʵ��� ����
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // �־��� �縸ŭ ���� ���ҽ�Ű�� �ּҰ��� 0���� ����
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // ���� ���� �ִ밪���� ������, UI ���� ä������ ������ ���
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
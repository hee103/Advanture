using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; // 현재 값
    public float maxValue; // 최대 값
    public float startValue; // 시작 값
    public Image uiBar;
    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    // 주어진 양만큼 값을 증가시키고 최대값을 넘지 않도록 제한
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // 주어진 양만큼 값을 감소시키고 최소값을 0으로 제한
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // 현재 값을 최대값으로 나누어, UI 바의 채워지는 비율을 계산
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
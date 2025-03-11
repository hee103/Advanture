using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller; // 플레이어의 이동 및 회전 등 제어하는 컴포넌트
    public PlayerCondition condition; // 플레이어의 상태를 관리하는 컴포넌트
    public ItemData itemData; // 플레이어가 보유한 아이템 데이터
    public Action addItem; // 아이템을 추가하는 액션

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
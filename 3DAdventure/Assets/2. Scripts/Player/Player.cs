using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller; // �÷��̾��� �̵� �� ȸ�� �� �����ϴ� ������Ʈ
    public PlayerCondition condition; // �÷��̾��� ���¸� �����ϴ� ������Ʈ
    public ItemData itemData; // �÷��̾ ������ ������ ������
    public Action addItem; // �������� �߰��ϴ� �׼�

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
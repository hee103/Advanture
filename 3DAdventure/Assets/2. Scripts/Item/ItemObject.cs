using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnUse();
}


public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    // ������ ��� �� ȣ��Ǵ� �޼���
    public void OnUse()
    {
        CharacterManager.Instance.Player.itemData = data;

        // �Һ� ���������� Ȯ��
        if (data.type == ItemType.Consumable)
        {
            foreach (var consumable in data.consumables)
            {
                if (consumable.Type == ConsumableType.Health) // ü�� ȸ�� �������̸�
                {
                    CharacterManager.Instance.Player.condition.HealOverTime(consumable.value, consumable.duration);
                    break; // ù ��° �ش� Ÿ���� �������� ����ϸ� ����
                }
            }
        }
        // ��� �� ������ ������Ʈ�� �ı�
        Destroy(gameObject);
    }

}

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

    // 아이템 사용 시 호출되는 메서드
    public void OnUse()
    {
        CharacterManager.Instance.Player.itemData = data;

        // 소비 아이템인지 확인
        if (data.type == ItemType.Consumable)
        {
            foreach (var consumable in data.consumables)
            {
                if (consumable.Type == ConsumableType.Health) // 체력 회복 아이템이면
                {
                    CharacterManager.Instance.Player.condition.HealOverTime(consumable.value, consumable.duration);
                    break; // 첫 번째 해당 타입의 아이템을 사용하면 종료
                }
            }
        }
        // 사용 후 아이템 오브젝트를 파괴
        Destroy(gameObject);
    }

}

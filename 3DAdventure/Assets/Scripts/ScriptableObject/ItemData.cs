using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Consumable,
    Resource
}
public enum ConsumableType
{
    Health,
    Hunger
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType Type;
    public float value;
    public float duration;
}
[CreateAssetMenu(fileName ="Itme",menuName ="New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;


    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}

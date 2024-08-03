using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct InventoryItem
{
    public Sprite itemImage;
    public string name; 
    public string description;
}

[CreateAssetMenu(fileName = "InventoryList", menuName = "Inventory/InventoryList", order = 1)]
public class InventoryList : ScriptableObject
{
    public InventoryItem[] items;  
}

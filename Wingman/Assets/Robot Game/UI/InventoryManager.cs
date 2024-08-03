using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    List<InventoryItem> inventoryItems;
    [SerializeField] InventoryList inventoryList;
    [SerializeField] GameObject inventoryUIContent;
    [SerializeField] GameObject inventoryitemPrefab;

    private void OnEnable()
    {
        InventoryObject.AddToInventory += AddToInventory;
    }

    private void AddToInventory(string name)
    {
        foreach (InventoryItem item in inventoryList.items)
        {
            if (item.name == name)
            {
                inventoryItems.Add(item);
                return;
            }
        }
    }

    private void AddToInventoryUI(InventoryItem item)
    {
        GameObject button = Instantiate(inventoryitemPrefab);
        button.GetComponent<Image>().sprite = item.itemImage;
        button.GetComponent<InventoryButton>().inventoryItem = item;
        button.transform.parent = inventoryUIContent.transform;

    }
}

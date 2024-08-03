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
        inventoryItems = new();
        InventoryObject.AddToInventory += AddToInventory;
        AddToInventory("eggs");
    }

    private void AddToInventory(string name)
    {
        foreach (InventoryItem item in inventoryList.items)
        {
            if (item.name == name)
            {
                inventoryItems.Add(item);
                AddToInventoryUI(item);
                return;
            }
        }
        //for stacking, if already in the list, add value to list with 2d array saying the item and then the number of them owned.
    }

    private void AddToInventoryUI(InventoryItem item)
    {
        GameObject button = Instantiate(inventoryitemPrefab);
        button.GetComponent<Image>().sprite = item.itemImage;
        button.GetComponent<InventoryButton>().inventoryItem = item;
        button.transform.parent = inventoryUIContent.transform;

    }
}

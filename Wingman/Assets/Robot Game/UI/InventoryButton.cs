using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public GameObject descriptionBox;

    public void InventoryButtonClicked()
    {
        descriptionBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inventoryItem.name;
        descriptionBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventoryItem.description;
    }
}

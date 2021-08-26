using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private ItemPickup inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemPickup>();
    }

    public void PickUpItem()
    {
        for (int i= 0; i < inventory.inventorySlot.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                //Item CAN BE ADDED TO INVENTORY !
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.inventorySlot[i].transform, false);
                Destroy(gameObject);
                break;
            }
                
        }
    }
        
}


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
                itemButton = Instantiate(itemButton, inventory.inventorySlot[i].transform, false);
                itemButton.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                transform.localPosition = Vector2.zero;
                break;
            }
        }
    }

    public void DropItem()
    {
        for (int i = 0; i < inventory.inventorySlot.Length; i++)
        {
            if (inventory.isFull[i] == true)
            {
                //Item CAN BE ADDED TO INVENTORY !
                inventory.isFull[i] = false;
                itemButton.SetActive(false);
                Destroy(gameObject);
                break;
            }
        }
    }        
}
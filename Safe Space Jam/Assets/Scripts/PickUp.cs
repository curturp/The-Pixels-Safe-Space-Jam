using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private ItemPickup inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemPickup>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"));
            for (int i= 0; i < inventory.InventorySlot.Length; i++)
        {
            if (inventory.isFull[i] == false)
                //Item CAN BE ADDED TO INVENTORY !
               inventory.isFull[i] = true;
            break;
        }


}
        
}


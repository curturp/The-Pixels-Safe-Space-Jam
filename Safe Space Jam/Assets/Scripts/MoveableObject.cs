using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private Transform player;

    public AudioManager audioManager;

    [Range(0f, 10f)]
    public float radius = 1f;
    [Range(0f, 10f)]
    public float playerRadius = 1f;

    private void Update()
    {
        PlayerInRange();
    }

    public void PlayerInRange()
    {
        // Calculates Distance Between This Object and the Player
        Vector2 playerPos = player.position;
        Vector2 origin = transform.position;
        float dist = Vector2.Distance(playerPos, origin);
        // Is the player in range?
        bool inRange = dist < radius + playerRadius;

        //Universal Interaction Key
        bool interact = Input.GetKeyDown(KeyCode.E);

        // If player is in Range and Interacts -- DO SOMETHING
        if (inRange && interact)
        {
            ChangeColor();
            audioManager.Play("Interact");
        }

        //Draw order for objects checked agaisnt player location
        SpriteRenderer objSprite = GetComponent<SpriteRenderer>();       

        if (origin.y >= playerPos.y)
        {
            objSprite.sortingOrder = -1;
        }
        else objSprite.sortingOrder = 1;       
    }

    private void ChangeColor()
    {
        SpriteRenderer objSprite = GetComponent<SpriteRenderer>();
        objSprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }


#if UNITY_EDITOR
    //Represents the distance detection system in the Editor
    private void OnDrawGizmos()
    {
        Vector2 playerPos = player.position;
        Vector2 origin = transform.position;

        float dist = Vector2.Distance(playerPos, origin);
        bool inRange = dist < radius + playerRadius;

        Gizmos.color = inRange ? Color.green : Color.red;

        Gizmos.DrawWireSphere(Vector2.zero + origin, radius);

        Gizmos.DrawWireSphere(Vector2.zero + playerPos, playerRadius);
    }
#endif
}

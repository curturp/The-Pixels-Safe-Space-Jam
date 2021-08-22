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

    public void PlayerInRange()
    {
        bool interact = Input.GetKeyDown(KeyCode.E);
        SpriteRenderer objSprite = GetComponent<SpriteRenderer>();

        Vector2 playerPos = player.position;
        Vector2 origin = transform.position;

        float dist = Vector2.Distance(playerPos, origin);
        bool inRange = dist < radius + playerRadius;

        if (origin.y > playerPos.y)
        {
            objSprite.sortingOrder = -1;
        }
        else objSprite.sortingOrder = 1;

        if (inRange && interact)
        {
            ChangeColor();
            audioManager.Play("Interact");
        }            
    }

    private void ChangeColor()
    {
        SpriteRenderer objSprite = GetComponent<SpriteRenderer>();
        objSprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}

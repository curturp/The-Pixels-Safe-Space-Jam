using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField]private GameObject player;
    private PlayerBehavior playerBehavior;
    private AudioManager audioManager;
    private DialogueTrigger dialogueTrigger;
    private PickUp pickUp;

    [Range(0f, 10f)]
    public float radius = 1f;
    private float playerRadius = 2.5f;

    public bool moveableObject;
    bool isHolding = false;
    public bool hasDialogue;
    public bool canChangeColor;
    public bool canPickUp;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        pickUp = GetComponent<PickUp>();
        playerBehavior = player.GetComponent<PlayerBehavior>();
    }

    private void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 origin = transform.position;
        bool interact = Input.GetKeyDown(KeyCode.E);
        bool inRange = PlayerInRange(origin, playerPos);

        Interact(inRange, interact);
        ObjectLayer(origin, playerPos);
    }

    public bool PlayerInRange(Vector2 obj, Vector2 playerPos)
    {
        // Calculates Distance Between This Object and the Player
        float dist = Vector2.Distance(obj, playerPos);
        // Is the player in range?
        bool inRange = dist < radius + playerRadius;

        if (inRange)
            playerBehavior.usePromptSignal = true;

        if (hasDialogue && !inRange)
            dialogueTrigger.EndDialogue();

        return inRange;
    }

    public void Interact(bool inRange, bool interact)
    {
        // If player is in Range and Interacts -- DO SOMETHING
        if (inRange && interact)
        {
            if (canChangeColor)
            {
                ChangeColor();
            }   
            if (hasDialogue)
            {                
                if (dialogueTrigger.dialogueManager.animator.GetBool("IsOpen") == true)
                {
                    dialogueTrigger.ContinueDialogue();
                }
                else dialogueTrigger.TriggerDialogue();
            }
            if (moveableObject)
            {
                isHolding = PushPull(isHolding);
            }
            if (canPickUp)
            {
                this.pickUp.PickUpItem();
            }
        } 
    }

    private bool PushPull(bool isHolding)
    {        
        if (isHolding == false)
        {
            isHolding = true;
            transform.parent = player.transform;
            playerBehavior.playerSpeed /= 2;
        }
        else
        {
            isHolding = false;
            transform.parent = null;
            playerBehavior.playerSpeed *= 2;
        }
        Debug.Log("isHolding is " + isHolding);
        return isHolding;
    }

    private void ObjectLayer(Vector2 obj, Vector2 player)
    {
        SpriteRenderer objSprite = GetComponent<SpriteRenderer>();

        // If player is above the obj, draw obj on top, if player is below the obj, draw obj on bottom
        if (obj.y >= player.y)
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
        Vector2 playerPos = player.transform.position;
        Vector2 origin = transform.position;

        float dist = Vector2.Distance(playerPos, origin);
        bool inRange = dist < radius + playerRadius;

        Gizmos.color = inRange ? Color.green : Color.red;

        Gizmos.DrawWireSphere(Vector2.zero + origin, radius);

        Gizmos.DrawWireSphere(Vector2.zero + playerPos, playerRadius);
    }
#endif
}

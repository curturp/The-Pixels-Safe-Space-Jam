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

        this.Interact(inRange, interact);
        ObjectLayer(origin, playerPos);
    }

    public bool PlayerInRange(Vector2 obj, Vector2 playerPos)
    {
        // Calculates Distance Between This Object and the Player
        float dist = Vector2.Distance(obj, playerPos);
        // Is the player in range?
        bool inRange = dist < radius + playerRadius;

        if (inRange && (moveableObject || hasDialogue || canPickUp))
            playerBehavior.usePromptSignal = true;

        return inRange;
    }

    public void Interact(bool inRange, bool interact)
    {
        // If player is in Range and Interacts -- DO SOMETHING
        if (inRange && interact)
        {  
            if (hasDialogue)
            {
                if (this.tag == "Red" && playerBehavior.hasLight == true)
                {
                    NPCDialogue redDialogue = GetComponent<NPCDialogue>();

                    if (redDialogue.dialogueTriggers[2].dialogueManager.animator.GetBool("IsOpen") == true)
                    {
                        redDialogue.RecieveLight();
                    }
                    else
                    {
                        player.GetComponentInChildren<PickUp>().DropItem();
                        redDialogue.RecieveLight();
                    }                    
                }
                else if (this.tag == "Red" && playerBehavior.hasLight == false)
                {
                    NPCDialogue redDialogue = GetComponent<NPCDialogue>();
                    redDialogue.RedDialouge();
                }
                else if (this.dialogueTrigger.dialogueManager.animator.GetBool("IsOpen") == true)
                {
                    dialogueTrigger.ContinueDialogue();
                }
                else dialogueTrigger.TriggerDialogue();
            }
            else if (moveableObject)
            {
                isHolding = PushPull(isHolding);
            }
            else if (canPickUp)
            {
                this.pickUp.PickUpItem();
                this.canPickUp = false;
                playerBehavior.hasLight = true;
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

    private void ObjectLayer(Vector2 obj, Vector2 playerPos)
    {
        
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        float distance = Vector2.Distance(obj, playerPos);

        // If player is above the obj, draw obj on top, if player is below the obj, draw obj on bottom
        if (distance <= 5)
        {
            if (obj.y >= playerPos.y)
            {

                playerSprite.sortingOrder = 3;
            }
            else
            {
                playerSprite.sortingOrder = 1;
            }
        }        
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
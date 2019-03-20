using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//RENAME TO SkillHandler WHEN FINALIZED
public class DraggableSkill : MonoBehaviour
{
    //Might need these to set skills
    private PlayerController YellowPlayer, GreenPlayer;
    private GameObject newParent, skillContainer, skillSlotHUDContainer;
    private int childIndexNumber;
    private bool selected = false, onPlayer, onSkillContainer, isTransferable;
    private Vector2 lastKnownLocation, playerPos;
    private LayerMask skillLayerMask;
    private Vector3 cursorPos;
    private Component attachedSkill;
    //[SerializeField]
    //[Tooltip("This is meant to hold the transform components of the SkillContainerHUDTarget object")]
    //private Transform[] skillSlotPositions;
    //**Skill Script Declaration template
    //private Component skillScript;
    //use a switch-case in the start method based on the name of this GameObject to get the correct SkillScript
    /*Example:
        switch(gameObject.name)
        {
            case "DoubleJump":
                {
                    skillScript = GetComponent<DoubleJumpSkill>();
                }
                break;
            default:
                break;
        }
    */
    public Component AttachedSkill
    {
        get { return attachedSkill; }
        set
        {
            attachedSkill = value;
        }
    }
    private Vector2 LastKnownLocation
    {
        get
        {
            return lastKnownLocation;
        }
        set => lastKnownLocation = value;
    }
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = true;
        skillLayerMask = LayerMask.GetMask("Skill");
        skillContainer = GameObject.Find("SkillContainer");
        skillSlotHUDContainer = GameObject.Find("SkillContainerHUDTarget");
        DetermineSkillComponent();

        /*May Not Need This
        for (int i = 0; i < skillSlotHUDContainer.transform.childCount; i++)
        {

        }
        */
    }

    private void DetermineSkillComponent()
    {
        //If the search for the specific component does not return null, Then activate the skill's method
        
        if (GetComponent<SkillConnectionTest>())
        {
            //AttachedSkill = GetComponent<SkillConnectionTest>();
            //TODO: Debug Log
            Debug.Log("Connection was successful. This Object now has an attached skill called " + AttachedSkill);
        }
        else if (GetComponent<DoubleJumpSkill>())
        {
            AttachedSkill = gameObject.GetComponent<DoubleJumpSkill>();
            //TODO: Debug Log
            Debug.Log("Connection was successful. This Object now has an attached skill called " + AttachedSkill);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (selected == true)
        {
            //TODO: Debug Log
            Debug.Log("Skill Selected");
            TrackCursor();
        }
        else if (selected == true && onPlayer == true)
        {
            //TODO: Debug Log
            Debug.Log("Skill Selected & On Player");
            TrackCursor();
        }
        else if (onPlayer == true && selected == false)
        {
            transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 1f);
        }
        else if (onPlayer == false && selected == false && transform.parent.name == "SkillContainer")
        {
            ReturnToSkillContainer();
        }
    }


    private void ReturnToSkillContainer()
    {
        transform.SetParent(skillContainer.transform);
        childIndexNumber = transform.GetSiblingIndex();
        //transform.position = skillSlotHUDContainer.transform.GetChild(childIndexNumber).GetComponent<Transform>().position;
        //TODO: Debug Log
        Debug.Log(name + " has a childIndex of " + childIndexNumber);

        if (childIndexNumber == 0)
        {
            transform.position = GameObject.Find("SkillSlot_0").GetComponent<Transform>().position;
        }
        else if (childIndexNumber == 1)
        {
            transform.position = GameObject.Find("SkillSlot_1").GetComponent<Transform>().position;
        }
        else if (childIndexNumber == 2)
        {
            transform.position = GameObject.Find("SkillSlot_2").GetComponent<Transform>().position;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
            isTransferable = true;

            Vector2 clickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero, skillLayerMask);
            
            foreach (RaycastHit2D obj in hit)
            {
                if (obj.collider.tag == "Skill")
                {
                    selected = true;
                    isTransferable = true;
                    transform.SetParent(null);
                }
            }
        }
    }

    private void OnMouseUp()
    {
        selected = false;
        isTransferable = false;
        if (Input.GetMouseButtonUp(0) && onPlayer == false)
        {
            ReturnToSkillContainer();
        }
        else if (Input.GetMouseButtonUp(0) && onPlayer == true)
        {
            transform.SetParent(newParent.transform);

            //TODO: Debug Log
            if(AttachedSkill)
            {
                Debug.Log("The AttachedSkill of " + gameObject.name + " is " + AttachedSkill.name);
                GetComponentInParent<PlayerController>().CheckCurrentSkills(AttachedSkill);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Not Checking for the MouseButtonDown(0) will cause the skill to disappear when the object's current parent collides with another
        if(collision.tag == "Player" && Input.GetMouseButtonDown(0))
        {
            onPlayer = false;
        }
        else if (collision.tag == "Player" && Input.GetMouseButtonUp(0))
        {
            onPlayer = false;
            ReturnToSkillContainer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetMouseButtonUp(0))
        {
            //TODO: Debug.Log
            Debug.Log(collision.name + " PlayerCollision Detected");
            onPlayer = true;
            newParent = collision.gameObject;
            playerPos = collision.transform.position;
        }
        else if (collision.gameObject.name == "SkillContainer" && Input.GetMouseButtonDown(0) == false)
        {
            //TODO: Debug.Log
            Debug.Log("SkillContainer Collision Detected");
            onPlayer = false;
            newParent = collision.gameObject;
            playerPos = collision.transform.position;
        }
    }

    private void TrackCursor()
    {
        cursorPos = Input.mousePosition;
        cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);
        this.gameObject.transform.localPosition = new Vector3(cursorPos.x, cursorPos.y, 0);
    }
}

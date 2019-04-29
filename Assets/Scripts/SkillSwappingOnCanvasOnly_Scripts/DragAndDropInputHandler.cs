using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Designed to be attached to the InputManager GameObject in the scene and handles
/// the Drag and Drop functionality
/// </summary>

public class DragAndDropInputHandler : MonoBehaviour
{
    private bool mouseButtonDownInput;
    private bool mouseButtonUpInput;
    private bool isDragging = false;

    private GameObject skillToDrag;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEvent;
    private EventSystem eventSystem;
    private SkillContainerSkillSlotPlacementHandler skillPlacementHandler;

    private GameObject[] skillBank;

    [SerializeField]
    [Tooltip("Used to adjust the X position of the draggable object")]
    private float xOffset, yOffset;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = true;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        skillPlacementHandler = transform.GetChild(0).GetComponent<SkillContainerSkillSlotPlacementHandler>();
        SetSkillsToHomePosition();
    }
    private void SetSkillsToHomePosition()
    {
        skillBank = GameObject.FindGameObjectsWithTag("Skill");
        
        for (int i = 0; i < skillBank.Length; i++)
        {
            skillBank[i].transform.position = 
                new Vector2(skillPlacementHandler.transform.GetChild(i).position.x + xOffset, skillPlacementHandler.transform.GetChild(i).position.y + yOffset);

        }
    }

    // Update is called once per frame
    private void Update()
    {
        ListenForClickInput();
    }
    
    private void ListenForClickInput()
    {
        mouseButtonDownInput = Input.GetButtonDown("LeftMouseButton");
        mouseButtonUpInput = Input.GetButtonUp("LeftMouseButton");

        if (mouseButtonDownInput)
        {
            Debug.Log("Click!");
            skillToDrag = GetDragTargetObject();
        }
        else if (mouseButtonUpInput)
        {
            StopDraggingSkill();
        }
    }

    private void StopDraggingSkill()
    {
        switch (skillToDrag.name)
        {
            case "Skill_DoubleJump":
                skillToDrag.GetComponent<Skill_DoubleJump>().IsBeingDragged = false;
                Debug.Log("Correct Skill Name Determined");
                break;

                //ADD NEW SKILLS HERE

            default:
                break;
        }
    }

    private GameObject GetDragTargetObject()
    {
        //Used to listen to Pointer based Events
        pointerEvent = new PointerEventData(EventSystem.current);
        pointerEvent.position = Input.mousePosition;

        //Create a list to store the objects detected by the GraphicRaycaster
        List<RaycastResult> detectedRaycastObjects = new List<RaycastResult>();

        //Fire to "RAY" "CAST"
        graphicRaycaster.Raycast(pointerEvent, detectedRaycastObjects);

        //Used to hold the target of the Raycast
        GameObject dragTarget = null;

        if (detectedRaycastObjects != null)
        {
            foreach (RaycastResult graphicObject in detectedRaycastObjects)
            {
                Debug.Log($"{graphicObject.gameObject.name} was detected by the graphic raycast");

                if (graphicObject.gameObject.tag == "Skill")
                {
                    dragTarget = graphicObject.gameObject;
                    dragTarget.GetComponent<Skill_DoubleJump>().IsBeingDragged = true;
                }
                else
                {
                    Debug.Log("No Matching Tags Found");
                }
            }
        }
        else
        {
            Debug.Log("NO OBJECTS DETECTED OR RAYCAST HAS FAILED");
        }

        if(dragTarget != null)
        {
            return dragTarget;
        }
        else
        {
            return null;
        }
    }
}

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

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = true;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        /* 
         * TODO:    ------HEY!! FUTURE MEE!------
         * This is where you left off. You were gonna start to implement
         * the draggable functionailty by starting with the click hander
         * and then the drag handler. However, you weren't sure if you should
         * do that by making it a function of the button component of the skill
         * by using a public function somewhere in this script OR by seeing where
         * the script in your browser takes you.
         * -- Good Luck brohannah :D
         */

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
            //isDragging = true;
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

            default:
                break;
        }
    }

    private GameObject GetDragTargetObject()
    {
        pointerEvent = new PointerEventData(EventSystem.current);
        pointerEvent.position = Input.mousePosition;

        List<RaycastResult> detectedRaycastObjects = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEvent, detectedRaycastObjects);

        GameObject dragTarget = null;

        if (detectedRaycastObjects == null)
        {
            Debug.Log("NO OBJECTS DETECTED OR RAYCAST HAS FAILED");
        }
        else
        {
            Debug.Log("Raycast Detected Objects");

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

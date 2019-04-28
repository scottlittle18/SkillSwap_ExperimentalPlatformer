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
            SelectObjectOnClick();
        }
        else if (mouseButtonUpInput)
        {
            //Release Cursor's Hold on Object
        }
    }

    private void SelectObjectOnClick()
    {
        Debug.Log("SelectObjectOnClick() Entered!");
        pointerEvent = new PointerEventData(EventSystem.current);
        pointerEvent.position = Input.mousePosition;

        List<RaycastResult> detectedRaycastObjects = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEvent, detectedRaycastObjects);

        if (detectedRaycastObjects == null)
        {
            Debug.Log("NO OBJECTS DETECTED OR RAYCAST HAS FAILED");
        }
        else if (detectedRaycastObjects != null)
        {
            Debug.Log("Raycast Detected Objects");

            foreach (RaycastResult graphicObject in detectedRaycastObjects)
            {
                Debug.Log($"{graphicObject.gameObject.name} was detected by the graphic raycast");
            }
        }
    }
}

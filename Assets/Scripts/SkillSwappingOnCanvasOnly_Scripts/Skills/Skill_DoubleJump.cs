using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DoubleJump : MonoBehaviour, IDraggable
{
    private bool isBeingDragged = false;

    public bool IsBeingDragged
    {
        get { return isBeingDragged; }
        set { isBeingDragged = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsBeingDragged)
        {
            StartBeingDraggedByCursor();
        }
    }

    #region IDraggable & IDroppable Interfaces
    public void StartBeingDraggedByCursor()
    {
        //Make the Target of the Drag Action follow the Cursor
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
    #endregion
}

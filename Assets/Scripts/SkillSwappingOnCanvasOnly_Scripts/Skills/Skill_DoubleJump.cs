using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DoubleJump : MonoBehaviour, IDraggable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region IDraggable & IDroppable Interfaces
    public void StartBeingDraggedByCursor()
    {
        //Make the Target of the Drag Action follow the Cursor
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
    #endregion
}

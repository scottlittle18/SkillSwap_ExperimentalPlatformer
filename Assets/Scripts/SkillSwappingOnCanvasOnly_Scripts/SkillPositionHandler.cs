using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Keeps the skill that this script is attached to in the correct position
/// even while the screen is scrolling/moving
/// </summary>
public class SkillPositionHandler : MonoBehaviour
{
    private Transform followTarget;

    public Transform FollowTarget
    {
        get { return followTarget; }
        set
        {
            followTarget = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //PositionHandler();
    }

    //Made public to allow communication between this and the DragAndDropInputHandler
    public void SetInitialFollowTarget(Transform target)
    {
        FollowTarget = target;
        //transform.position = target.position;
    }

    private void PositionHandler()
    {
        transform.position = FollowTarget.position;
    }
}

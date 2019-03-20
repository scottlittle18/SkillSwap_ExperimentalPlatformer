using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformSkill : MonoBehaviour
{
    //This is for determining the time that the player is jumping, e.g. before or after being suspended
    private bool firstJump;
    private PlayerController playerController;

    public bool FirstJump
    {
        get { return firstJump; }
        set => firstJump = value;
    }

    private void Start()
    {
        if (transform.parent != null && transform.parent.tag != "SkillContainer")
        {
            playerController = GetComponentInParent<PlayerController>();
            //TODO: Test to make sure that this isn't throw-away code
            Debug.Log("Script retrieved from the Parent of this skill is " + playerController);
        }
    }
}

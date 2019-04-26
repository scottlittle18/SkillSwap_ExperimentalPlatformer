using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the positioning of the SkillContainer Object
/// </summary>

public class SkillContainerScreenPlacement : MonoBehaviour
{
    //Game Object that is a child of the camera and will be used to place the Skill Container
    private GameObject hudTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        hudTargetPosition = GameObject.Find("SkillContainerHUDTarget");
        transform.position = new Vector3(hudTargetPosition.transform.position.x, hudTargetPosition.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(hudTargetPosition.transform.position.x, hudTargetPosition.transform.position.y, 0);
    }
}

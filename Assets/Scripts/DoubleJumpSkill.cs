using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpSkill : MonoBehaviour
{
    private bool hasDoubleJumped;
    private PlayerController playerController;

    public bool HasDoubleJumped
    {
        get { return hasDoubleJumped; }
        set => hasDoubleJumped = value;
    }

    private void Start()
    {
        if (transform.parent != null && transform.parent.tag != "SkillContainer")
            playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

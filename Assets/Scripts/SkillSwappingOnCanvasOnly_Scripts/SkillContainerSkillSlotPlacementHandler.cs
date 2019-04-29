using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainerSkillSlotPlacementHandler : MonoBehaviour
{
    private Transform skillSlot1, skillSlot2, skillSlot3;

    public Transform SkillSlot1 { get { return skillSlot1; } private set { skillSlot1 = value; } }
    public Transform SkillSlot2 { get { return skillSlot2; } private set { skillSlot2 = value; } }
    public Transform SkillSlot3 { get { return skillSlot3; } private set { skillSlot3 = value; } }

    // Start is called before the first frame update
    void Start()
    {
        InitializeSkillContainerSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeSkillContainerSlots()
    {
        SkillSlot1 = transform.GetChild(0);
        SkillSlot2 = transform.GetChild(1);
        SkillSlot3 = transform.GetChild(2);
    }
}

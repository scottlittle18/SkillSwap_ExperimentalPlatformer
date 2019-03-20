using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConnectionTest : MonoBehaviour
{
    //TODO: If DraggableSkill script was renamed make sure to update this as well
    private DraggableSkill dragScript;
    
    // Start is called before the first frame update
    void Start()
    {
        dragScript = GetComponent<DraggableSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

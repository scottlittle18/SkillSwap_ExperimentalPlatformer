using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillContainer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the player that this Container belongs to.")]
    private GameObject assignedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(assignedPlayer.transform.localPosition.x, assignedPlayer.transform.localPosition.y + 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

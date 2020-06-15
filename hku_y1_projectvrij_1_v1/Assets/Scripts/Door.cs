using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private ManagerUI managerUI;

    public TriggerNextLevel nextLevel;

    public bool debugKey = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        managerUI = FindObjectOfType<ManagerUI>();

        if(Application.isEditor == false)
        {
            debugKey = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            if(Player.hasKey || debugKey)
            {
                animator.Play("DoorOpen");
                managerUI.BlackScreen(true);
                Player.hasKey = false;
                if(nextLevel != null)
                {
                    nextLevel.EnterNextLevel();
                }
            }
        }
    }
}

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

    private bool vaderAlive = false;
    private bool broerAlive = false;
    private bool zusAlive = false;
    private bool vaderEnterd = false;
    private bool broerEnterd = false;
    private bool zusEnterd = false;

    public GameObject vader;
    public GameObject broer;
    public GameObject zus;

    public TextMesh tm;

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

        vaderEnterd = false;
        broerEnterd = false;
        zusEnterd = false;
        tm.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        vaderAlive = vader.GetComponent<Character>().isAlive;
        broerAlive = broer.GetComponent<Character>().isAlive;
        zusAlive = zus.GetComponent<Character>().isAlive;

        // display
        if(Player.hasKey || debugKey)
        {
            tm.gameObject.SetActive(true);
            int amR = 0;
            if(vaderAlive) amR++;
            if(broerAlive) amR++;
            if(zusAlive) amR++;
            int amL = 0;
            if(vaderEnterd) amL++;
            if(broerEnterd) amL++;
            if(zusEnterd) amL++;

            tm.text = amL.ToString() + "/" + amR.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            if(other.name == vader.name)
            {
                vaderEnterd = true;
            }
            else if(other.name == broer.name)
            {
                broerEnterd = true;
            }
            else if(other.name == zus.name)
            {
                zusEnterd = true;
            }

            if(Player.hasKey || debugKey)
            {
                // Check if all has enterd
                if(vaderAlive && vaderEnterd == false) return;
                if(broerAlive && broerEnterd == false) return;
                if(zusAlive && zusEnterd == false) return;

                // Clear
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

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Character")
        {
            if(other.name == vader.name)
            {
                vaderEnterd = false;
            }
            else if(other.name == broer.name)
            {
                broerEnterd = false;
            }
            else if(other.name == zus.name)
            {
                zusEnterd = false;
            }
        }
    }
}

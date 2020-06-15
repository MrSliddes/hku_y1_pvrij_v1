using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private bool isHidden = false;
    private bool canExit = false;

    private Character characterThatIsHidden;
    private ManagerUI managerUI;

    public Animator animator;

    public AudioSource sqeak;

    // Start is called before the first frame update
    void Start()
    {
        managerUI = FindObjectOfType<ManagerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isHidden)
        {
            if(Input.GetButtonDown("Input E") && canExit)
            {
                if(characterThatIsHidden.isControlled == false) return;

                // come out
                isHidden = false;
                characterThatIsHidden.HideCharacter(false);
                characterThatIsHidden = null;
                animator.Play("verstopKastOpen");
                sqeak.Play();
            }
            else
            {
                canExit = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Character")
        {
            if(other.GetComponent<Character>().isAlive == false || other.GetComponent<Character>().isControlled == false) return;
            managerUI.Pickup(true);
            if(Input.GetButtonDown("Input E"))
            {
                if(!isHidden)
                {
                    // hide
                    animator.Play("verstopKastDicht");
                    canExit = false;
                    print("hide");
                    isHidden = true;
                    characterThatIsHidden = other.GetComponent<Character>();
                    characterThatIsHidden.HideCharacter(true);
                    sqeak.Play();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Character" && !isHidden)
        {
            managerUI.Pickup(false);
        }
    }
}

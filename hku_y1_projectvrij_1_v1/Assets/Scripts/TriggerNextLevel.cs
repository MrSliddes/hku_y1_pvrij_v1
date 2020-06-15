using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextLevel : MonoBehaviour
{
    public bool nextLvl = false;

    public int level = 1;

    //cam
    public Player player;

    // Char
    public GameObject char_0;
    public GameObject char_1;
    public GameObject char_2;

    public Transform spawnPos_0;
    public Transform spawnPos_1;
    public Transform spawnPos_2;

    private ManagerUI managerUI;

    // Start is called before the first frame update
    void Start()
    {
        managerUI = FindObjectOfType<ManagerUI>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterNextLevel()
    {
        char_0.GetComponent<Character>().isControlled = false;
        char_1.GetComponent<Character>().isControlled = false;
        char_2.GetComponent<Character>().isControlled = false;
        if(char_0.GetComponent<Character>().isAlive) char_0.transform.position = spawnPos_0.position;
        if(char_1.GetComponent<Character>().isAlive) char_1.transform.position = spawnPos_1.position;
        if(char_2.GetComponent<Character>().isAlive) char_2.transform.position = spawnPos_2.position;


        if(level == 1)
        {
            player.isLevel0 = false;
        }
        else if(level == 2)
        {
            player.isLevel2 = true;
        }

        managerUI.SelectChar(666);
    }

    private void OnValidate()
    {
        if(nextLvl)
        {
            EnterNextLevel();
            nextLvl = false;
        }
    }
}

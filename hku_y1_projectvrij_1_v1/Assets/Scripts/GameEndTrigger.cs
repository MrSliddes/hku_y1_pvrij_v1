using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTrigger : MonoBehaviour
{
    public GameObject uiChoose;
    public GameObject blackBg;
    public GameObject fade;

    public GameObject ui4Survive;
    public GameObject ui3Survive;
    public GameObject ui2Survice;

    public GameObject vader;
    public GameObject broer;
    public GameObject zus;

    public GameObject headV, headB, headZ;

    private bool aliveVader;
    private bool aliveBroer;
    private bool aliveZus;

    // Start is called before the first frame update
    void Start()
    {
        uiChoose.SetActive(false);
        blackBg.SetActive(false);
        fade.SetActive(false);
        ui4Survive.SetActive(false);
        ui3Survive.SetActive(false);
        ui2Survice.SetActive(false);

        AudioListener.volume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("enter");
        if(other.tag == "Character")
        {
            // Mute sound
            //FindObjectOfType<Player>().gameObject.GetComponent<AudioListener>().
            AudioListener.volume = 0;

            // if all alive + meds = happy end
            // else choose ui screen
            aliveVader = vader.GetComponent<Character>().isAlive;
            aliveBroer = broer.GetComponent<Character>().isAlive;
            aliveZus = zus.GetComponent<Character>().isAlive;

            if(aliveVader && aliveBroer && aliveZus)
            {
                // iedereen overleeft, moeder ook
                ui4Survive.SetActive(true);
                fade.SetActive(true);
                blackBg.SetActive(true);
                StartCoroutine(ReturnMainMenu());
            }
            else
            {                
                uiChoose.SetActive(true);
                headV.SetActive(!aliveVader);
                headB.SetActive(!aliveBroer);
                headZ.SetActive(!aliveZus);
            }
        }
    }

    

    public void ChooseMom()
    {
        int alive = 0;
        if(aliveVader) alive++;
        if(aliveBroer) alive++;
        if(aliveZus) alive++;

        switch(alive)
        {
            case 1:
                ui2Survice.SetActive(true);
                break;
            case 2:
                ui3Survive.SetActive(true);
                break;
            default:
                break;
        }

        fade.SetActive(true);
        blackBg.SetActive(true);
        StartCoroutine(ReturnMainMenu());
    }

    public void ChooseRest()
    {
        ui3Survive.SetActive(true);
        fade.SetActive(true);
        blackBg.SetActive(true);
        StartCoroutine(ReturnMainMenu());
    }

    public IEnumerator ReturnMainMenu()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene(0);
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    // UI
    public GameObject pickUpUI;
    public GameObject plankUI;
    public GameObject keyUI;
    public GameObject trowableUI;
    public TextMeshProUGUI trowableTextUI;

    public Character currentCharacter;

    // character ui
    public GameObject selectVader;
    public GameObject selectZoon;
    public GameObject selectDochter;

    public Animator blackscreen;

    public GameObject lightningMonster1;

    public GameObject deadScreen;

    // Start is called before the first frame update
    void Start()
    {
        deadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterUI();
    }

    private void UpdateCharacterUI()
    {
        if(currentCharacter == null)
        {
            trowableUI.SetActive(false);
            keyUI.SetActive(false);
            plankUI.SetActive(false);
            pickUpUI.SetActive(false);
            return;
        }

        trowableUI.SetActive(true);
        trowableTextUI.text = currentCharacter.trowableAmount.ToString();
        plankUI.SetActive(PlankOppakken.hasPlank);
        keyUI.SetActive(Player.hasKey);
    }

    public void Pickup(bool show)
    {
        pickUpUI.SetActive(show);
    }

    public void SelectChar(int index)
    {
        switch(index)
        {
            case 0:
                selectVader.SetActive(false);
                selectZoon.SetActive(true);
                selectDochter.SetActive(true);
                break;
            case 1:
                selectVader.SetActive(true);
                selectZoon.SetActive(false);
                selectDochter.SetActive(true);
                break;
            case 2:
                selectVader.SetActive(true);
                selectZoon.SetActive(true);
                selectDochter.SetActive(false);
                break;
            default:
                selectVader.SetActive(true);
                selectZoon.SetActive(true);
                selectDochter.SetActive(true);
                break;
        }
    }

    public void BlackScreen(bool show)
    {
        if(show)
        {
            blackscreen.Play("blackscreenOpen");
        }
        else
        {
            blackscreen.Play("blackscreen");
        }
    }

    public void RestartScene()
    {
        deadScreen.SetActive(false);
        print("Reset you fuck");
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractions : MonoBehaviour
{
    [SerializeField]
    private GameObject uiMenu;
    [SerializeField]
    private GameObject rageMeter;
    [SerializeField]
    private GameObject creditsUI;
    public void StartGame()
    {
        uiMenu.SetActive(false);
        creditsUI.SetActive(false);
        rageMeter.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        uiMenu.SetActive(true);
        creditsUI.SetActive(false);
        rageMeter.SetActive(false);
    }

    public void OpenCredits()
    {
        uiMenu.SetActive(false);
        creditsUI.SetActive(true);
        rageMeter.SetActive(false);
    }
}

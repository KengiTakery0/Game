using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenueController : MonoBehaviour
{
    [SerializeField] GameObject startPane;
    [SerializeField] GameObject newPane;

    [SerializeField] TMP_InputField userName;
    public void Drop()
    {
        DBM.DropSaves();        
    }
    public void OnNew()
    {
        startPane.SetActive(false);
        newPane.SetActive(true);
    }
    public void OnPlay()
    {
        startPane.SetActive(true);
        newPane.SetActive(false);
    }
    public void EnterName()
    {
        string name = userName.text;
        DBM.SaveUser(name);
        ToGame();
    }
    public void ToGame()
    {
        DBM.SaveEpisode(0);
        SceneManager.LoadScene(1);

    }
    public void Exit()
    {
        Application.Quit();
    }
}

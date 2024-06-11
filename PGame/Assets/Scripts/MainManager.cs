using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
    public bool isPaused = false;
    [Space]
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject DialoguePanel;
    [SerializeField] GameObject MovePanel;
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject ReadlePanel;
    [SerializeField] GameControl Readle;
    [SerializeField] DialogueMaster dialogueMaster;
    [Space]

    [SerializeField] GameObject Hol;
    [SerializeField] List<GameObject> ObjList;
    [SerializeField] List<InventioryCell> Icells;
    [SerializeField] List<GameObject> letter;

    [SerializeField] GameObject EndPanel;

    [Space]
    [SerializeField] List<Sprite> BGs;
    [SerializeField] Image BG;
    int bgIndex = 0;
    [SerializeField] bool isQuestComplite;
    int butcell = -1;
    int chcell = -1;
    int pepcell = -1;
    [SerializeField] GameObject but;
    public int ingest = 0;
    bool isGame;
    int letC;
    bool CompletedTask;
    public bool canEndGame;

    [SerializeField] Animator anim;

    private void CheckLetter()
    {
        for (int i = 0; i < Icells.Count; i++)
        {
            if (Icells[i].item != null)
                if (Icells[i].item.itemType == ItemType.letter && (Icells[i].item != Icells[i + 1].item
                        || Icells[i].item != Icells[i - 1].item))
                {
                    letC++;
                    Debug.Log(letC);
                }
        }
    }
    private void CheckQuest()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Icells[i].item != null)
            {
                if (Icells[i].item.itemType == ItemType.bread && butcell == -1)
                {
                    ingest++;
                    butcell = i;
                }
                if (Icells[i].item.itemType == ItemType.cheese && chcell == -1)
                {
                    ingest++;
                    chcell = i;
                }
                if (Icells[i].item.itemType == ItemType.pepperoni && pepcell == -1)
                {
                    ingest++;
                    pepcell = i;
                }
            }
            if (ingest == 3)
            {
                RemoveFromInventory(butcell);
                RemoveFromInventory(chcell);
                RemoveFromInventory(pepcell);
                isQuestComplite = true;
                ingest = 0;
                for (int j = 0; j < ObjList.Count; j++)
                {
                    ObjList[j].SetActive(false);
                }
                Hol.SetActive(false);
                AddToInventory(but);
            }
        }
    }

    public void EndGame()
    {
        DialoguePanel.SetActive(false);
        InventoryPanel.SetActive(false);
        MovePanel.SetActive(false);
        EndPanel.SetActive(true);
        Invoke("ToMainMenu", 10);
    }
    private void Update()
    {
        if (letC >= 10)
        {
            canEndGame = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = !isPaused;
            SetPause(isPaused);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = !isPaused;
            SetPause(isPaused);
            if (!isGame)
                StartCoroutine(dialogueMaster.StartDialogue());
        }

        CheckQuest();
    }

    private void ShowObjects()
    {
        for (int i = 0; i < ObjList.Count; i++)
        {
            ObjList[i].SetActive(false);
        }
        ObjList[bgIndex].SetActive(true);
    }

    public void SwapNextBG()
    {
        bgIndex++;
        if (bgIndex >= BGs.Count)
        {
            bgIndex = 0;
        }
        BG.sprite = BGs[bgIndex];
        ShowObjects();
    }
    public void SwapPrevBG()
    {
        if (bgIndex <= 0)
        {
            bgIndex = BGs.Count;
        }
        bgIndex--;
        BG.sprite = BGs[bgIndex];
        ShowObjects();
    }
    private void SetPause(bool pause)
    {
        PausePanel.SetActive(pause);
        Time.timeScale = pause ? 0f : 1f;
    }


    public void CompliteQuest()
    {
        if (!isQuestComplite && !CompletedTask) return;
        RemoveButer();
        Invoke("StartDialogue", 2f);
    }
    void RemoveButer()
    {
        Debug.Log(0);
        for (int i = 0; i < Icells.Count; i++)
        {
            if (Icells[i].item != null)
            {
                if (Icells[i].item.itemType == ItemType.Buter)
                {
                    Icells[i].Remove();
                    CompletedTask = true;
                    return;
                }

            }
        }
    }
    public void RemoveFromInventory(int inx)
    {
        for (int i = 0; i < Icells.Count; i++)
        {
            if (i == inx)
            {
                Icells[i].Remove();
                return;
            }
        }

    }
    public void AddToInventory(GameObject item)
    {
        for (int i = 0; i < Icells.Count; i++)
        {
            if (Icells[i].isTaken == false)
            {
                Icells[i].Add(item);
                if (Icells[i].item.itemType == ItemType.letter)
                {
                    letC++;
                }
                return;
            }
        }
    }
    public void StartDialogue()
    {
        InventoryPanel.SetActive(false);
        MovePanel.SetActive(false);
        DialoguePanel.SetActive(true);
        StartCoroutine(dialogueMaster.FinDialogue());
    }

    public void ExitReadle()
    {
        ReadlePanel.SetActive(false);
        InventoryPanel.SetActive(true);
        MovePanel.SetActive(true);
    }
    
    public void EnterReadle()
    {
        if (canEndGame)
        {
            ReadlePanel.SetActive(true);
            InventoryPanel.SetActive(false);
            MovePanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        DialoguePanel.SetActive(false);
        InventoryPanel.SetActive(true);
        MovePanel.SetActive(true);
        dialogueMaster.enabled = false;
        isGame = true;
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

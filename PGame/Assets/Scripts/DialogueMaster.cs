using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMaster : MonoBehaviour
{
    [SerializeField] MainManager mainManager;
    [SerializeField] Image BG;
    [SerializeField] Image Character;
    [SerializeField] TextMeshProUGUI CharacterName;
    [SerializeField] TextMeshProUGUI DialogueText;
    [SerializeField] AudioSource audioSource;
    [SerializeField]  int CurrentDialogueIndex = 0;
    [SerializeField] List<Dialogue> Dialogues;
    Dialogue CurrentDialogue;

    private void OnEnable()
    {
        if (DBM.LoadEpisode() != 0)
        {
            CurrentDialogueIndex = DBM.LoadEpisode();
        }
        else CurrentDialogueIndex = 0;
        BG.sprite = Dialogues[0].BGImage;
        audioSource.Play();
        StartCoroutine(StartDialogue());
        
    }

   
    public IEnumerator FinDialogue()
    {
        CurrentDialogue = Dialogues[14];
        DialogueText.text = CurrentDialogue.DialogueText;
        CharacterName.text = CurrentDialogue.CharacterName;
        BG.sprite = CurrentDialogue.BGImage;
        Character.gameObject.SetActive(true);
        Character.sprite = CurrentDialogue.CharacterImage;
        if (!mainManager.isPaused)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            mainManager.StartGame();
        }
    }
    public IEnumerator StartDialogue()
    {
       
        if (CurrentDialogueIndex <= 3)
        {
            audioSource.playOnAwake = true;
            audioSource.loop = true;
        }
        else
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        CurrentDialogue = Dialogues[CurrentDialogueIndex];
        DialogueText.text = CurrentDialogue.DialogueText;
        if (CurrentDialogue.CharacterName == "") CharacterName.text = DBM.GetLastUser();
        else CharacterName.text = CurrentDialogue.CharacterName;
        BG.sprite = CurrentDialogue.BGImage;
        if (CurrentDialogueIndex == 13)
        {
            audioSource.volume = .2f;
            BG.sprite = Dialogues[13].BGImage;
            mainManager.StartGame();
        }

        if (CurrentDialogue.AudioClip != null)
        {
            audioSource.clip = CurrentDialogue.AudioClip;
            audioSource.Play();
        }

        if (CurrentDialogue.CharacterImage != null)
        {
            Character.gameObject.SetActive(true);
            Character.sprite = CurrentDialogue.CharacterImage;
        }
        else Character.gameObject.SetActive(false);

        if (!mainManager.isPaused)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        DBM.SaveEpisode(CurrentDialogueIndex);
        CurrentDialogueIndex++;
        StartCoroutine(StartDialogue());
    }
}

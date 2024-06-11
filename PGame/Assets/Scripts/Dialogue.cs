using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "D", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public int Index;
    public string CharacterName;
    public string DialogueText;
    public Sprite CharacterImage;
    public Sprite BGImage;
    public AudioClip AudioClip;

   
}

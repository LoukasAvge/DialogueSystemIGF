using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A struct that hold the basic data for a dialogue
//Base Class 
[System.Serializable]
public struct DialogueData
{
    public string Name;
   
    [TextArea(3,10)]
    public string[] ArrayOfSentences;
    [TextArea(3, 10)]
    public string GoodAnswer;
    [TextArea(3, 10)]
    public string BadAnswer;

    public AudioSource NpcVoice;
    
    
    //  public string type;
}
//Base Class For player Choice
[System.Serializable]
public class Choice
{
    [TextArea(3, 10)]
    public string sentence;
    public string type;

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] Text NpcNameText;
    [SerializeField] Text DialogueText;
    [SerializeField] Text NextButtonText;
    [SerializeField] GameObject NextButton;
    [SerializeField] Animator thisAnimator;
    [SerializeField] Animator ChoiceBoxAnimator;
    [SerializeField] GameObject LongCamera;
    [SerializeField] GameObject FocusCamera;
    [SerializeField] AudioSource NpcVoice;
    //[SerializeField] Animator AnimatorOfChoiceBox;
    [SerializeField] Text ChoiceButtonText1;
    [SerializeField] Text ChoiceButtonText2;
    private bool DialogueIsEnding = false;
    string boolForAnimator = "IsOpen";
    //Creates a Queue To Store All sentences by FIFO
   private Queue<string> sentences;
    private List<string> GoodSentences;
    private List<string> BadSentences;
    private List<string> Choices;
    string Choice1Name;
    string Choice2Name;
    string Choice1Type;
    string Choice2Type;
    string GoodAnswer;
    string BadAnswer;
    bool FinishedChoice;
    
    private void Start()
    {
        //Closes Dialogue focus camera and actiavtes default camera also creates a new queue and makes finished choice false(finished choice is a bool to check if the player has chosen-
        //-beetween the two choices
        FocusCamera.SetActive(false);
        LongCamera.SetActive(true);
        sentences = new Queue<string>();
        FinishedChoice = false;
        
    }
    //Method to start the diallogue 
    public void StartDialogue(DialogueData dialogue , Choice choice , Choice choice2)
    {
        //When dialogue starts focus camera becomes enabled the chat box appears and the name of the dialogue box becomes the name of the current npc
        DialogueIsEnding = false;
        LongCamera.SetActive(false);
        FocusCamera.SetActive(true);
        thisAnimator.SetBool(boolForAnimator,true);
        NextButtonText.text = "Next";
        NpcNameText.text = dialogue.Name;
        //Clears all variables and the queue from previous values that can remain after a conversation
        Choice1Name = "";
        Choice2Name = "";
        Choice1Type = "";
        Choice2Type = "";
        GoodAnswer = "";
        BadAnswer = "";
        ChoiceButtonText1.text = Choice1Name;
        ChoiceButtonText2.text = Choice2Name;
        sentences.Clear();
        //Initializes every variable to the current npc's assigned variables from inspector
        GoodAnswer = dialogue.GoodAnswer;
        BadAnswer = dialogue.BadAnswer;
        Choice1Name = choice.sentence;
        Choice2Name = choice2.sentence;
        Choice1Type = choice.type;
        Choice2Type = choice2.type;
        NpcVoice = dialogue.NpcVoice;
        //loads the queue with the sentences from  the inspector
        foreach (string sentence in dialogue.ArrayOfSentences)
        {
            sentences.Enqueue(sentence);
        }
        NpcVoice.Play();
        //calls dispolay next sentenece
        DisplayNextSentence();
    }
    //Method to display next sentence
    public void DisplayNextSentence()
    {
        //if there is no other element in sentences remaining when the method is called end the conversation
        if (sentences.Count == 0 && FinishedChoice== true)
        {
            EndDialogue();
        }
        //if there is only one element left then call display choies and disable the button next because the player need to choose
        if (sentences.Count == 1)
        {
            NpcVoice.Play();
            DisplayChoices();
            NextButton.SetActive(false);
        }
        //If there is at least one element left in the queue the next sentence will be dispplayed 
        if(sentences.Count != 0)
        {
            NpcVoice.Play();
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

       
    }
    //Method to display the 2 choices the player must make
    public void DisplayChoices()
    {
        //Makes the value of the 2 choices tghe value from the inspector that the script of the current npc has
        ChoiceBoxAnimator.SetBool("IsChoiceOpen", true);
        string choice1content = Choice1Name;
        string choice2content = Choice2Name;

        ChoiceButtonText1.text = choice1content;
        ChoiceButtonText2.text = choice2content;

    }
    //Method to mnake Type effect when sentences are displayed 
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }
    //Method that is called if player selects the first choice and determines what is displayed after
    public void IfPlayerChoseFistChoice()
    {
        //Checks the choice's type , enables the next button again and displays the correct message based on the type of the choice the player made
        if(Choice1Type =="Good")
        {
            NpcVoice.Play();
            DialogueText.text = GoodAnswer;
            NextButton.SetActive(true);
            NextButtonText.text = "Close";
            FinishedChoice = true;
            ChoiceBoxAnimator.SetBool("IsChoiceOpen", false);
        }
        else 
        {
            NpcVoice.Play();
            DialogueText.text = BadAnswer;
            NextButton.SetActive(true);
            NextButtonText.text = "Close";
            FinishedChoice = true;
            ChoiceBoxAnimator.SetBool("IsChoiceOpen", false);
        }
        
        
    }
    //Method that is called if player selects the seconds choice and determines what is displayed after
    public void IfPlayerChoseSecondChoice()
    {

        //Checks the choice's type , enables the next button again and displays the correct message based on the type of the choice the player made
        if (Choice2Type == "Good")
        {
            NpcVoice.Play();
            DialogueText.text = GoodAnswer;
            NextButton.SetActive(true);
            NextButtonText.text = "Close";
            FinishedChoice = true;
            ChoiceBoxAnimator.SetBool("IsChoiceOpen", false);
        }
        else
        {
            NpcVoice.Play();
            DialogueText.text = BadAnswer;
            NextButton.SetActive(true);
            NextButtonText.text = "Close";
            FinishedChoice = true;
            ChoiceBoxAnimator.SetBool("IsChoiceOpen", false);

        }
    }
    //Method to end the dialogue
    void EndDialogue()
    {
        //changes camera focus to default , makes the chat box dissapear and changes tghe value of thhe bool that checks if dialogue has ended
        //The bool is mostly needed in DialogueTrigger script
        FocusCamera.SetActive(false);
        LongCamera.SetActive(true);
        DialogueIsEnding = true;
        thisAnimator.SetBool(boolForAnimator,false);
    }
    //Gets the queues element count
    public int GetQueueCount()
    {
        return sentences.Count;
    }
    //A get for the bool  that checks if dialogue has ended
    public bool GetBoolOfEnd()
    {
        return DialogueIsEnding;
    }
   
    
     
    
}

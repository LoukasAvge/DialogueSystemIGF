using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//All Npcs need a rigid body for raycast 
[RequireComponent(typeof(Rigidbody))]
public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;
    [SerializeField] Choice choice;
    [SerializeField] Choice choice2;
    [SerializeField] Transform playerPosition;
    [SerializeField] Animator NpcIsCloseBoxAnimator;
    [SerializeField] GameObject Player;
   bool IsDialogueEnding;
    //Creates 2 Vectors one for the npc position and one for the players
   
   
    float InitialDistanceToEngage = 2f;
  public  bool talking;

    void Start()
    {
       
       
      
        talking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //is dialogue ending is a bool from Dialogue manager that becomes true when dialogue ends and false when dialogue is under way
        IsDialogueEnding = FindObjectOfType<DialogueManager>().GetBoolOfEnd();
      
        
        //if player is close to the npc and is not already talking by pressing e the dialogue box appers the player cannot moce and we call the trigger dialogue method
        if (Vector3.Distance(transform.position , playerPosition.position) < InitialDistanceToEngage)
        {
           
            
            if(talking == false)
            {
                if (Input.GetKey(KeyCode.E))
                {

                    NpcIsCloseBoxAnimator.SetBool("IsOpen", false);
                    talking = true;
                    Player.GetComponent<PlayerMovement>().speed = 0;
                    TriggerDialogue();

                }

            }
          //when conversation end all changed values reset and dialogue box dissapears
            if (IsDialogueEnding == true)
            {

                NpcIsCloseBoxAnimator.SetBool("IsOpen", true);
                Player.GetComponent<PlayerMovement>().speed = 5;
                talking = false;
            }
            

        }
      
    }
   //Method Start dialogue of Dialogue Manager Script
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue , choice , choice2);
    }
   
    
}

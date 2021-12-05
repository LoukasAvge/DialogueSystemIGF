using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNpcInformation : MonoBehaviour
{
    [SerializeField] float maxDetectionDistance;
    [SerializeField] LayerMask NpcLayerMask;
    [SerializeField] Text NpcIsCloseText;
    [SerializeField] Animator NpcIsCloseBoxAnimator;
    Ray ray;
    RaycastHit hit;
    GameObject NpcHitResult;
    public bool DetectedNpc;
   public  bool talking;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        DetectedNpc = Physics.Raycast(ray, out hit, maxDetectionDistance, NpcLayerMask);

        //If player detects an npc via ray cast it checks the status of the npc's <DialogueTrigger> script in order to show the message if the ray hit and the player is nopt already having a-
        //-conversation with the npc
        if (DetectedNpc)
        {

           NpcHitResult = hit.rigidbody.gameObject;
            talking = NpcHitResult.GetComponent<DialogueTrigger>().talking;
           
            if (talking == false)
            {
               
                //Changes the text by getting the name of the npc that the ray hits
                NpcIsCloseText.text = "Press E To Talk To " + NpcHitResult.GetComponent<DialogueTrigger>().dialogue.Name;
                NpcIsCloseBoxAnimator.SetBool("IsOpen", true);


            }
            else
            {

                NpcIsCloseBoxAnimator.SetBool("IsOpen", false);
            }

        }
        else if (!DetectedNpc)
        {
            NpcIsCloseBoxAnimator.SetBool("IsOpen", false);
        }
    }
    

}

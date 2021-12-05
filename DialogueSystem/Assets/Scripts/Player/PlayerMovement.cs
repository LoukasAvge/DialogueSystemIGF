using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Animator thisAnimator;
   [SerializeField]CharacterController thisCharacterController;

    [Header("PlayerMovement")]
    float InpVer;
    float InpHor;
    float Speed_Sprint_Increment = 1;
   public float speed = 5;
    Vector3 Direction;

   

    // Start is called before the first frame update
    void Start()
    {
        thisCharacterController = GetComponent<CharacterController>();
        //thisAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        InpHor = Input.GetAxis("Horizontal");
        InpVer = Input.GetAxis("Vertical");
        
        Direction = new Vector3(InpHor * speed * Speed_Sprint_Increment, 0, InpVer * speed * Speed_Sprint_Increment);
        
        Direction = transform.TransformDirection(Direction);
        thisCharacterController.Move(Direction * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed_Sprint_Increment = 2;
        }
        else
        {
            Speed_Sprint_Increment = 1;
        }
    }
}

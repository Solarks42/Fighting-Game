using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    public CharacterController2D ControlMovement;
    float HorizontalMove = 0f;
    public float MoveSpeed = 40f;

    void Update()
    {
       HorizontalMove = InputAction.GetAxisRaw("Horizontal");
        Debug.Log(InputAction.GetAxisRaw("Horizontal"));
    }

    void FixedUpdate()
    {
        ControlMovement.Move(HorizontalMove * Time.fixedDeltaTime, false, false);
    }

}




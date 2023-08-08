using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour//MovementController
{


    private Rigidbody Rigidbody => rigidbody ??= GetComponent<Rigidbody>();
    private Rigidbody rigidbody;

    private PlayerAnimatorController PlayerAnimatorController => playerAnimatorController ??= GetComponent<PlayerAnimatorController>();
    private PlayerAnimatorController playerAnimatorController;


    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;

    void FixedUpdate()
    {

        Rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, Rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);


        if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            PlayerAnimatorController.PlayAnimation(AnimationType.Idle);

        }
        else if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
        {
            PlayerAnimatorController.PlayAnimation(AnimationType.Walk);

        }
    }
}


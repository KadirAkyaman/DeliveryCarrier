using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FixedJoystick _joystick;


    private void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            _animator.SetBool("isWalk", true);
        else
            _animator.SetBool("isWalk", false);

        if (ObjectStackController.Instance._objectList.Count > 0)
            _animator.SetBool("isHeld", true);
        else
            _animator.SetBool("isHeld", false);
    }
}

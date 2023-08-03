using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private FixedJoystick _joystick;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            _animator.SetBool("isRun",true);
        else
            _animator.SetBool("isRun", false);
    }
}

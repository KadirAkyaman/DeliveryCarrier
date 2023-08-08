using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private AnimationType _currentAnimation;

    [SerializeField] private Animator _animator;
    [SerializeField] private FixedJoystick _joystick;

    private Dictionary<AnimationType, string> _animationTypes = new Dictionary<AnimationType, string>()
    {
        {AnimationType.Idle,"Idle"},{AnimationType.Walk,"Walk"}
    };
    private void Start()
    {
        _currentAnimation = AnimationType.Idle;
    }

    public void PlayAnimation(AnimationType _animationType)
    {
        if (_currentAnimation==_animationType)
        {
            return;
        }
        if (_animationTypes.ContainsKey(_animationType))//add
        {
            _animator.Play(_animationTypes[_animationType]);
            _currentAnimation = _animationType;
        }
    }

    public void ChangeAnimationLayer(bool _activeLayer)
    {
        int layerIndex = _animator.GetLayerIndex("UpperBody");
        _animator.SetLayerWeight(layerIndex, (_activeLayer ? 1 : 0));
    }
}

public enum AnimationType
{
    Walk,
    Idle
}

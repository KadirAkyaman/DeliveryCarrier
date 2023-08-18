using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Pop Sound Effect")]
    public AudioClip popSoundEffect;

    [Header("Throw Sound Effect")]
    public AudioClip throwSoundEffect;

    private void Awake()
    {
        Instance = this;
    }
}

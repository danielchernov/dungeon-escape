using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioClip sfxAudio;

    public void PlayAttackSound()
    {
        AudioManager.Instance.PlaySFX(sfxAudio, 0.5f);
    }
}

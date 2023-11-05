using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.SoundEffect.UIButton);
    }
}

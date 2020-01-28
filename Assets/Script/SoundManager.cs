using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, cherryAudio;

    public bool isPlaySound = true;

   private void Awake() {
       instance = this;
   }

    public void JumpAudio()
    {
        if (isPlaySound)
        {
            audioSource.clip = jumpAudio;
            audioSource.Play();
        }
    }
    public void CherryAudio()
    {
        if (isPlaySound)
        {
            audioSource.clip = cherryAudio;
            audioSource.Play();
        }

    }
    public void HurtAudio()
    {
        if (isPlaySound)
        {
            audioSource.clip = jumpAudio;
            audioSource.Play();
        }
    }

    public void StopAll(){
        AudioSource[] source =  GetComponents<AudioSource>();
        for(int i = 0 ; i<source.Length;i ++){
            source[i].enabled = false;
        }
    }
}

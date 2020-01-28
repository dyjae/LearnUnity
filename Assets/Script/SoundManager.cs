using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpAudio,hurtAudio,cherryAudio;

    private void Awake() {
        instance = this;
    }

    public void JumpAudio(){
        audioSource.clip = jumpAudio;  
        audioSource.Play();  
    }
    public void CherryAudio(){
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }
    public void HurtAudio(){
        audioSource.clip = jumpAudio;   
        audioSource.Play();
    }
}

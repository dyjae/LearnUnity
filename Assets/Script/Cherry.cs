using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    Animator anim;

    AudioSource beCollectAudio;

    private void Start()
    {
        anim = GetComponent<Animator>();
        beCollectAudio = GetComponent<AudioSource>();
    }

    public void OnDead()
    {
        FindObjectOfType<PlayerController>().AddCherry();
        Destroy(transform.gameObject);
    }

    public void BeCollect()
    {
        beCollectAudio.Play();
        anim.SetBool("beCollect", true);
    }
}

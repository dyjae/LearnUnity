using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnDead()
    {
        FindObjectOfType<PlayerController>().AddCherry();
        Destroy(transform.gameObject);
    }

    public void BeCollect()
    {
        SoundManager.instance.CherryAudio();
        anim.SetBool("beCollect", true);
    }
}

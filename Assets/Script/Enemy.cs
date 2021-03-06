﻿using System;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    protected Rigidbody2D rb;

    protected Animator anim;

    protected AudioSource deathAudio;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    private void IsDesdory() {
        //关掉碰撞体
        GetComponent<Collider2D>().enabled = false;
        //销毁
        Destroy(this.gameObject);
    }


    public void OnDead()
    {
        deathAudio.Play();
        anim.SetBool("deading", true);
    }
}

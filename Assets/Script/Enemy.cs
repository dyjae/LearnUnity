using System;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    protected Rigidbody2D rb;

    protected Animator anim;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void IsDesdory() {
        Destroy(this.gameObject);
    }


    public void OnDead()
    {
        anim.SetBool("deading", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{

    //private Rigidbody2D rb;

    private Collider2D coll;

    //private Animator anim;

    public LayerMask ground;

    public Transform rightPoint,leftPoint;

    public bool isFaceLeft = true;

    public float speed = 2;

    public float ySpeed = 1;

    private float leftx, rightx;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(rightPoint.gameObject);
        Destroy(leftPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveMent();
        SwitchAnima();
    }

      void MoveMent()
    {
        if (isFaceLeft)
        {
            // Debug.Log(coll.name);
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, ySpeed);
            }
            if(transform.position.x < leftx)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                isFaceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, ySpeed);
            }
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                isFaceLeft = true;
            }
        }
    }

    void SwitchAnima()
    {
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("failing", true);
            }
        }
        if(coll.IsTouchingLayers(ground) && anim.GetBool("failing"))
        {
            anim.SetBool("failing",false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{

    public Transform top, buttom;

    public float topY, buttomY;

    public float speed;

    private bool isUp = true;

    // Start is called before the first frame update
     protected override void Start()
    {
        base.Start();
        topY = top.position.y;
        buttomY = buttom.position.y;
        Destroy(top.gameObject);
        Destroy(buttom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }

   void MoveMent()
    {

        if (IsUp())
        {
            isUp = false;
        }
        else if (IsDown())
        {
            isUp =true;
        }


        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
       
    }

    private bool IsUp()
    {
        float y = transform.position.y;
        return y > topY && y> buttomY;
    }

    private bool IsDown()
    {
        float y = transform.position.y;
        return y < buttomY && y < topY;
    }
}

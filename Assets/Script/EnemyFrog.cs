using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{

    private Rigidbody2D rb;

    public Transform rightPoint,leftPoint;

    public bool isFaceLeft = true;

    public float speed = 10;

    private float leftx, rightx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(rightPoint.gameObject);
        Destroy(leftPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }

    void MoveMent()
    {
        if (isFaceLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if(transform.position.x < leftPoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isFaceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightPoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isFaceLeft = true;
            }
        }
    }

}

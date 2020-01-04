using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 角色刚体
    public Rigidbody2D rb;

    // 速度
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("开始了");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("移动了");
        Movement();
    }

    void Movement()
    {
        //获取横向移动值
        float horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(horizontalMove);
        //不等于0时，表示有移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        }
    }
}
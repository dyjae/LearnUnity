using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 角色刚体
    public Rigidbody2D rb;

    // 动画
    public Animator anim;

    // 速度
    public float speed = 10f;

    // 跳跃
    public float jumpforce;

    // 地面图层
    public LayerMask grouder;

    // 玩家碰撞体
    public Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("开始了");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("移动了");
        Movement();
        SwitchAnim();
    }

    void Movement()
    {
        //获取横向移动值
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal"); //直接获得  -1，0，1
        Debug.Log(horizontalMove);
        //不等于0时，表示有移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        // 转向
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        // 跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (anim.GetBool("jumping"))
        {//跳跃时
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("failing", true);
            }
        }
        else if (coll.IsTouchingLayers(grouder))
        {//落地时
            anim.SetBool("failing", false);
            anim.SetBool("idle", true);
        }
    }
}
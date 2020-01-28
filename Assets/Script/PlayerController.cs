using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 角色刚体
    [SerializeField] private Rigidbody2D rb;

    // 玩家碰撞体
    public Collider2D coll;

    // 动画
    private Animator anim;

    // 速度
    public float speed = 10f;

    // 跳跃
    public float jumpforce;
    //地面监测
    public Transform groundCheck;

    // 地面图层
    public LayerMask grounder;

    //是否在地面上，是否跳跃
    public bool isGround, isJump;

    public int jumpCount = 2;

    public bool jumpPress;


    public Collider2D disColl;

    public Transform cellingCheck;

    private int cherry;

    public Text CherryNumber;

    private bool isHurt;


    // 操作杆
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CheckJump())
        {
            jumpPress = true;
        }
        CherryNumber.text = cherry.ToString();
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, grounder);
        if (!isHurt)
        {
            GroundMovement();
            Jump();
            Crouch();
        }
        SwitchAnim();
    }

    void GroundMovement()
    {
        float horizontal = GetHorizontal();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if (jumpPress && isGround)
        {
            SoundManager.instance.JumpAudio();
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPress = false; // 确保只执行一次
        }
        else if (jumpPress && jumpCount > 0 && isJump)
        {//在天上
            SoundManager.instance.JumpAudio();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPress = false; // 确保只执行一次
        }



    }

    //跳跃监测
    private bool CheckJump()
    {
        return (Input.GetButtonDown("Jump") || (joystick.enabled && joystick.Vertical > 0.5f)) && jumpCount > 0;
    }


    private float GetHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (joystick.enabled && joystick.Horizontal != 0)
        {
            horizontal = joystick.Horizontal;
        }
        return horizontal;
    }

    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));


        if (isGround)
        { //跑动
            anim.SetBool("failing", false);
        }
        else if (rb.velocity.y > 0)
        { //跳
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        { //下落
            anim.SetBool("jumping", false);
            anim.SetBool("failing", true);
        }

        if (isHurt)
        {
            anim.SetBool("hurting", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {
                anim.SetBool("hurting", false);

                isHurt = false;
            }
        }

    }

    //碰撞判断器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //搜集物品
        if (collision.CompareTag("Collection"))
        {
            Cherry cherryObj = collision.GetComponent<Cherry>();
            cherryObj.BeCollect();
        }
        else if (collision.CompareTag("DeadLine"))
        {
            SoundManager.instance.StopAll();
            Invoke("Restart", 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (anim.GetBool("failing"))
            {
                this.Jump();
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.OnDead();
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {//左侧移动
                this.Hurt(-5);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {//右侧移动
                Hurt(5);
            }
        }
    }



    private void Hurt(int h)
    {
        SoundManager.instance.HurtAudio();
        rb.velocity = new Vector2(h, rb.velocity.y);
        isHurt = true;
    }

    //下蹲
    private void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, grounder))//判断上面是否有碰撞物
        {
            if ((joystick.enabled && joystick.Vertical < -0.5f) || Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                disColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                disColl.enabled = true;
            }
        }

    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCherry()
    {
        cherry++;
    }
}

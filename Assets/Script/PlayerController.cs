using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 角色刚体
    [SerializeField] private Rigidbody2D rb;

    // 动画
    private Animator anim;

    // 速度
    public float speed = 10f;

    // 跳跃
    public float jumpforce;

    // 地面图层
    public LayerMask grouder;

    // 玩家碰撞体
    public Collider2D coll;

    public Collider2D disColl;

    public Transform cellingCheck;

    private int cherry;

    public Text CherryNumber;

    private bool isHurt;

    public AudioSource jumpAudio;

    public AudioSource hurtAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            //Debug.Log("移动了");
            Movement();
        }
        SwitchAnim();

    }

    void Movement()
    {
        //获取横向移动值
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal"); //直接获得  -1，0，1
        //Debug.Log(horizontalMove);
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
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(grouder))
        {
            this.Jump();
        }
        //下蹲
        Crouch();
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
        else if (isHurt)
        {
            anim.SetBool("hurting", true);
            anim.SetFloat("running", 0);
            if(Mathf.Abs(rb.velocity.x) < 0.1)
            {
                anim.SetBool("hurting", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
            
            
        }
        else if (coll.IsTouchingLayers(grouder))
        {//落地时
            anim.SetBool("failing", false);
            anim.SetBool("idle", true);
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
            cherry++;
            CherryNumber.text = cherry.ToString();
        }
        else if(collision.CompareTag("DeadLine")){
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            if (anim.GetBool("failing")) 
                {
                    //Debug.Log("JUMP");
                    this.Jump();
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.OnDead();
                    //Destroy(collision.gameObject);
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

    private void Jump() {
        jumpAudio.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
        anim.SetBool("jumping", true);
    }

    private void Hurt(int h)
    {
        hurtAudio.Play();
        rb.velocity = new Vector2(h, rb.velocity.y);
        isHurt = true;
    }

    //下蹲
    private void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, grouder))//判断上面是否有碰撞物
        {
            if (Input.GetButton("Crouch"))
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

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

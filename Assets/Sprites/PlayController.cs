using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour
{
    private Rigidbody2D rb;//刚体
    private Animator anim;
    private int Cherry = 0;//樱桃计数

    public AudioSource jumpAudio;//声音
    public Collider2D coll;
    public Transform CellingCheck;
    public Collider2D DisColl;
    [Space]
    public float speed = 400;//移动速度
    public float jumpforce = 600;
    [Space]
    public LayerMask ground;//下落地面
    public Text CherryNum;
    public bool isHurt = true;//默认是false


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
             Movement();
        }
        SwitchAnim();
    }

    void Movement()//设置函数让角色移动
    {
        float horizontalmove = Input.GetAxis("Horizontal");//-1=左,1=右，0=不动
        float facedircetion = Input.GetAxisRaw("Horizontal");

        //角色移动
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedircetion));
        }
        if (facedircetion != 0)
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }

        //角色跳跃
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
            jumpAudio.Play();
        }
        Crouch();
    }
    
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
            {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            } else
            {
                anim.SetBool("crouching", false);
            }
        }
    }

    void Restart()
    {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    //切换动画效果
    void SwitchAnim()
    {
        anim.SetBool("idling", false); 

        if (rb.velocity.y != 0f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }

        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            { 
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            } 
        }else if (isHurt)//如果触发受伤动画
        {
            anim.SetBool("hurting", true);
            if(Mathf.Abs(rb.velocity.x) < 0.5f )
            {
                anim.SetBool("hurting", false);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idling",true);
        }
    }
    
    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集物品
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }

        if (collision.tag == "DeadLine")
        { 
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }

    //kill enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling")) 
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
  
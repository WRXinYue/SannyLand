using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public Collider2D coll;

    private Animator anim;
    private Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        switchAnim();
    }


    void Movement()
    {
        //角色移动
        float horizontalMove = Input.GetAxis("HorizontalP1");
        //面向方向
        float faceDircetion = Input.GetAxisRaw("HorizontalP1");

        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(faceDircetion));//跑步动画
        }
        if (faceDircetion !=0)
        {
            transform.localScale = new Vector3(faceDircetion,1,1);
        }
        //角色跳跃
        if (Input.GetButton("JumpP1"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce*Time.fixedDeltaTime);
            if (rb.velocity.y > 0)
            {
                anim.SetBool("jumping", true);
            }
        }
    }
    void switchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public short speed;
    public short jumpForce;
    public Collider2D coll;
    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        switchAnim();
    }
    void Movement()
    {
        float horizontalMove = Input.GetAxis("HorizontalP2");
        float faceDircetion = Input.GetAxisRaw("HorizontalP2");

        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(faceDircetion));
        }
        if (faceDircetion != 0)
        {
            transform.localScale = new Vector3(-faceDircetion, 1, 1);
        }
        //Play Jump
        if (Input.GetButton("JumpP2"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
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
            /*if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
            }*/
        }
        if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("jumping", false);
        }
    }
}

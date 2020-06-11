using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    public float speed;
    public GroundCheck ground_script;
    public float gravity;
    public float jumpSpeed;
    public float jumpHeight;
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;


    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private float dashTime, jumpTime;
    private float beforeKey;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGround = ground_script.IsGround();

        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        float horizontalKey = Input.GetAxis("Horizontal");
        float verticalKey = Input.GetAxis("Vertical");

        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            if (verticalKey > 0 && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }




        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run", true);
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run", true);
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("run", false);
            xSpeed = 0.0f;
            dashTime += 0.0f;
        }

        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if(horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
}

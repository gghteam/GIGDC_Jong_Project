using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator anime;
    private SpriteRenderer SR;

    public float Speed;
    public float JumpPower;
    public float MaxSpeed;

    private float x; private float Onesec = 0;
    private float f = 0; private bool isJumping = false; private bool isDown = false;
    private Rigidbody2D rigidbody;
    private void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        anime = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        rigidbody.velocity = (new Vector2(x * Speed, 0));

        if(Input.GetButton("Horizontal"))
        {
            Onesec += Time.deltaTime;
            if (Onesec >= 1)
                anime.SetBool("IsRuning", true);
        }
        else 
        {
            Onesec = 0; anime.SetBool("IsRuning", false);
        }

        if (Input.GetButton("Horizontal"))
        {
            SR.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // 걷는 애니메이션
        if (!anime.GetBool("IsRuning"))
        {
            if (Mathf.Abs(rigidbody.velocity.x) < 0.2f)
                anime.SetBool("IsWalking", false);
            else
                anime.SetBool("IsWalking", true);
        }

        if (Input.GetKey(KeyCode.X) && !isJumping && !isDown)
        {
            f += Time.deltaTime;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 40);
            Debug.Log(f);
            if (f >= 0.3f)
            {
                isJumping = true; isDown = true;
                f = 0;
            }
            anime.SetBool("IsJumping", true);
        }

        if (Input.GetKeyUp(KeyCode.X)) { isDown = false; f = 0; }


        if (rigidbody.velocity.x > MaxSpeed)
        {
            rigidbody.velocity = new Vector2(MaxSpeed, rigidbody.velocity.y);
        }
        else if (rigidbody.velocity.x < -MaxSpeed)
        {
            rigidbody.velocity = new Vector2(-MaxSpeed, rigidbody.velocity.y);
        }

        if (rigidbody.velocity.y > JumpPower*3)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower * 3);
        }
        else if (rigidbody.velocity.y < -JumpPower * 3)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -JumpPower * 3);
        }

        Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 2, LayerMask.GetMask("Platform"));
        if(rigidbody.velocity.y < 0.3f)
        {
            if(rayHit.collider != null)
            {
                if(rayHit.distance > 0.2f)
                {
                    anime.SetBool("IsJumping", false); isJumping = false;
                }
                Debug.Log(rayHit.collider.name);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator anime;
    public SpriteRenderer SR;
    private PlayerStat stat;

    public float JumpPower;
    public float Speed;
    public float gravity = 0;
    public float beforeJumpY = 0f;

    private float time1 = 0; private float JumpTime = 0;
    
    private float speedTemp = 0; 
    private float jumpTemp = 0;

    private enum JumpState
    {
        NotJump,
        Falling,
        FirstJump,
        SecoundJump
    }

    [SerializeField]
    JumpState jumpState;
    JumpState beforeState;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        anime = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        stat = gameObject.GetComponent<PlayerStat>();

        speedTemp = Speed;
        jumpTemp = JumpPower;

        jumpState = JumpState.NotJump;
    }
    private void Update()
    {
        Move();
        Jump();
        JumpRayCast();

        //밑 코드는 데미지 테스트 코드 임다
        if(Input.GetKeyDown(KeyCode.D))
        {
            stat.OnDamage(10);
            Debug.Log(stat.hp);
        }
    }

    private void JumpRayCast()
    {
        // 무한으로 즐기는 점프 방지하기
        Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 2, LayerMask.GetMask("Platform"));
        if (rigidbody.velocity.y < 0.3f)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance > 0.2f)
                {
                    jumpState = JumpState.NotJump;
                    anime.SetBool("IsJumping", false);
                }
                //Debug.Log(rayHit.collider.name);
            }
            //else
            //{
            //    anime.SetBool("IsJumping", true); 
            //    endJump = true;
            //}
        }
    }

    private void Jump()
    {
        if(jumpState == JumpState.Falling)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -gravity);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (jumpState)
            {
                case JumpState.NotJump:
                    jumpState = JumpState.FirstJump;
                    beforeJumpY = transform.position.y;
                    break;
                case JumpState.FirstJump:
                    break;
                case JumpState.Falling:
                    if(beforeState == JumpState.FirstJump)
                    {
                        beforeJumpY = transform.position.y;
                        jumpState = JumpState.SecoundJump;
                    }
                    break;
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (jumpState != JumpState.Falling && jumpState != JumpState.NotJump)
            {
                anime.SetBool("IsJumping", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);

                if (transform.position.y > beforeJumpY + 5)
                {
                    beforeState = jumpState;
                    jumpState = JumpState.Falling;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            if (jumpState == JumpState.FirstJump)
            {
                beforeState = jumpState;
                anime.SetBool("IsJumping", false);
                jumpState = JumpState.Falling;
            }
            else if (jumpState == JumpState.SecoundJump)
            {
                beforeState = jumpState;
                anime.SetBool("IsJumping", false);
                jumpState = JumpState.Falling;
            }
        }
    }
    private void Move()
    {
        //움직이기
        if (anime.GetBool("IsRunning"))
        {
            rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed * 1.2f, 0);
        }
        else
        {
            rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, 0);
        }
        //0.5초 후 달리기
        if (Input.GetButton("Horizontal"))
        {
            time1 += Time.deltaTime;
            if (time1 >= 0.5f)
            {
                anime.SetBool("IsRunning", true);
            }
        }
        else
        {
            time1 = 0; anime.SetBool("IsRunning", false);
        }

        //방향전환
        if (Input.GetButton("Horizontal"))
        {
            SR.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // 걷는 애니메이션
        if (!anime.GetBool("IsRunning"))
        {
            if (Mathf.Abs(rigidbody.velocity.x) < 0.2f)
                anime.SetBool("IsWalking", false);
            else
                anime.SetBool("IsWalking", true);
        }

    }
}

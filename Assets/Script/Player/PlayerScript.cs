using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator anime;
    public SpriteRenderer SR;
    private PlayerStat stat;

    public float JumpPower;
    public float Speed;
    public float gravity = 0;

    private float time1 = 0; private float JumpTime = 0;
    
    private float speedTemp = 0; 
    private float jumpTemp = 0;

    private bool isJumping = false; 
    private bool isFall = false; // 낙하 중이면 true
    private bool doubleJump = false;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        anime = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        stat = gameObject.GetComponent<PlayerStat>();

        speedTemp = Speed;
        jumpTemp = JumpPower;
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
                    anime.SetBool("IsJumping", false);
                    doubleJump = false;
                    isJumping = false;
                }
                //Debug.Log(rayHit.collider.name);
            }
            else
            {
                anime.SetBool("IsJumping", true); 
                isJumping = true;
            }
        }
    }


    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, -gravity);
        //점프
        if (!isJumping)
        {
            if (Input.GetKey(KeyCode.X))// 처음 점프
            {
                JumpTime += Time.deltaTime;
                //JumpPower -= JumpTime * (jumpTemp / 10);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);
                if (JumpTime >= 0.3f)
                {
                    isJumping = true;
                    isFall = true;
                    JumpTime = 0;
                }
                anime.SetBool("IsJumping", true);
            }
        }
        else if (Input.GetKey(KeyCode.X)) // 두번째 점프
        {
            if (!doubleJump)
            {
                anime.Play("IsJumping");
                isFall = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);
                JumpTime += Time.deltaTime;
                if (JumpTime >= 0.3f)
                {
                    isFall = true;
                    JumpTime = 0;
                    doubleJump = true;
                }
            }
        }
        else
        {
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, -Gravity);
            JumpPower = jumpTemp;
        }

        //점프2
        if (Input.GetKeyUp(KeyCode.X))
        {
            isJumping = true;
            isFall = true;
            JumpTime = 0;
            JumpPower = jumpTemp;
            
        }

        //느리게 낙하
        if (isFall && Input.GetKey(KeyCode.X))
        { 
            Speed = speedTemp; 
            gravity = 2f;
        }
        else
        {
            gravity = 10f;
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
            if (!isFall)
            {
                time1 += Time.deltaTime;
                if (time1 >= 0.5f)
                {
                    anime.SetBool("IsRunning", true);
                }
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

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

    private bool isFall = false; // 낙하 중이면 true
    private bool firstJump = false;
    [SerializeField]
    private bool doubleJump = false;
    [SerializeField]
    private bool endJump = false;

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
                    endJump = false;
                    firstJump = false;
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
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, -gravity);

        if (!firstJump)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                beforeJumpY = transform.position.y;
                anime.SetBool("IsJumping", false);
                anime.SetBool("IsJumping", true);
            }
            if (Input.GetKey(KeyCode.X))
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);
                if (transform.position.y > beforeJumpY + 5)
                {
                    Debug.Log("fffff");
                    firstJump = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                firstJump = true;
            }
        }
        else if(!endJump)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                beforeJumpY = transform.position.y;
                doubleJump = true;
            }
            if (Input.GetKey(KeyCode.X) && doubleJump)
            {
                Debug.Log("ddddd");
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);
                if (transform.position.y > beforeJumpY + 5)
                {
                    doubleJump = false;
                    endJump = true;
                }
                
            }
            if (Input.GetKeyUp(KeyCode.X) && doubleJump)
            {
                doubleJump = false;
                endJump = true;
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

    private IEnumerator Anime_IsJumping()
    {
        anime.SetBool("IsJumping", false);
        yield return new WaitForSeconds(0.1f);
        anime.SetBool("IsJumping", true);
    }
}

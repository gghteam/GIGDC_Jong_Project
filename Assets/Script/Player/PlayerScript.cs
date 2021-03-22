using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator anime;
    public SpriteRenderer SR;

    public float RunSpeed;
    public float JumpPower;
    public float MaxSpeed;
    public float Gravity;

    private float time1 = 0; private float time2 = 0;
    
    private float x = 0;
    private float temp1 = 0; private float temp2 = 0; private float temp3 = 0;
    private bool isJumping = false; private bool isDown = false; private bool isFall = false;
    private Rigidbody2D rigidbody;
    private void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        anime = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        temp1 = MaxSpeed;
        temp2 = JumpPower;
        temp3 = Gravity;
    }
    private void Update()
    {
        //움직이기
        x = Input.GetAxis("Horizontal");
        rigidbody.velocity = (new Vector2(x * MaxSpeed, 0));

        //0.5초 후 달리기
        if (Input.GetButton("Horizontal"))
        {
            if (!isFall)
            {
                time1 += Time.deltaTime;
                if (time1 >= 0.5f)
                {
                    anime.SetBool("IsRuning", true);
                }
            }
        }
        else
        {
            time1 = 0; anime.SetBool("IsRuning", false);
        }

        //방향전환
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

       //점프
        if (Input.GetKey(KeyCode.X) && !isJumping && !isDown)
        {
            time2 += Time.deltaTime;
            JumpPower -= time2 * (temp2/10);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);
            if (time2 >= 0.3f)
            {
                isJumping = true; isDown = true;
                time2 = 0;
            }
            anime.SetBool("IsJumping", true);
        }
        else
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -Gravity); JumpPower = temp2;
        }

        //느리게 낙하
        if(isJumping && Input.GetKey(KeyCode.X))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -5); isFall = true; MaxSpeed = temp1; Debug.Log("적용 중");
        }
        else
        {
            Gravity = temp3; isFall = false;
        }

        //점프2
        if (Input.GetKeyUp(KeyCode.X)) { isJumping = true; isDown = false; time2 = 0; JumpPower = temp2; }

        //달리면 속도 빨라지기
        if (anime.GetBool("IsRuning"))
            MaxSpeed = RunSpeed;
        else
            MaxSpeed = temp1;

        //속도 제한 두기
        if (rigidbody.velocity.x > MaxSpeed)
        {
            rigidbody.velocity = new Vector2(MaxSpeed, rigidbody.velocity.y);
        }
        else if (rigidbody.velocity.x < -MaxSpeed)
        {
            rigidbody.velocity = new Vector2(-MaxSpeed, rigidbody.velocity.y);
        }

        //점프, 낙하 속도 제한 두기
        if (rigidbody.velocity.y > JumpPower*3)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower * 3);
        }
        else if (rigidbody.velocity.y < -JumpPower * 3)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -JumpPower * 3);
        }

        // 무한으로 즐기는 점프 방지하기
        Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 2, LayerMask.GetMask("Platform"));
        if(rigidbody.velocity.y < 0.3f)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance > 0.2f)
                {
                    anime.SetBool("IsJumping", false); isJumping = false;
                }
                Debug.Log(rayHit.collider.name);
            }
            else
            {
                anime.SetBool("IsJumping", true); isJumping = true;
            }
           
        }
    }
}

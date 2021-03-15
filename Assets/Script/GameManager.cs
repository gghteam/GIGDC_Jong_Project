using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public GameObject player;
    private GameObject[] bu;
    public GameObject dron1;
    public GameObject dron2;

    private Rigidbody2D rb;
    private Rigidbody2D prb;
    private SpriteRenderer SR;
    private Animator animator;

    public float BulletSpeed;
    public float playerMaxSpeed;
    public float JumpPower;

    private int count;
    private bool b = false;
    void Start()
    {
        bu = new GameObject[70];
        prb = player.GetComponent<Rigidbody2D>();
        SR = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
        for (int n = 0; n < bu.Length; n++)
        {
            bu[n] = Instantiate(bullet);
            bu[n].SetActive(false);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MainFunc();
    }

    private void Update()
    {
        // Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Fire();
        }

        // 점프
        if (Input.GetButtonDown("Jump") && !animator.GetBool("IsJumping"))
        {
            prb.AddForce(new Vector2(0, 10 * JumpPower), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }

        //방향키 뗏을 때 급격히 멈추게 하는 코드
        if (Input.GetButtonUp("Horizontal"))
        {
            prb.velocity = new Vector2(0, prb.velocity.y);
        }

        // 플레이어 방향 반전
        if (Input.GetButton("Horizontal"))
        {
            SR.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // 윗키
        if (Input.GetKey(KeyCode.UpArrow))
            b = true;
        else
            b = false;


        //아랫키 + 나중에 할거
        if (Input.GetKey(KeyCode.DownArrow))
        {

        }

        // 걷는 애니메이션
        if (Mathf.Abs(prb.velocity.x) < 0.2f)
            animator.SetBool("IsWalking", false);
        else
            animator.SetBool("IsWalking", true);
    }
    private void MainFunc()
    {

        // 움직이기
        float f = Input.GetAxisRaw("Horizontal");
        prb.AddForce(new Vector2(100 * f, 0), ForceMode2D.Impulse);

        //속도 제한 두기 (나중에 스킬을 통해 속도 늘리기)
        if(prb.velocity.x > playerMaxSpeed)
        {
            prb.velocity = new Vector2(playerMaxSpeed, prb.velocity.y);
        }
        if(prb.velocity.x < playerMaxSpeed * (-1))
        {
            prb.velocity = new Vector2(playerMaxSpeed * (-1), prb.velocity.y);
        }

        //무한 점프 막기
        Debug.DrawRay(player.transform.position, Vector2.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
        if (prb.velocity.y < 0)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance > 0.5f)
                {
                    animator.SetBool("IsJumping", false);
                }
                Debug.Log(rayHit.collider.name);
            }
        }
    }
    private void Fire()
    {
        // pool로 최적화를 한 총알 발사 코드
        for (int i = 0; i < bu.Length; i++)
        {
            if (!bu[i].activeSelf)
            {
                // 첫번째 드론의 총알 발사 코드
                bu[i].transform.position = dron1.transform.position;   
                bu[i].SetActive(true);                                  
                rb = bu[i].transform.GetComponent<Rigidbody2D>();       
                if (b)
                {
                    rb.AddForce(new Vector2(0, 1000f * BulletSpeed));
                    bu[i].transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else if (SR.flipX == true)
                {
                    rb.AddForce(new Vector2(-1000f * BulletSpeed, 0));
                    bu[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    rb.AddForce(new Vector2(1000f * BulletSpeed, 0));
                    bu[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                StartCoroutine(Wait4Sec(bu[i]));                        
                for (int j = 0; j < bu.Length; j++)                     
                {
                    if (!bu[j].activeSelf)
                    {
                        // 두번째 코드의 발사 코드
                        bu[j].transform.position = dron2.transform.position;
                        bu[j].SetActive(true);
                        rb = bu[j].transform.GetComponent<Rigidbody2D>();
                        if (b)
                        {
                            rb.AddForce(new Vector2(0, 1000f * BulletSpeed));
                            bu[j].transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (SR.flipX == true)
                        {
                            rb.AddForce(new Vector2(-1000f * BulletSpeed, 0));
                            bu[j].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                        {
                            rb.AddForce(new Vector2(1000f * BulletSpeed, 0));
                            bu[j].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        StartCoroutine(Wait4Sec(bu[j]));
                        return;
                    }
                }
            }
            
        }
    }
    IEnumerator Wait4Sec(GameObject gm)
    {
        // 발사된 총알이 2초뒤 비활설화 됨
        yield return new WaitForSeconds(2f);
        gm.SetActive(false);
    }
}

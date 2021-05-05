using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float xMove;
    private PlayerInput playerInput;
    private PlayerTP playerTP;
    private PlayerHit playerhit;

    [Header("이동관련")]
    public bool facingRight = true;
    public float moveSpeed = 8f;
    public float runSpeed = 20f;
    public float walkToRun = 1f;
    public bool dontMove = false;

    [Header("점프관련")]
    public bool isGround = false;
    public float maxJumpHeight = 5f; //최대 점프 높이
    public LayerMask whatIsGround; //무엇이 바닥인가?
    public Transform groundChecker;
    public int maxJumpCount = 2; //최대 2회 점프
    public float jumpSpeed = 5f;
    public float jumpTime = 0.5f; //.5초동안 누르고 있을 수 있다.


    private int jumpCount;
    private float stayJumpKey = 0;
    private float stayMoveKey = 0;
    private float startYPos = 0; //점프를 시작했을 때 y포지션

    private float saveMoveSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerTP = GetComponent<PlayerTP>();
        playerhit = GetComponent<PlayerHit>();
        saveMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (dontMove)
            return;

        if (playerTP.state == TPState.CHARGING)
        {
            xMove = 0;
            return; //차징중일때는 움직일 수 없고 오로지 차저만을 작동시킨다.
        }

        xMove = playerInput.xMove;
        if(playerInput.xMove != 0)
        { // 움직이고 있을 때
            MoveStart();
        }

        if ((facingRight && xMove < 0) || (!facingRight && xMove > 0))
        {
            Flip();
        }

        if (playerInput.jump)
        { //점프키가 눌렸을 때
            JumpStart();
        }
    }
    private void MoveStart()
    {
        if(playerInput.xMove != 0)
        {
            stayMoveKey = walkToRun; // walkToRun만큼 작동
            StartCoroutine(MoveKeyHolding());
        }
    }
    IEnumerator MoveKeyHolding()
    {
        while(playerInput.xMove != 0)
        {
            stayMoveKey -= Time.deltaTime;
            if(stayMoveKey < 0 && isGround)
            { // walkToRun의 시간이 끝나면 달리기로 변환
                moveSpeed = runSpeed;
            }
            yield return null; // 프레임 마다 실행
        }
        moveSpeed = saveMoveSpeed;
    }
    private void JumpStart()
    {
        if (isGround || jumpCount > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            stayJumpKey = jumpTime; //점프시간만큼만 점프키를 누를 수 있다.
            startYPos = transform.position.y;
            StartCoroutine(JumpKeyHolding());
        }
    }

    IEnumerator JumpKeyHolding()
    {
        //키가 계속 눌리고 있거나, 점프키다운시간이 다되었거나 점프높이를 초과했다면 그만
        while (playerInput.jumpHolding && stayJumpKey > 0 && transform.position.y <= startYPos + maxJumpHeight)
        {
            stayJumpKey -= Time.deltaTime;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            yield return null;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, 0); //가속 제거
        jumpCount--;
    }

    void FixedUpdate()
    {
        if (dontMove)
            return;
        rigid.velocity = new Vector3(xMove * moveSpeed, rigid.velocity.y);

        isGround = Physics2D.OverlapCircle(groundChecker.position, 0.1f, whatIsGround);

        if (isGround && rigid.velocity.y < 0)
        { //땅바닥에 착지했다면 점프카운트 리셋
            jumpCount = maxJumpCount;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // -1을 곱해줘서 반전해주기
        facingRight = !facingRight;
        transform.localScale = scale;
    }
}

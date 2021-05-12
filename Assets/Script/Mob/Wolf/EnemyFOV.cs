using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float viewRange = 10f;
    [Range(0, 360)]
    public float viewAngle = 30f;
    public LayerMask layerMask;
    private int playerLayer;
    private EnemyMove enemyMove;
    public Vector3 targetPos;
    private void Awake()
	{
        enemyMove = GetComponent<EnemyMove>();
        playerLayer = LayerMask.NameToLayer("Player");
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector2 CirclePoint(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
    //플레이어와 나 사이에 다른 장애물이 없는가?
    public bool IsViewPlayer()      // 플레이어를 봤을때
    {
        bool isView = false;
        Vector2 dir = GameManager.Player.position - transform.position;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir.normalized, viewRange, layerMask);
        Debug.DrawRay(transform.position, dir.normalized * (viewRange+10), Color.red, 0.1f);
        if (hit2D.collider != null)
        {
            isView = (hit2D.collider.gameObject.CompareTag("Player"));
            Debug.Log(isView);
        }

        return isView;
    }

    public bool IsTracePlayer()         //원안에 들어왔을때 무조건 인식
    {
        bool isTrace = false;
        Collider2D col = Physics2D.OverlapCircle(transform.position, viewRange, 1 << playerLayer);
        if (col != null)
        { //서클안에 충돌체가 있다면 거리 측정
            Vector2 dir = GameManager.Player.position - transform.position; //z를 무시하고 받기 위해 Vector2로 형변환 한다.
            Vector3 right = enemyMove.facingright ? transform.right*-1 : transform.right;
            // Debug.DrawRay(transform.position, dir.normalized * viewRange, Color.white, 0.1f);
            if (enemyMove.isTargeting)
            {
                isTrace = true;
            }

            if (Vector2.Angle(right, dir) < viewAngle * 0.5f)           //늑대 시선
            {
                isTrace = true;
            }
            targetPos = new Vector3(transform.position.x + dir.x , transform.position.y);
        }
        return isTrace;
    }
}

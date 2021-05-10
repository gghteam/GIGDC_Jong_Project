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
        //if (IsTracePlayer())    //원 안에 플레이어가 있는가
        //{
        //    if (IsViewPlayer())//플레이어와 적 사이에 다른 물체는 없는가?
        //    {
        //        if (IsCanAttack())  //플레이어가 적 공격 사거리안에 들어왔는가
        //        {
        //        }
        //    }

        //}
    }
    public Vector2 CirclePoint(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
    //플레이어와 나 사이에 다른 장애물이 없는가?
    public bool IsViewPlayer()
    {
        bool isView = false;
        Vector2 dirVec = GameManager.Player.position - transform.position;
        float dir;
        if (dirVec.x >= 0)
        {
            dir = 1f;
        }
        else
        {
            dir = -1f;
        }            
        
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dirVec.normalized, viewRange, layerMask);
        Debug.DrawRay(transform.position, dirVec.normalized * viewRange, Color.red, 0.1f);
        if (hit2D.collider != null)
        {
            isView = (hit2D.collider.gameObject.CompareTag("Player"));
            if (isView)
                targetPos = (hit2D.collider.gameObject.transform.position /*+ new Vector3(dir*5,0,0)*/);
            Debug.Log(targetPos);
        }

        return isView;
    }
    public bool IsCanAttack()
    {
        bool canAttack = false;
        Vector2 dir = GameManager.Player.position - transform.position;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir.normalized, viewRange*0.7f, layerMask);
        Debug.DrawRay(transform.position, dir.normalized * viewRange*0.7f, Color.green, 0.1f);
        if (hit2D.collider != null)
        {
            canAttack = (hit2D.collider.gameObject.CompareTag("Player"));
        }

        return canAttack;
    }
    public bool IsTracePlayer()
	{
		bool isTrace = false;
		Collider2D col = Physics2D.OverlapCircle(transform.position, viewRange, 1 << playerLayer);
		if (col != null)
		{ //서클안에 충돌체가 있다면 거리 측정
            
			Vector2 dir = GameManager.Player.position - transform.position; //z를 무시하고 받기 위해 Vector2로 형변환 한다.
            Vector2 right = enemyMove.facingright ? transform.right *-1 : transform.right;
			if (Vector2.Angle(right, dir) < viewAngle * 0.5f&&GameManager.GetScale() != 0)
			{
				isTrace = true;
                //Debug.Log("각도 안에 플레이어 들어옴");
			}
		}
		return isTrace;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float viewRange = 10f;
    [Range(0, 360)]
    public float viewAngle = 30f;
    public LayerMask layerMask;
    private int playerLayer;
    private void Awake()
	{
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
    public bool IsViewPlayer()
    {
        bool isView = false;
        Vector2 dir = GameManager.Player.position - transform.position;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir.normalized, viewRange, layerMask);
        if (hit2D.collider != null)
        {
            isView = (hit2D.collider.gameObject.CompareTag("Player"));
        }

        return isView;
    }
    //public bool IsTracePlayer()
    //{
    //    bool isTrace = false;
    //    Collider2D col = Physics2D.OverlapCircle(transform.position, viewRange, 1 << playerLayer);
    //    if (col != null)
    //    { //서클안에 충돌체가 있다면 거리 측정
    //        Vector2 dir = GameManager.Player.position - transform.position; //z를 무시하고 받기 위해 Vector2로 형변환 한다.
    //        //Vector3 right = enemyMove.facingRight ? transform.right : transform.right * -1;

    //        if (Vector2.Angle(right, dir) < viewAngle * 0.5f)
    //        {
    //            isTrace = true;
    //        }
    //    }
    //    return isTrace;
    //}
}

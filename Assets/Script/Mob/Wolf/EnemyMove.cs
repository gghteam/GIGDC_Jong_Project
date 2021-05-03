using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool facingright = true;
    private Rigidbody2D rigid;

    public bool isMoving = true;
    public float patrolDistance = 5f;
    public float movingSpeed = 2f;
    public float stayDelay = 3f;

    public Vector3[] patrolPoint = { new Vector3(0, 0, 0), new Vector3(5, 0, 0), new Vector3(10, 0, 0) };
    private List<Vector2> patrolList = new List<Vector2>();
    private int patrolIndex = 0;
    private Vector3 destination;

    private bool isStop = false;
    // Start is called before the first frame update
    private void Awake()
	{
        rigid = GetComponent<Rigidbody2D>();
        patrolPoint[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < patrolPoint.Length; i++)
        {
            patrolPoint[i] = new Vector3(patrolPoint[0].x+ patrolDistance*(i), patrolPoint[0].y, patrolPoint[0].z);
            patrolList.Add(patrolPoint[i]);
        }
        destination = patrolPoint[0];


    }
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;
        if ((destination - transform.position).sqrMagnitude <= 0.5f)
        {
            isStop = true;
            StartCoroutine(StayPoint());
        }
        rigid.velocity = (destination - transform.position).normalized * GetSpeed();

        if ((!facingright && rigid.velocity.x < 0) || (facingright && rigid.velocity.x > 0))
        {
            Flip();
        }
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero;
        StopAllCoroutines(); //모든 코루틴 종료
    }
    IEnumerator StayPoint()
    {
        yield return new WaitForSeconds(stayDelay);
        MoveNextPosition();
    }
    private void MoveNextPosition()
    {
        patrolIndex = (++patrolIndex) % patrolList.Count; //다음 패트롤 인덱스 구함
        destination = patrolList[patrolIndex];// 다음 인덱스를 패트롤리스트에 넣음
        isStop = false;
    }
    private float GetSpeed()
    {
        return movingSpeed * GameManager.GetScale();
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        facingright = !facingright;
        transform.localScale = scale;
    }
}

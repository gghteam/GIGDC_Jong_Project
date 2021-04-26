using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;

public class Wolf : MonoBehaviour
{
    private Stat stat;
    private AttackRangeCheck check;

    public float speed = 10f;
    public bool onTarget;
    public bool canMove;
    public bool inWall;
    public int dir = 1;
    public Rigidbody2D rigidbady;

    //RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        rigidbady = GetComponent<Rigidbody2D>();
        check = GetComponentInChildren<AttackRangeCheck>();
        stat = GetComponent<Stat>();
    }
    private void Update()
    {
        //공격 범위 안에 들어오면 공격 시작
        if (check.canAttack&& onTarget)
        {
            StartCoroutine(WolfAttack());
        }
        

        Debug.DrawRay(transform.position, Vector3.right*dir*2.5f, Color.blue, 0.001f);
    }

    // Update is called once per frame
  
    IEnumerator WolfAttack()
    {
        check.canAttack = false;
        canMove = false;
        yield return new WaitForSeconds(1f);

        DOTween.Clear();
        //점프 공격들

        //transform.DOJump(new Vector3(transform.position.x + 20 * dir, 2, 0), 2, 1, 1f); // 이건 점프하는거 
        //transform.DOBlendableLocalMoveBy(new Vector3(3 * dir, 0, 0), 1f);
        rigidbady.AddForce(Vector2.right*dir*15,ForceMode2D.Impulse);
        rigidbady.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        Debug.Log("공격");
        yield return new WaitForSeconds(1f);
        canMove = true;

    }
}

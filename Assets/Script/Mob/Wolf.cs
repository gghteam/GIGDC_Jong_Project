using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Wolf : MonoBehaviour
{
    private Stat stat;
    private AttackRangeCheck check;
    private BoxCollider2D range;
    public float speed = 10f;
    public BoxCollider2D attackZone;
    public bool isAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        check = GetComponentInChildren<AttackRangeCheck>();
        stat = GetComponent<Stat>();
        range = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (check.canAttack)
        {
            StartCoroutine(WolfAttack());
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            range.size = new Vector2(range.size.x*2, range.size.y * 2);
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")&& !isAttack)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            //오른쪽
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                transform.rotation = new Quaternion(0, 180, 0,0);
                
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
   
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            range.size = new Vector2(1.5f, 1.5f);
        }
    }
    IEnumerator WolfAttack()
    {
        yield return new WaitForSeconds(0.5f);
        check.canAttack = false;
        Debug.Log("공격");
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wolf : MonoBehaviour
{
    private Stat stat;
    private BoxCollider2D range;
    private SpriteRenderer rd;
    public float speed = 10f;
    public bool isAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponent<Stat>();
        range = GetComponent<BoxCollider2D>();
        rd = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isAttack)
        {
            StartCoroutine(WolfAttack());
            isAttack = false;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            range.size = new Vector2(3f, 1.5f);
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")&& !isAttack)
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                rd.flipX = true;
                transform.Translate(Vector2.right*speed * Time.deltaTime);
            }
            else
            {
                rd.flipX = false;
                transform.Translate(Vector2.left *speed* Time.deltaTime);
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
        for (int i = 0; i < 30; i++)
        {
            transform.Translate(0.1f, 0.1f, 0);
        }
        
    }
}

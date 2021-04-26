using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private float dir = -1;
    private float startX;
    private float startY;
    public float speed;
    public float maxX;  //오른쪽으로 해당 값만큼 이동가능
    public float minX;  //왼쪽으로 해당 값 만큼 이동 가능
    public float maxY;
    public float minY;
    public bool isX = true;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;  //초기 x 저장
        startY = transform.position.y;  //초기 y 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (isX)
        {
            MoveX();
        }
        else
        {
            MoveY();
        }
        
        
    }
    void MoveX()
    {
        transform.Translate(Vector2.right* dir * speed*Time.deltaTime);
        if (transform.position.x > startX+maxX)     //오른쪽
        {
            dir = -1;
        }
        else if(transform.position.x < startX-minX)     //왼쪽
        {
            dir = 1;
        }
    }
    void MoveY()
    {
        transform.Translate(Vector2.up * dir* speed * Time.deltaTime);
        if (transform.position.y > startY + maxY)     //위
        {
            dir = -1;
        }
        else if (transform.position.y < startY - minY)     //아래
        {
            dir = 1;
        }
    }
}

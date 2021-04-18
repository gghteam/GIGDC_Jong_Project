using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private Text damageText;

    private Color textColor;

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float alphaSpeed = 2f;
    [SerializeField]
    private float destroyTime = 2f;

    public int damage;

    private void Start()
    {
        damageText = GetComponent<Text>();
        textColor = damageText.color;

        Invoke("DestoryObj", destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));

        textColor.a = Mathf.Lerp(textColor.a, 0, Time.deltaTime * alphaSpeed);
        damageText.color = textColor;
    }

    private void DestoryObj()
    {
        gameObject.SetActive(false);
    }
}

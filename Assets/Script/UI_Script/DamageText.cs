using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMesh damageText;

    private Color textColor;
    [SerializeField]
    private Color originColor;

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float alphaSpeed = 1f;
    [SerializeField]
    private float destroyTime = 2f;

    private void Start()
    {
        damageText = GetComponent<TextMesh>();
        damageText.color = originColor;
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

    private void OnEnable()
    {
        damageText = GetComponent<TextMesh>();
        damageText.color = originColor;
        textColor = damageText.color;
        Invoke("DestoryObj", destroyTime);
    }
}

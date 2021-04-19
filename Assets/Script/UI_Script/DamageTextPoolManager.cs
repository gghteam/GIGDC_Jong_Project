using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextPoolManager : MonoBehaviour
{
    private static DamageTextPoolManager instance;

    [SerializeField]
    private int count = 10;

    [SerializeField]
    private GameObject textPrefabs = null;

    private List<GameObject> textList = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CreatePrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject damageText = Instantiate(textPrefabs) as GameObject;
            damageText.transform.parent = this.transform;
            damageText.SetActive(false);
            textList.Add(damageText);
        }
    }

    public GameObject GetDamageText(Vector3 pos, float damage)
    {
        GameObject reqText = null;
        TextMesh damageText = null;

        for (int i = 0; i < textList.Count; i++)
        {
            if (textList[i].activeSelf.Equals(false))
            {
                reqText = textList[i];
                break;
            }
        }
        if (reqText == null)
        {
            GameObject newText = Instantiate(textPrefabs) as GameObject;
            newText.transform.parent = this.transform;
            textList.Add(newText);
            reqText = newText;
        };

        reqText.SetActive(true);
        damageText = reqText.GetComponent<TextMesh>();

        if (damageText.Equals(null))
        {
            Debug.LogError("텍스트 속성을 가져오지 못했습니다");
        }

        damageText.text = damage.ToString();
        reqText.transform.position = pos;
        return reqText;
    }


}

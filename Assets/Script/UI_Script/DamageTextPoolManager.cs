using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject GetDamageText(Vector3 pos)
    {
        GameObject reqText = null;

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
            GameObject newArrow = Instantiate(textPrefabs) as GameObject;
            newArrow.transform.parent = this.transform;
            textList.Add(newArrow);
            reqText = newArrow;
        }
        reqText.SetActive(true);
        reqText.transform.position = pos;
        return reqText;
    }


}

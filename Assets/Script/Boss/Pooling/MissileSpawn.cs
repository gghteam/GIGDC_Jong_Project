using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawn : MonoBehaviour
{
    public static MissileSpawn instance;
    public GameObject missilePrepab;

    List<GameObject> missile = new List<GameObject>();

    private void Awake()
    {
        if (MissileSpawn.instance == null)
        {
            MissileSpawn.instance = this;
        }
    }
    void Start()
    {
        CreateMissile(5);
    }
    public void CreateMissile(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _missile = Instantiate(missilePrepab) as GameObject;
            _missile.transform.parent = this.transform;
            _missile.SetActive(false);
            missile.Add(_missile);
        }
    }
    public GameObject GetMissile(Vector3 pos)
    {
        GameObject reqMissile = null;
        for (int i = 0; i < missile.Count; i++)
        {
            if (missile[i].activeSelf.Equals(false))
            {
                reqMissile = missile[i];
                break;
            }
        }
        if (reqMissile == null)
        {
            GameObject newMissile = Instantiate(missilePrepab) as GameObject;
            newMissile.transform.parent = this.transform;
            missile.Add(newMissile);
            reqMissile = newMissile;
        }
        reqMissile.SetActive(true);
        reqMissile.transform.position = pos;
        return reqMissile;
    }

    public void AllClear_Missile()
    {
        for (int i = 0; i < missile.Count; i++)
        {
            missile[i].SetActive(false);
        }
    }
}

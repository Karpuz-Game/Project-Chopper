using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chopperBulletPool : MonoBehaviour
{
    public int poolSize = 10;

    List<GameObject> goList;

    [SerializeField] private GameObject objectPrefabToPool;

    void Awake()
    {
  

        goList = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tempGO = Instantiate(objectPrefabToPool) as GameObject;
            tempGO.transform.SetParent(transform);
            goList.Add(tempGO);
            goList[i].SetActive(false);
        }


    }

    public GameObject GetNextAvailableObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!goList[i].activeSelf)
            {
                goList[i].SetActive(true);
                return goList[i];
            }

        }
        return null;
    }
}
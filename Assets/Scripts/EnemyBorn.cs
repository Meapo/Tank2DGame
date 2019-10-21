using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorn : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1.0f);
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        int num = Random.Range(0, 19);
        int numRed = Random.Range(0, 2);
        if (numRed < 1)
        {
            if (num < 6)
            {
                GameObject go = Instantiate(enemyPrefab[5], transform.position, Quaternion.identity);
                MapCreator.Instance1.enemyList.Add(go);
            }
            else if (num < 12)
            {
                GameObject go = Instantiate(enemyPrefab[4], transform.position, Quaternion.identity);
                MapCreator.Instance1.enemyList.Add(go);
            }
            else
            {
                GameObject go = Instantiate(enemyPrefab[3], transform.position, Quaternion.identity);
                MapCreator.Instance1.enemyList.Add(go);
            }
            
        }
        else
        {
            if (num < 2)
            {
                GameObject go = Instantiate(enemyPrefab[2], transform.position, Quaternion.identity);
                MapCreator.Instance1.enemyList.Add(go);
            }
            else if (num < 11)
            {
                GameObject go = Instantiate(enemyPrefab[1], transform.position, Quaternion.identity);
                MapCreator.Instance1.enemyList.Add(go);
            }
            else
            {
                GameObject go = Instantiate(enemyPrefab[0], transform.position, Quaternion.identity);
                MapCreator.Instance1.enemyList.Add(go);
            }
        }
        
    }
}

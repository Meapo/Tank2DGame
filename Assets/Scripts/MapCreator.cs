using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // 初始化地图所需物品
    // 0. Base 1. Wall 2. Barrier 3. Water 4. Grass 5. Born 6. EnemyBorn 7. AirBarrierH 8. AirBarrierV
    public GameObject[] items;

    public float baseBarrierTime;

    public List<Vector3> itemPosition = new List<Vector3>();

    public List<GameObject> enemyList = new List<GameObject>();

    public List<GameObject> baseWallList = new List<GameObject>();

    static MapCreator Instance;

    public static MapCreator Instance1
    {
        get
        {
            return Instance;
        }

        set
        {
            Instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
        InitMap();
    }

    void Update()
    {
        if (baseBarrierTime >= 0)
        {
            createBaseBarrier();
            baseBarrierTime -= Time.deltaTime;
            if (!IsInvoking("createBaseWall"))
            {
                Invoke("createBaseWall", baseBarrierTime);
            }
        }
    }
    // 初始化地图
    private void InitMap()
    {
        // instantiate base
        createItem(items[0], new Vector3(0.0f, -8.0f, 0.0f), Quaternion.identity);
        GameObject item = Instantiate(items[1], new Vector3(-1.0f, -8.0f, 0.0f), Quaternion.identity);
        baseWallList.Add(item);
        itemPosition.Add(item.transform.position);
        item = Instantiate(items[1], new Vector3(1.0f, -8.0f, 0.0f), Quaternion.identity);
        baseWallList.Add(item);
        itemPosition.Add(item.transform.position);
        for (int i = -1; i < 2; i++)
        {
            item = Instantiate(items[1], new Vector3(i, -7.0f, 0.0f), Quaternion.identity);
            baseWallList.Add(item);
            itemPosition.Add(item.transform.position);
        }
        // instantiate airBarrier
        createItem(items[7], new Vector3(0.0f, -9.0f, 0.0f), Quaternion.identity);
        createItem(items[7], new Vector3(0.0f, 9.0f, 0.0f), Quaternion.identity);
        createItem(items[8], new Vector3(-11.0f, 0.0f, 0.0f), Quaternion.identity);
        createItem(items[8], new Vector3(11.0f, 0.0f, 0.0f), Quaternion.identity);

        // instantiate Player 
        createItem(items[5], new Vector3(-2.0f, -8.0f, 0.0f), Quaternion.identity);
        if (!position.Instance.isSingleMode)
        {
            GameObject go = Instantiate(items[5], new Vector3(2.0f, -8.0f, 0.0f), Quaternion.identity);
            go.GetComponent<Born>().isPlayer1 = false;
        }
        // instantiate Enemy
        createItem(items[6], new Vector3(-10.0f, 8.0f, 0.0f), Quaternion.identity);
        createItem(items[6], new Vector3(0.0f, 8.0f, 0.0f), Quaternion.identity);
        createItem(items[6], new Vector3(10.0f, 8.0f, 0.0f), Quaternion.identity);

        InvokeRepeating("createEnemy", 5.0f, 5);
        // instantiate wall/barrier/water/grass
        for (int i = 0; i < 50; i++)
        {
            createItem(items[1], createRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            createItem(items[2], createRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            createItem(items[3], createRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            createItem(items[4], createRandomPosition(), Quaternion.identity);
        }
    }
    private void createBaseBarrier()
    {
        // destroy wall of base
        foreach (GameObject wall in MapCreator.Instance1.baseWallList)
        {
            Destroy(wall);
        }
        baseWallList.RemoveRange(0, 5);
        // instantiate wall of base
        GameObject go = Instantiate(items[2], new Vector3(-1.0f, -8.0f, 0.0f), Quaternion.identity);
        baseWallList.Add(go);
        go = Instantiate(items[2], new Vector3(1.0f, -8.0f, 0.0f), Quaternion.identity);
        baseWallList.Add(go);
        for (int i = -1; i < 2; i++)
        {
            go = Instantiate(items[2], new Vector3(i, -7.0f, 0.0f), Quaternion.identity);
            baseWallList.Add(go);
        }
    }

    public void createBaseWall()
    {
        // destroy wall of base
        foreach (GameObject wall in MapCreator.Instance1.baseWallList)
        {
            Destroy(wall);
        }
        baseWallList.RemoveRange(0, 5);
        // instantiate wall of base
        GameObject go = Instantiate(items[1], new Vector3(-1.0f, -8.0f, 0.0f), Quaternion.identity);
        baseWallList.Add(go);
        go = Instantiate(items[1], new Vector3(1.0f, -8.0f, 0.0f), Quaternion.identity);
        baseWallList.Add(go);
        for (int i = -1; i < 2; i++)
        {
            go = Instantiate(items[1], new Vector3(i, -7.0f, 0.0f), Quaternion.identity);
            baseWallList.Add(go);
        }
    }
    

    private void createItem(GameObject createObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject item =  Instantiate(createObject, createPosition, createRotation);
        item.transform.SetParent(gameObject.transform);
        itemPosition.Add(createPosition);
    }

    // 不生成x=-10，10或y=-8，8
    private Vector3 createRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!itemPosition.Contains(createPosition))
            {
                return createPosition;
            }
        }
    }

    // 产生敌人方法
    private void createEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 enemyPos;
        if (num == 0)
        {
            enemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            enemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            enemyPos = new Vector3(10, 8, 0);
        }
        createItem(items[6], enemyPos, Quaternion.identity);
    } 
    
}

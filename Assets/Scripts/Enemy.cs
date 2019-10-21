using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 属性值
    public float moveSpeed = 3;

    public bool isRed;

    private bool isFreeze;

    public bool isBoss;

    private int lifeValue = 0;

    private float h = 0.0f;

    private float v = 0.0f;

    private Vector3 bulletEulerAngles;

    private float timeVal;

    private float timeValChangeDirection = 2.0f;

    // 引用
    private SpriteRenderer sr;

    public GameObject bulletPrefab;

    public GameObject explossionPrefab;

    public GameObject[] bonusPrefab;

    public Sprite[] tankSprite; // up right down left

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isBoss)
        {
            lifeValue += 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isFreeze)
        {
            return;
        }
        Move();
        // 无敌状态

        // 敌人攻击间隔
        if (timeVal >= 3.0f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    // 坦克的移动方法
    private void Move()
    {
        if (timeValChangeDirection >= 2.0f)
        {
            int num = Random.Range(0, 9);
            if (num > 5.0f)
            {
                v = -1.0f;
                h = 0.0f;
            }
            else if (num == 0.0f)
            {
                v = 1.0f;
                h = 0.0f;
            }
            else if (num <= 2.0f)
            {
                v = 0.0f;
                h = 1.0f;
            }
            else
            {
                v = 0.0f;
                h = -1.0f;
            }
            timeValChangeDirection = 0.0f;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        if (h > 0)
        {
            sr.sprite = tankSprite[lifeValue * 4 + 1];
            bulletEulerAngles.z = -90.0f;
        }
        else if (h < 0)
        {
            sr.sprite = tankSprite[lifeValue * 4 + 3];
            bulletEulerAngles.z = 90.0f;
        }

        if (h != 0)
        {
            return;
        }
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v > 0)
        {
            sr.sprite = tankSprite[lifeValue * 4];
            bulletEulerAngles.z = 0.0f;
        }
        else if (v < 0)
        {
            sr.sprite = tankSprite[lifeValue * 4 + 2];
            bulletEulerAngles.z = 180.0f;
        }
       
    }

    // 坦克攻击方法
    private void Attack()
    {
        GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        if (isBoss)
        {
            go.GetComponent<Bullet>().isAbleDestroyBarrier = true;
        }
        timeVal = 0.0f;
    }

    // 坦克死亡方法
    private void Die()
    {
        if (lifeValue <= 0)
        {
            MapCreator.Instance1.enemyList.Remove(gameObject);
            // 产生爆炸特效
            Instantiate(explossionPrefab, transform.position, transform.rotation);
            // 死亡
            Destroy(gameObject);
            if (isRed)
            {
                while (true)
                {
                    Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
                    if (!MapCreator.Instance1.itemPosition.Contains(createPosition))
                    {
                        // Instantiate(bonusPrefab[Random.Range(0,bonusPrefab.Length)], createPosition, Quaternion.identity);
                        Instantiate(bonusPrefab[Random.Range(0, bonusPrefab.Length)], createPosition, Quaternion.identity);
                        break;
                    }
                }

            }
        }
        else
        {
            lifeValue--;
        }
        
    }

    private void Boom()
    {
        // 产生爆炸特效
        Instantiate(explossionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
        if (isRed)
        {
            while (true)
            {
                Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
                if (!MapCreator.Instance1.itemPosition.Contains(createPosition))
                {
                    // Instantiate(bonusPrefab[Random.Range(0,bonusPrefab.Length)], createPosition, Quaternion.identity);
                    Instantiate(bonusPrefab[Random.Range(0, bonusPrefab.Length)], createPosition, Quaternion.identity);
                    break;
                }
            }

        }
    }
    // 敌方坦克碰撞时反向
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            v = -v;
            h = -h;
        }
    }

    public void Freeze()
    {
        isFreeze = true;
        Invoke("UnFreeze", 10.0f);
    }

    public void UnFreeze()
    {
        isFreeze = false;
    }
}

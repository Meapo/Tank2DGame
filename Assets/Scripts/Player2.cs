using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    // 属性值
    public float moveSpeed = 3;

    private int level = 0;

    public float v = 0.0f;

    public float h = 0.0f;

    private Vector3 bulletEulerAngles;

    private float timeVal;

    public float defendTimeVal = 3;

    private bool isDefended = true;

    // 引用
    private SpriteRenderer sr;

    public GameObject bulletPrefab;

    public GameObject explossionPrefab;

    public GameObject defendPrefab;

    public Sprite[] tankSprite; // up right down left

    public AudioSource moveAudio;

    public AudioClip[] tankAudio;
    

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 无敌状态
        if (defendTimeVal > 0)
        {
            isDefended = true;
            defendPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
        }
        else
        {
            isDefended = false;
            defendPrefab.SetActive(false);
        }

        // 攻击CD
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (PlayerManager2.Instance2.isDefeat)
        {
            return;
        }
        Move();

        playAudio();


    }

    // 坦克的移动方法
    private void Move()
    {
        h = Input.GetAxisRaw("Player2Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h > 0)
        {
            sr.sprite = tankSprite[level * 4 + 1];
            bulletEulerAngles.z = -90.0f;
        }
        else if (h < 0)
        {
            sr.sprite = tankSprite[level * 4 + 3];
            bulletEulerAngles.z = 90.0f;
        }

        if (h != 0)
        {
            return;
        }

        v = Input.GetAxisRaw("Player2Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v > 0)
        {
            sr.sprite = tankSprite[level * 4 + 0];
            bulletEulerAngles.z = 0.0f;
        }
        else if (v < 0)
        {
            sr.sprite = tankSprite[level * 4 + 2];
            bulletEulerAngles.z = 180.0f;
        }
    }

    // 坦克攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            if (level == 1)
            {
                go.GetComponent<Bullet>().bulletSpeed = go.GetComponent<Bullet>().bulletLv2Speed;
            }
            else if (level == 2)
            {
                go.GetComponent<Bullet>().bulletSpeed = go.GetComponent<Bullet>().bulletLv3Speed;
            }
            if (level == 3)
            {
                go.GetComponent<Bullet>().isAbleDestroyBarrier = true;
                go.GetComponent<Bullet>().bulletSpeed = go.GetComponent<Bullet>().bulletLv3Speed;
            }
            timeVal = 0.0f;
        }


    }

    // 坦克死亡方法
    private void Die()
    {
        if (!isDefended)
        {
            if (level <= 0)
            {
                // 死亡状态变为true
                PlayerManager2.Instance2.isPlayer2Dead = true;
                // 产生爆炸特效
                Instantiate(explossionPrefab, transform.position, transform.rotation);
                // 死亡
                Destroy(gameObject);
            }
            else
            {
                level--;
            }
            
        }
    }

    // 吃到无敌特效
    private void Defend()
    {
        defendTimeVal += 10.0f;
    }

    // 坦克音效
    private void playAudio()
    {
        if (v == 0 && h == 0)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    // 升级
    private void levelUp()
    {
        if (level < 3)
        {
            level++;
        }

    }
}

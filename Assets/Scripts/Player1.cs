using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    // 属性值
    public float moveSpeed = 3;

    private float v = 0.0f;

    private float h = 0.0f;

    private Vector3 bulletEulerAngles;

    private int level = 0;

    private float timeVal;

    private float defendTimeVal = 3;

    private bool isDefended = true;
    // 引用
    private Animator ani;

    public GameObject bulletPrefab;

    public GameObject explossionPrefab;

    public GameObject defendPrefab;

    // public Animator tankAni;   // up right down left

    public AudioSource moveAudio;

    public AudioClip[] tankAudio;

    private void Awake()
    {
        ani = this.GetComponent<Animator>();
        
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
        if (position.Instance.isSingleMode)
        {
            if (PlayerManger.Instance.isDefeat)
            {
                return ;
            }
        }
        else
        {
            if (PlayerManager2.Instance2.isDefeat)
            {
                return;
            }
        }
        
       
        
        Move();

        playAudio();
        
    }

    // 坦克的移动方法
    private void Move()
    {
        h = Input.GetAxisRaw("Player1Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h > 0)
        {
            ani.SetInteger("Horizontal", 1);
            bulletEulerAngles.z = -90.0f;
        }
        else if (h < 0)
        {
            ani.SetInteger("Horizontal", -1);
            bulletEulerAngles.z = 90.0f;
        }
        else
        {
            ani.SetInteger("Horizontal", 0);
        }
        if (h != 0)
        {
            return;
        }

        v = Input.GetAxisRaw("Player1Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v > 0)
        {
            ani.SetInteger("Vertical", 1);
            bulletEulerAngles.z = 0.0f;
        }
        else if (v < 0)
        {
            ani.SetInteger("Vertical", -1);
            bulletEulerAngles.z = 180.0f;
        }
        else
        {
            ani.SetInteger("Vertical", 0);
        }
    }

    // 坦克攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            go.GetComponent<Bullet>().isPlayer1Bullet = true;
            if (level == 1)
            {
                go.GetComponent<Bullet>().bulletSpeed = go.GetComponent<Bullet>().bulletLv2Speed;
            }
            else if (level == 2)
            {
                go.GetComponent<Bullet>().bulletSpeed = go.GetComponent<Bullet>().bulletLv3Speed;
            }
            else if (level == 3)
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
            if (level > 0)
            {
                level--;
                ani.SetInteger("Level", level);
            }
            else
            {
                // 死亡状态变为true
                if (position.Instance.isSingleMode)
                {
                    PlayerManger.Instance.isDead = true;
                }
                else
                {
                    PlayerManager2.Instance2.isPlayer1Dead = true;
                }
            
                // 产生爆炸特效
                Instantiate(explossionPrefab, transform.position, transform.rotation);
                // 死亡
                Destroy(gameObject);
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
            ani.SetInteger("Level", level);
        }
        
    }
}

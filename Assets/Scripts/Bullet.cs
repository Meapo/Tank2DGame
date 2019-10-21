using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 属性值
    public float bulletSpeed = 10.0f;

    public float bulletLv2Speed = 15.0f;

    public float bulletLv3Speed = 20.0f;

    public bool isPlayerBullet;

    public bool isPlayer1Bullet;

    public bool isAbleDestroyBarrier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        switch (collison.tag)
        {
            case ("Tank"):
                if (!isPlayerBullet)
                {
                    collison.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case ("Barrier"):
                collison.SendMessage("playAudio");
                if (isAbleDestroyBarrier)
                {
                    Destroy(collison.gameObject);
                }
                Destroy(gameObject);
                break;
            case ("AirBarrier"):
                Destroy(gameObject);
                break;
            case ("Wall"):
                Destroy(collison.gameObject);
                Destroy(gameObject);
                break;
            case ("Enemy"):
                if (isPlayerBullet)
                {
                    if (position.Instance.isSingleMode)
                    {
                        PlayerManger.Instance.playerScore++;
                        collison.SendMessage("Die");
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (isPlayer1Bullet)
                        {
                            PlayerManager2.Instance2.player1Score++;
                        }
                        else
                        {
                            PlayerManager2.Instance2.player2Score++;
                        }
                        collison.SendMessage("Die");
                        Destroy(gameObject);
                    }
                }
                break;
            case ("Base"):
                collison.SendMessage("Die");
                Destroy(gameObject);
                if (position.Instance.isSingleMode)
                {
                    PlayerManger.Instance.isDefeat = true;
                }
                else
                {
                    PlayerManager2.Instance2.isDefeat = true;
                }
                break;
            case ("Bullet"):
                Destroy(collison.gameObject);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    void setBulletSpeedLv2()
    {
        bulletSpeed = bulletLv2Speed;
    }

    void setBulletSpeedLv3()
    {
        bulletSpeed = bulletLv3Speed;
    }
}

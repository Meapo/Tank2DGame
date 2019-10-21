using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int type;

    public GameObject Barrier;

    public GameObject Wall;

    public AudioClip bonusAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playAudio()
    {
        AudioSource.PlayClipAtPoint(bonusAudio, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            playAudio();
            switch (type)
            {
                case (0):
                    if (position.Instance.isSingleMode)
                    {
                        PlayerManger.Instance.lifeValue++;
                    }
                    else
                    {
                        PlayerManager2.Instance2.player1LifeValue++;
                        PlayerManager2.Instance2.player2LifeValue++;
                    }
                    break;
                case (1):
                    foreach(GameObject go in MapCreator.Instance1.enemyList)
                    {
                        go.SendMessage("Freeze");
                    }
                    break;
                case (2):
                    MapCreator.Instance1.baseBarrierTime += 15.0f;
                    break;
                case (3):
                    foreach (GameObject item in MapCreator.Instance1.enemyList)
                    {
                        item.SendMessage("Boom");
                    }
                    MapCreator.Instance1.enemyList.Clear();
                    break;
                case (4):
                    collision.SendMessage("levelUp");
                    break;
                case (5):
                    collision.SendMessage("Defend");
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    
}

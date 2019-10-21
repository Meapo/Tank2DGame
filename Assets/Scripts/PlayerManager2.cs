using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager2 : MonoBehaviour
{
    // 属性值
    public int player1LifeValue = 3;

    public int player1Score = 0;

    public int player2LifeValue = 3;

    public int player2Score = 0;

    public bool isPlayer1Dead;

    public bool isPlayer2Dead;

    public bool isDefeat;
    
    // 引用
    public GameObject Born;

    public Text player1ScoreText;

    public Text player1LifeValueText;

    public Text player2ScoreText;

    public Text player2LifeValueText;

    public GameObject UIGameOver;
    // 单例
    private static PlayerManager2 instance2;

    public static PlayerManager2 Instance2
    {
        get
        {
            return instance2;
        }

        set
        {
            instance2 = value;
        }
    }

    private void Awake()
    {
        Instance2 = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            UIGameOver.SetActive(true);
            Invoke("ReturnToMenu", 5.0f);
            return;
        }
        if (isPlayer1Dead)
        {
            Recover1();
        }
        if (isPlayer2Dead)
        {
            Recover2();
        }
        if (player1LifeValue <= 0 && player2LifeValue <= 0)
        {
            isDefeat = true;
        }
        player1ScoreText.text = player1Score.ToString();
        player1LifeValueText.text = player1LifeValue.ToString();

        player2ScoreText.text = player2Score.ToString();
        player2LifeValueText.text = player2LifeValue.ToString();
    }

    // 复活
    private void Recover1()
    {
        if (player1LifeValue > 0)
        {
            player1LifeValue--;
            Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            isPlayer1Dead = false;
        }
    }
    private void Recover2()
    {
        if (player2LifeValue > 0)
        {
            player2LifeValue--;
            GameObject go = Instantiate(Born, new Vector3(2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().isPlayer1 = false;
            isPlayer2Dead = false;
        }
    }
    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Start");
    }
}

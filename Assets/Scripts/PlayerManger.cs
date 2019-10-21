using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManger : MonoBehaviour
{
    // 属性值
    public int lifeValue = 3;

    public int playerScore = 0;

    public bool isDead;

    public bool isDefeat;
    // 引用
    public GameObject Born;

    public Text playerScoreText;

    public Text playerLifeValueText;

    public GameObject UIGameOver;
    // 单例
    private static PlayerManger instance;

    public static PlayerManger Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
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
        if (isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();
    }

    // 复活
    private void Recover()
    {
        if (lifeValue <= 0)
        {
            isDefeat = true;  // 游戏失败，返回界面
        }
        else
        {
            lifeValue--;
            Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            isDead = false;
        }
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Start");
    }
}

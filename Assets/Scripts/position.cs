using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class position : MonoBehaviour
{
    public bool isSingleMode;

    private int choice = 1;

    public Transform pos1;

    public Transform pos2;

    static position instance;

    public static position Instance
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

    void Awake()
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
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            choice = 1;
            transform.position = pos1.position;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            choice = 2;
            transform.position = pos2.position;
        }

        if (choice==1&&Input.GetKeyDown(KeyCode.Space))
        {
            isSingleMode = true;
            SceneManager.LoadScene("game");
        }
        else if (choice == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            isSingleMode = false;
            SceneManager.LoadScene("game2");
        }
    }
}

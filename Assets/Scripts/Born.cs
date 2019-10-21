using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject player1Prefab;

    public GameObject player2Prefab;

    public bool isPlayer1;
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
        if (isPlayer1)
        {
            Instantiate(player1Prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(player2Prefab, transform.position, Quaternion.identity);
        }
        
    }
}

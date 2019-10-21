using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    private SpriteRenderer sr;

    public GameObject explossionPrefab;

    public Sprite gameOver;

    public GameObject UIGameOver;

    public AudioClip dieAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        Instantiate(explossionPrefab, transform.position, transform.rotation);
        sr.sprite = gameOver;
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Start");
    }
}

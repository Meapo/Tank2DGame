using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explossion : MonoBehaviour
{
    public AudioClip exploseAudio;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(exploseAudio, transform.position);
        Destroy(gameObject, 0.334f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

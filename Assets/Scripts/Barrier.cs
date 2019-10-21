using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public AudioClip hitAudio;

    private void playAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}

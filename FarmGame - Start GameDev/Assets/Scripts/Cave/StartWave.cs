using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public GameObject dungeonGate;
    public GameObject waveSpawner;
    public AudioClip clipBGM;
    public AudioClip clipSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.instance.playBGM(clipBGM);
            AudioManager.instance.playOneShotSound(clipSFX);
            dungeonGate.SetActive(true);
            waveSpawner.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

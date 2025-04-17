using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public GameObject dungeonGate;
    public GameObject waveSpawner;
    public AudioClip clipBGM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.instance.playBGM(clipBGM);
            dungeonGate.SetActive(true);
            waveSpawner.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSceneControl : MonoBehaviour
{
    [SerializeField] private AudioClip bgmMusic; // Essa é a música que irá tocar quando a cena mudar (pode ser uma música diferente da que estava tocando)
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.playBGM(bgmMusic);
    }
}

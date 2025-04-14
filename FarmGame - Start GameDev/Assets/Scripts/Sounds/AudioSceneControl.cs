using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSceneControl : MonoBehaviour
{
    [SerializeField] private AudioClip bgmMusic; // Essa � a m�sica que ir� tocar quando a cena mudar (pode ser uma m�sica diferente da que estava tocando)

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.playBGM(bgmMusic);
    }
}

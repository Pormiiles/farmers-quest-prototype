using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton para manter o objeto do AudioController na cena 
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance); // Destory a instância se existir outra - evita duplicidade
        }
    }

    public void playBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

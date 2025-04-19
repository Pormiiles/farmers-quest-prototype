using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton para manter o objeto do AudioController na cena 
    public static AudioManager instance { get; private set; }
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject); // Destrói a instância se existir outra - evita duplicidade
        }
    }

    public void playBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void playOneShotSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

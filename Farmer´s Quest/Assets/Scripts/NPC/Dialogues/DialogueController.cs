using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public enum LanguagesEnum
    {
        PT,
        ENG,
        SPA
    }

    [Header("Components")]
    public GameObject dialogueWindowObj;
    public Image profileSprite;
    public Text speechText;
    public Text actorNameText;

    [Header("Dialogue Settings")]
    public float typingSpeed;

    private bool isWindowBeingShowed;
    private int index;

    private List<string> sentences;
    private List<string> actorNames;
    private List<Sprite> actorSprites;

    public LanguagesEnum language;

    [Header("Audio")]
    public AudioSource voiceSource; // arraste um AudioSource no inspector
    public float voicePitchVariation = 0.05f; // variação para parecer mais "vivo"
    private List<AudioClip> currentVoices = new List<AudioClip>();


    public static DialogueController instance;

    public bool IsWindowBeingShowed { get => isWindowBeingShowed; set => isWindowBeingShowed = value; }

    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        actorNameText.text = actorNames[index];
        profileSprite.sprite = actorSprites[index];
        speechText.text = "";

        // Obtemos o som de voz da NPC atual
        AudioClip currentVoice = currentVoices[index];

        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;

            if (!char.IsWhiteSpace(letter) && currentVoice != null)
            {
                voiceSource.pitch = 1f + Random.Range(-voicePitchVariation, voicePitchVariation);
                voiceSource.PlayOneShot(currentVoice);
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }


    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Count - 1)
            {
                index++;
                StartCoroutine(TypeSentence());
            }
            else
            {
                index = 0;
                dialogueWindowObj.SetActive(false);
                IsWindowBeingShowed = false;
            }
        }
    }

    public void Speech(List<string> lines, List<string> names, List<Sprite> sprites, List<AudioClip> voiceClips)
    {
        if (!IsWindowBeingShowed)
        {
            dialogueWindowObj.SetActive(true);
            sentences = lines;
            actorNames = names;
            actorSprites = sprites;
            currentVoices = voiceClips;

            index = 0;
            StartCoroutine(TypeSentence());
            IsWindowBeingShowed = true;
        }
    }

    public void closeWindow()
    {
        StopAllCoroutines(); // Para a execução da corrotina TypeSentence
        dialogueWindowObj.SetActive(false);
        isWindowBeingShowed = false;
        index = 0;
        speechText.text = ""; 
    }
}

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
    public GameObject dialogueWindowObj; // Janela do di�logo
    public Image profileSprite; // Sprite do perfil
    public Text speechText; // Texto da fala
    public Text actorNameText; // Nome do NPC

    [Header("Dialogue Settings")]
    public float typingSpeed; // Velocidade do texto da fala

    // Atributos privados da classe
    private bool isWindowBeingShowed; // Verifica se a janela est� vis�vel
    private int index; // Ind�ce das falas/textos
    private string[] sentences;

    public LanguagesEnum language;

    public static DialogueController instance;

    public bool IsWindowBeingShowed { get => isWindowBeingShowed; set => isWindowBeingShowed = value; }

    // Awake � chamado antes do Start 
    private void Awake()
    {
        instance = this; // Inicializa a inst�ncia dessa mesma classe - Criando um Singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Quebra o texto da fala/di�logo de letra em letra 
    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Pula para a pr�xima fala/di�logo
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // Quando as falas terminam
            {
                speechText.text = "";
                index = 0;
                dialogueWindowObj.SetActive(false);
                IsWindowBeingShowed = false;
            }
        }
    }

    // Chamar� a fala do NPC
    public void Speech(string[] text)
    {
        if(!IsWindowBeingShowed)
        {
            dialogueWindowObj.SetActive(true);
            sentences = text;
            StartCoroutine(TypeSentence());
            IsWindowBeingShowed = true;
        }
    }
}

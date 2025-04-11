using System.Collections.Generic;
using UnityEngine;

public enum EstadoLayla
{
    AntesDaCaverna,
    DepoisDaCaverna,
    DepoisDeConstruirCasa
}

public class LaylaDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    [Header("Referência para cada estado da Layla")]
    public DialoguesSettings antesDaCaverna;
    public DialoguesSettings depoisDaCaverna;
    public DialoguesSettings depoisDeConstruirCasa;

    bool playerHit;
    private List<string> sentences = new List<string>();

    // Novo: armazena o estado anterior para detectar mudanças
    private EstadoLayla estadoAnterior;

    void Start()
    {
        estadoAnterior = GameManager.instance.estadoLayla;
        GetSpeechText();
    }

    void Update()
    {
        // Debug para acompanhar o estado da Layla
        Debug.Log("Estado da Layla: " + GameManager.instance.estadoLayla);

        // Atualiza os diálogos se o estado mudou
        if (GameManager.instance.estadoLayla != estadoAnterior)
        {
            estadoAnterior = GameManager.instance.estadoLayla;
            GetSpeechText();
        }

        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueController.instance.Speech(sentences.ToArray());
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetSpeechText()
    {
        sentences.Clear(); // Limpa antes de carregar

        DialoguesSettings dialogoAtual = antesDaCaverna;

        switch (GameManager.instance.estadoLayla)
        {
            case EstadoLayla.DepoisDaCaverna:
                dialogoAtual = depoisDaCaverna;
                break;

            case EstadoLayla.DepoisDeConstruirCasa:
                dialogoAtual = depoisDeConstruirCasa;
                break;
        }

        for (int i = 0; i < dialogoAtual.dialogues.Count; i++)
        {
            switch (DialogueController.instance.language)
            {
                case DialogueController.LanguagesEnum.PT:
                    sentences.Add(dialogoAtual.dialogues[i].sentence.portuguese);
                    break;

                case DialogueController.LanguagesEnum.ENG:
                    sentences.Add(dialogoAtual.dialogues[i].sentence.english);
                    break;

                case DialogueController.LanguagesEnum.SPA:
                    sentences.Add(dialogoAtual.dialogues[i].sentence.spanish);
                    break;
            }
        }
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        playerHit = hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}

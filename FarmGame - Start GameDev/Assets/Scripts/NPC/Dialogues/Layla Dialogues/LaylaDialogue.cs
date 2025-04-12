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

    private bool playerHit;
    private Player player;

    private List<string> sentences = new List<string>();
    private List<string> actorNames = new List<string>();
    private List<Sprite> actorSprites = new List<Sprite>();

    private EstadoLayla estadoAnterior;

    void Start()
    {
        estadoAnterior = GameManager.instance.estadoLayla;
        GetSpeechData();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (GameManager.instance.estadoLayla != estadoAnterior)
        {
            estadoAnterior = GameManager.instance.estadoLayla;
            GetSpeechData();
        }

        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueController.instance.Speech(sentences, actorNames, actorSprites);
            player.IsPlayerSpeedPaused = true;
            StartCoroutine(WaitForDialogueToFinish());
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetSpeechData()
    {
        sentences.Clear();
        actorNames.Clear();
        actorSprites.Clear();

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

        foreach (Sentences s in dialogoAtual.dialogues)
        {
            switch (DialogueController.instance.language)
            {
                case DialogueController.LanguagesEnum.PT:
                    sentences.Add(s.sentence.portuguese);
                    break;
                case DialogueController.LanguagesEnum.ENG:
                    sentences.Add(s.sentence.english);
                    break;
                case DialogueController.LanguagesEnum.SPA:
                    sentences.Add(s.sentence.spanish);
                    break;
            }

            actorNames.Add(s.actorName);
            actorSprites.Add(s.profile);
        }
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        playerHit = hit != null;
    }

    System.Collections.IEnumerator WaitForDialogueToFinish()
    {
        // espera enquanto o diálogo estiver ativo
        while (DialogueController.instance.dialogueWindowObj.activeSelf)
        {
            yield return null;
        }

        // libera o movimento quando o painel sumir
        player.IsPlayerSpeedPaused = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
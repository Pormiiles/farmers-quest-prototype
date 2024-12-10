using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaylaDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public DialoguesSettings dialogue;

    bool playerHit;

    private List<string> sentences = new List<string>(); 

    // Start is called before the first frame update
    void Start()
    {
        GetSpeechText();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueController.instance.Speech(sentences.ToArray());
        }    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetSpeechText()
    {
        for(int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch(DialogueController.instance.language)
            {
                case DialogueController.LanguagesEnum.PT:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;

                case DialogueController.LanguagesEnum.ENG:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;

                case DialogueController.LanguagesEnum.SPA:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }
            
        }
    }

    // Quando o player encostar perto do NPC, a janela de diálogo surgirá na tela
    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if(hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueTrigger : MonoBehaviour
{
    public DialoguesSettings dialogueData;

    private void Start()
    {
        StartCoroutine(TriggerDialogue());
    }

    IEnumerator TriggerDialogue()
    {
        // Pega as falas de acordo com o idioma selecionado
        string[] lines = GetDialogueLines(dialogueData);

        // Inicia o diálogo
        DialogueController.instance.Speech(lines);

        // Espera o diálogo terminar
        yield return new WaitUntil(() => DialogueController.instance.IsWindowBeingShowed == false);

        // Aqui você pode colocar o que quiser que aconteça depois do diálogo (ativar inimigos, abrir portões, etc)
    }

    string[] GetDialogueLines(DialoguesSettings data)
    {
        List<string> lines = new List<string>();
        foreach (var s in data.dialogues)
        {
            switch (DialogueController.instance.language)
            {
                case DialogueController.LanguagesEnum.ENG:
                    lines.Add(s.sentence.english);
                    break;
                case DialogueController.LanguagesEnum.SPA:
                    lines.Add(s.sentence.spanish);
                    break;
                default:
                    lines.Add(s.sentence.portuguese);
                    break;
            }
        }
        return lines.ToArray();
    }
}

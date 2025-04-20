using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialoguesSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>(); // Vetor que vai armazenar um conjunto de sentenças/diálogos 
}

[System.Serializable] // Permitir que as classes apareçam no Inspector do ScriptableObject
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
    [Header("Voice Settings")]
    public AudioClip voiceClip;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialoguesSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialoguesSettings dialSet = (DialoguesSettings)target;

        Languages lang = new Languages();
        lang.portuguese = dialSet.sentence;

        Sentences sent = new Sentences();
        sent.profile = dialSet.speakerSprite;
        sent.sentence = lang;

        if(GUILayout.Button("Create New Dialogue"))
        {
            if(dialSet.sentence != "")
            {
                dialSet.dialogues.Add(sent);

                dialSet.speakerSprite = null;
                dialSet.sentence = "";
            }
        }
    }
}
#endif
using System.Collections;
using System.Collections.Generic;
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
}

public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

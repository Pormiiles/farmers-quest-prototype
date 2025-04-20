using UnityEngine;
using UnityEngine.Playables;

public class CutsceneCameraTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false; // Desativa trigger após ativar a cutscene
        }
    }
}

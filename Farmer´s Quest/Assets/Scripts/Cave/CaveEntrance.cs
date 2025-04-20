using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveEntrance : MonoBehaviour
{
    private PlayerItems playerItems;

    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Verifica se o jogador possui os itens necessários
            if (playerItems.fishTotal >= 5 && playerItems.seedCarrotTotal >= 10)
            {
                playerItems.fishTotal -= 5;
                playerItems.seedCarrotTotal -= 10;

                SceneManager.LoadScene("Cave");
            }
        }
    }
}

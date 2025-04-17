using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveEntrance : MonoBehaviour
{
    private PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerItems.fishTotal >= 5 && playerItems.seedCarrotTotal >= 10) // Verifica se o player tem 5 peixes e 10 cenouras para poder entrar na caverna
            {
                // Remove a quantidade de itens do inventário do jogador (caso haja sistema de save quando o player voltar a fazenda)
                playerItems.fishTotal -= 5;
                playerItems.seedCarrotTotal -= 10;

                // Carrega a cena da caverna
                SceneManager.LoadScene("Cave");
            }
        }
    }
}

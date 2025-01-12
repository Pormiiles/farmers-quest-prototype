using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
        onRun();
    }

    // Separando o conteúdo em áreas
    #region Movement 

    void onMove()
    {
        if (player.playerDirection.sqrMagnitude > 0) // Se o player estiver se movimentando
        {
            if (player.isPlayerRolling)
            {
                anim.SetTrigger("isRolling");
            } else
            {
                anim.SetInteger("transition", 1);
            }  
        }
        else
        {
            anim.SetInteger("transition", 0); // Se não estiver andando, ele entra na animação de Idle
        }

        // Verifica a direção horizontal
        if (player.playerDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Direita
        }
        else if (player.playerDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Esquerda (espelha no eixo X - o sprite é invertido)
        }

        if(player.isPlayerCutting)
        {
            anim.SetInteger("transition", 3);
        }

        if (player.isPlayerDigging)
        {
            anim.SetInteger("transition", 4);
        }

        if (player.IsPlayerWatering)
        {
            anim.SetInteger("transition", 5);
        }
    }

    void onRun()
    {
        if(player.isPlayerRunning)
        {
            anim.SetInteger("transition", 2);
        }
    }

    #endregion
}

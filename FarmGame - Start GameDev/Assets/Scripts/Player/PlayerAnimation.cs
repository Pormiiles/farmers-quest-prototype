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
            anim.SetInteger("transition", 0); // Se n�o estiver andando, ele entra na anima��o de Idle
        }

        // Verifica a dire��o horizontal
        if (player.playerDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Direita
        }
        else if (player.playerDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Esquerda (espelha no eixo X - o sprite � invertido)
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

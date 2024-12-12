using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    [SerializeField] private int treeLife;           // Vida da �rvore
    [SerializeField] private Animator anim;         // Refer�ncia ao Animator
    [SerializeField] private GameObject woodPrefab; // Prefab da madeira a ser dropada
    [SerializeField] private Transform baseLog;     // Refer�ncia ao filho Base Log

    private bool isTreeCut;

    // M�todo chamado quando o Player atinge a �rvore
    public void OnHit()
    {
        treeLife--;
        anim.SetTrigger("cuttingTree");

        if (treeLife <= 0)
        {
            // Usa o centro do Base Log para instanciar o WoodItem
            Vector3 baseLogCenter = GetBaseLogCenter();

            // Instancia a madeira na posi��o correta
            GameObject wood = Instantiate(
                woodPrefab,
                baseLogCenter, // Agora instanciamos no centro do Base Log
                Quaternion.identity
            );

            // Define a dire��o de movimento para a madeira
            Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
            wood.GetComponent<WoodItem>().SetMoveDirection(moveDirection);

            // Ativa a anima��o da �rvore sendo cortada
            anim.SetTrigger("isTreeCut");

            isTreeCut = true;
        }
    }

    // Obt�m o centro do Base Log com base no Renderer
    private Vector3 GetBaseLogCenter()
    {
        if (baseLog != null)
        {
            Renderer baseLogRenderer = baseLog.GetComponent<Renderer>();

            if (baseLogRenderer != null)
            {
                return baseLogRenderer.bounds.center; // Centro do sprite do Base Log
            }

            return baseLog.position; // Fallback para a posi��o do Base Log
        }

        Debug.LogError("Base Log n�o foi atribu�do no inspector!");
        return transform.position; // Caso o Base Log n�o esteja definido
    }

    // Detecta colis�o com o machado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isTreeCut)
        {
            OnHit();
        }
    }
}

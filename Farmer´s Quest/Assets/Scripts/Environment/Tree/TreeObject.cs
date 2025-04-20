using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    [SerializeField] private int treeLife;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private Transform baseLog;
    [SerializeField] private ParticleSystem leavesParticles;

    private bool isTreeCut;

    public void OnHit()
    {
        treeLife--;
        anim.SetTrigger("cuttingTree");
        leavesParticles.Play();

        if (treeLife <= 0)
        {
            Vector3 baseLogCenter = GetBaseLogCenter();

            GameObject wood = Instantiate(
                woodPrefab,
                baseLogCenter,
                Quaternion.identity
            );

            Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
            wood.GetComponent<WoodItem>().SetMoveDirection(moveDirection);

            anim.SetTrigger("isTreeCut");
            isTreeCut = true;
        }
    }

    private Vector3 GetBaseLogCenter()
    {
        if (baseLog != null)
        {
            Renderer baseLogRenderer = baseLog.GetComponent<Renderer>();

            if (baseLogRenderer != null)
                return baseLogRenderer.bounds.center;

            return baseLog.position;
        }

        Debug.LogError("Base Log não foi atribuído no inspector!");
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isTreeCut)
        {
            OnHit();
        }
    }
}

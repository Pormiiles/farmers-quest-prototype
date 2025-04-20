using System;
using UnityEngine;

public class Casting : MonoBehaviour
{
    private bool playerDetected;
    private PlayerItems playerItems;
    private PlayerAnimation playerAnim;

    [SerializeField] private int castingPercentage;
    public GameObject fishPrefab;
    public GameObject keyAdvice;

    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        playerAnim = playerItems.GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.onCastingStart();
        }
    }

    public void onCasting()
    {
        int chanceValueRandom = UnityEngine.Random.Range(1, 100);

        if (chanceValueRandom <= castingPercentage)
        {
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-2f, -1f), 0f, 0f);
            Instantiate(fishPrefab, playerItems.transform.position + spawnOffset, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            keyAdvice.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            keyAdvice.SetActive(false);
        }
    }
}

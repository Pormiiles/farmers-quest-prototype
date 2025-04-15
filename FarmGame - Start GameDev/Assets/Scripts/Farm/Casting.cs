using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    private bool playerDetected;
    private PlayerItems playerItems;
    private PlayerAnimation playerAnim;
    [SerializeField] private int castingPercentage; // Chance de pescar um peixe
    public GameObject fishPrefab;
    public GameObject keyAdvice;

    // Start is called before the first frame update
    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        playerAnim = playerItems.GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
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

        if(chanceValueRandom <= castingPercentage)
        {
            Instantiate(fishPrefab, playerItems.transform.position + new Vector3(UnityEngine.Random.Range(-2f, -1f), -0f, 0f), Quaternion.identity);
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

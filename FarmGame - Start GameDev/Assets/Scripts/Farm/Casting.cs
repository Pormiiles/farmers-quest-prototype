using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    private Boolean playerDetected;
    private PlayerItems player;
    [SerializeField] private int castingPercentage; // Chance de pescar um peixe

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            onCasting();
        }
    }

    void onCasting()
    {
        int chanceValueRandom = UnityEngine.Random.Range(1, 100);

        if(chanceValueRandom <= castingPercentage)
        {
            Debug.Log("pescou!");
        } else
        {
            Debug.Log("não pescou!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}

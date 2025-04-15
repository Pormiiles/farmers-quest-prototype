using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Boolean playerDetected;
    private PlayerItems player;
    [SerializeField] private float waterValue;
    public GameObject keyAdvice;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected && Input.GetKeyDown(KeyCode.E)) 
        {
            player.WaterLimit(waterValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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

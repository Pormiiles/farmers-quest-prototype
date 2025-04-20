using System;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private bool playerDetected;
    private PlayerItems player;
    [SerializeField] private float waterValue;
    public GameObject keyAdvice;

    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
    }

    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            player.WaterLimit(waterValue);
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

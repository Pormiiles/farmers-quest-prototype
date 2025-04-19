using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Limits")]
    private float woodLimit = 5f; // Limite de quantidade de madeira
    private float waterLimit = 50f; // Limite de quantidade de água
    private float seedCarrotsLimit = 10f; // Limite de quantidade de sementes de cenoura
    private float fishLimit = 5f;

    [Header("Total Amount")]
    [SerializeField] public int woodTotal; // Valor atual da quantidade de madeira
    [SerializeField] public float waterTotal; // Valor atual da quantidade de água do jogador
    [SerializeField] public int seedCarrotTotal; // Valor atual da quantidade de sementes de cenoura
    [SerializeField] public int fishTotal; // Valor atual da quantidade de peixes

    public float WoodLimit { get => woodLimit; set => woodLimit = value; }
    public float WaterLimit1 { get => waterLimit; set => waterLimit = value; }
    public float SeedCarrotsLimit { get => seedCarrotsLimit; set => seedCarrotsLimit = value; }
    public float FishLimit { get => fishLimit; set => fishLimit = value; }

    public void WaterLimit(float waterValue)
    {
        if(waterTotal < WaterLimit1)
        {
            waterTotal += waterValue;
        }
    }
}

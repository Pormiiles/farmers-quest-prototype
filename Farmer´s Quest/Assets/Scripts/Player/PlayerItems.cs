using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Limits")]
    private float woodLimit = 5f;
    private float waterLimit = 50f;
    private float seedCarrotsLimit = 10f;
    private float fishLimit = 5f;

    [Header("Total Amount")]
    [SerializeField] public int woodTotal;
    [SerializeField] public float waterTotal;
    [SerializeField] public int seedCarrotTotal;
    [SerializeField] public int fishTotal;

    public float WoodLimit { get => woodLimit; set => woodLimit = value; }
    public float WaterLimit1 { get => waterLimit; set => waterLimit = value; }
    public float SeedCarrotsLimit { get => seedCarrotsLimit; set => seedCarrotsLimit = value; }
    public float FishLimit { get => fishLimit; set => fishLimit = value; }

    public void WaterLimit(float waterValue)
    {
        if (waterTotal < WaterLimit1)
        {
            waterTotal += waterValue;
        }
    }
}

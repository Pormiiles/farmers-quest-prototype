using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image waterBarUI;
    [SerializeField] private Text woodNumberText;
    [SerializeField] private Text carrotNumberText;
    [SerializeField] private Text fishNumberText;

    [SerializeField] private List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectedToolColor;
    [SerializeField] private Color uncheckedToolColor;

    private Player player;
    private PlayerItems playerItems;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>(); 
        player = playerItems.GetComponent<Player>(); // usando a vaiável playerItems para obter o componente Player 
    }

    // Start is called before the first frame update
    void Start()
    {
        waterBarUI.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Atualiza as barras de quantidade na UI de itens coletáveis
        waterBarUI.fillAmount = playerItems.waterTotal / playerItems.WaterLimit1;
        woodNumberText.text = playerItems.woodTotal.ToString();
        carrotNumberText.text = playerItems.seedCarrotTotal.ToString();
        fishNumberText.text = playerItems.fishTotal.ToString();

        // Atualiza o valor Alpha da ferramenta que foi selecionado ou não na UI do inventário
        for (int i = 0; i < toolsUI.Count; i++)
        {
            if(i == player.HandlingTool - 1)
            {
                toolsUI[i].color = selectedToolColor;
            } else
            {
                toolsUI[i].color = uncheckedToolColor;
            }
        }
    }
}

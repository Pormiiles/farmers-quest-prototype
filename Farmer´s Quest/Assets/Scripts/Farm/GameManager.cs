using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EstadoLayla estadoLayla = EstadoLayla.AntesDaCaverna;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Impede que a nova instância sobrescreva
        }
    }
}
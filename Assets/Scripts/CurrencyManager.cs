using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int totalCoins = 0; // Total de moedas do jogador
    public TextMeshProUGUI coinText; // Exibição das moedas

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateUI();
        Debug.Log($"Moedas atuais: {totalCoins}");
    }

    public bool SpendCoins(int amount)
    {
        Debug.Log("Tentando gastar " + amount + " moedas");
        
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            UpdateUI();
            return true; // Compra realizada com sucesso
        }
        return false; // Moedas insuficientes
    }

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = totalCoins.ToString();
    }
}

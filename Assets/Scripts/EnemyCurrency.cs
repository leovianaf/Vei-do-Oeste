using UnityEngine;

public class EnemyCurrency : MonoBehaviour
{
    [Header("Configuração de Moedas")]
    public int minCoinsDrop = 1;
    public int maxCoinsDrop = 5;

    public void DropCoins()
    {
        if (CurrencyManager.instance != null)
        {
            int coinsDropped = Random.Range(minCoinsDrop, maxCoinsDrop + 1);
            CurrencyManager.instance.AddCoins(coinsDropped);
            Debug.Log($"Inimigo {gameObject.name} dropou {coinsDropped} moedas!");
        }
        else
        {
            Debug.LogError("CurrencyManager não foi encontrado na cena!");
        }
    }
}

using UnityEngine;

public class InteractiveTilesShop : MonoBehaviour
{
    public GameObject shopPanel;  // O painel da loja
    private ShopManager shopManager;  // Referência ao ShopManager

    private void Start()
    {
        // Obtém a referência do ShopManager para chamar ToggleShop()
        shopManager = ShopManager.instance;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.I))
        {
            // Verifica se o ShopManager está ativo e se o painel da loja foi atribuído
            if (shopManager != null)
            {
                Debug.Log("Abrindo a loja...");
                shopManager.ToggleShop();  // Chama a função ToggleShop do ShopManager para abrir a loja
            }
            else
            {
                Debug.LogError("ShopManager não encontrado!");
            }
        }
    }
}
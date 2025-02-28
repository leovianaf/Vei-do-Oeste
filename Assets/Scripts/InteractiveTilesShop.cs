using UnityEngine;

public class InteractiveTilesShop : MonoBehaviour
{
    public GameObject shopPanel;  // O painel da loja
    private ShopManager shopManager;  // Referência ao ShopManager
    public GameObject interactionText;

    private bool isPlayerInRange = false;

    private void Start()
    {
        // Obtém a referência do ShopManager para chamar ToggleShop()
        shopManager = ShopManager.instance;
        interactionText.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.C))
        {
            if (shopManager != null)
            {
                Debug.Log("Abrindo a loja de upgrades...");
                shopManager.ToggleShop();  // Chama a função ToggleShop do UpgradeShopManager para abrir o painel
            }
            else
            {
                Debug.LogError("UpgradeShopManager não encontrado!");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionText.SetActive(true);

            /* if(Input.GetKeyDown(KeyCode.C)){

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
            } */
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
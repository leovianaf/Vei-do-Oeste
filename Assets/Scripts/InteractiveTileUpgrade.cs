using UnityEngine;

public class InteractiveTileUpgrade : MonoBehaviour
{
    public GameObject upgradePanel;  // O painel da loja de upgrades
    private UpgradeShopManager upgradeShopManager;  // Referência ao UpgradeShopManager
    public GameObject interactionText;

    private bool isPlayerInRange = false;

    private void Start()
    {
        // Obtém a referência do UpgradeShopManager para chamar ToggleShop()
        upgradeShopManager = UpgradeShopManager.instance;
        interactionText.SetActive(false);
    }

     void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.U))
        {
            if (upgradeShopManager != null)
            {
                Debug.Log("Abrindo a loja de upgrades...");
                upgradeShopManager.ToggleShop();  // Chama a função ToggleShop do UpgradeShopManager para abrir o painel
            }
            else
            {
                Debug.LogError("UpgradeShopManager não encontrado!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(true);
            isPlayerInRange = true;
            // Verifica se o UpgradeShopManager está ativo e se o painel de upgrades foi atribuído
/*             if (upgradeShopManager != null)
            {
                Debug.Log("Abrindo a loja de upgrades...");
                upgradeShopManager.ToggleShop();  // Chama a função ToggleShop do UpgradeShopManager para abrir o painel
            }
            else
            {
                Debug.LogError("UpgradeShopManager não encontrado!");
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
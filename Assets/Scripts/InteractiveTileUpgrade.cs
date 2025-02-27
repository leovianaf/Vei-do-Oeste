using UnityEngine;

public class InteractiveTileUpgrade : MonoBehaviour
{
    public GameObject upgradePanel;  // O painel da loja de upgrades
    private UpgradeShopManager upgradeShopManager;  // Referência ao UpgradeShopManager

    private void Start()
    {
        // Obtém a referência do UpgradeShopManager para chamar ToggleShop()
        upgradeShopManager = UpgradeShopManager.instance;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.U))
        {
            // Verifica se o UpgradeShopManager está ativo e se o painel de upgrades foi atribuído
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
}
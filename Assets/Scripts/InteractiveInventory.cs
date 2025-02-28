using UnityEngine;

public class InteractiveInventory : MonoBehaviour
{
    private InventoryManager inventoryManager;  // Referência ao UpgradeShopManager
    public GameObject interactionText;

    private bool isPlayerInRange = false;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        interactionText.SetActive(false);
    }

     void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.ToggleInventory();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(true);
            isPlayerInRange = true;

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

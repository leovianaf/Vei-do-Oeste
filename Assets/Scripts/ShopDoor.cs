using TMPro;
using UnityEngine;

public class ShopDoor : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.E;
    public GameObject interactionText;
    private bool isPlayerInRange = false;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        interactionText.SetActive(false);

    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            gameManager.LoadRandomMap();
            gameManager.LoadUI();
            gameManager.isInShop = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionText.SetActive(false);
        }
    }

}

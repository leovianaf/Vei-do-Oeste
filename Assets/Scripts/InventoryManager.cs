using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public static bool IsInventoryOpen { get; private set; }
    public GameObject inventoryPanel;
    public Transform weaponsContainer;
    public GameObject weaponSlotPrefab;
    public TMP_Text inventoryMessageText;
    public Weapon defaultWeapon;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }           
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerWeapon.instance != null)
        {
            AddWeapon(defaultWeapon);
            PlayerWeapon.instance.EquipWeapon(defaultWeapon);
        }
        else
        {
            Debug.LogError("PlayerWeapon instance não encontrada ou nenhuma arma definida!");
        }
        inventoryMessageText.text = "";
    }

    void Update()
    {
/*         if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I pressionado!"); // Adicione esta linha
            ToggleInventory();
        } */
    }

    public void AddWeapon(Weapon weapon)
    {
        GameObject slot = Instantiate(weaponSlotPrefab, weaponsContainer);
        slot.GetComponent<InventoryWeaponSlot>().Initialize(weapon);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        IsInventoryOpen = inventoryPanel.activeSelf;
        Time.timeScale = IsInventoryOpen ? 0 : 1; 
        inventoryMessageText.text = "";

        Debug.Log("Inventário ativo: " + inventoryPanel.activeSelf);
    }

    public void EquipWeapon(Weapon weapon)
    {
        PlayerWeapon.instance.EquipWeapon(weapon);
        ShowEquipMessage("Arma equipada!", "#88FFA9");
    }

    void ShowEquipMessage(string message, string hexColor)
    {
        inventoryMessageText.text = message;
        
        if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            inventoryMessageText.color = color;
        }
        else
        {
            Debug.LogWarning("Cor inválida: " + hexColor);
        }

        StopAllCoroutines();
        StartCoroutine(ClearMessageAfterDelay());
    }

    IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f); // Usa tempo real para funcionar mesmo com Time.timeScale = 0
        inventoryMessageText.text = "";
    }

}
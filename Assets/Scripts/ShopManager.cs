using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public static bool IsShopOpen { get; private set; }
    public GameObject shopPanel;
    public Transform weaponsContainer;
    public GameObject weaponButtonPrefab;
    public TMP_Text shopMessageText;
    public Weapon[] availableWeapons;

    private Dictionary<Weapon, GameObject> weaponButtons = new Dictionary<Weapon, GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        PopulateShop();
        shopMessageText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C pressionado!"); // Adicione esta linha
            ToggleShop();
        }
    }

    void PopulateShop()
    {
        foreach (Weapon weapon in availableWeapons)
        {
            GameObject button = Instantiate(weaponButtonPrefab, weaponsContainer);
            button.GetComponent<WeaponButton>().Initialize(weapon);
            weaponButtons.Add(weapon, button);
        }
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        IsShopOpen = shopPanel.activeSelf;
        Time.timeScale = IsShopOpen ? 0 : 1; // Pausa o jogo
        shopMessageText.text = "";
        Debug.Log("Loja ativa: " + shopPanel.activeSelf);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Debug.Log("Tentando comprar: " + weapon.weaponName);
        
        if (CurrencyManager.instance.SpendCoins(weapon.cost))
        {
            Debug.Log("Compra bem-sucedida!");
            InventoryManager.instance.AddWeapon(weapon);
            Destroy(weaponButtons[weapon]);
            weaponButtons.Remove(weapon);
            ShowMessage("Compra realizada!", "#88FFA9");
        }
        else
        {
            ShowMessage("Moedas insuficientes!", "#88FFA9");
            Debug.Log("Moedas insuficientes!");
        }
    }

    void ShowMessage(string message, string hexColor)
    {
        shopMessageText.text = message;

        if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            shopMessageText.color = color;
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
        shopMessageText.text = "";
    }
}
// WeaponButton.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] Image weaponIcon;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text infoText;
    [SerializeField] Button buyButton;
    
    private Weapon weapon;

    public void Initialize(Weapon newWeapon)
    {
        weapon = newWeapon;
        weaponIcon.sprite = weapon.weaponIcon;
        nameText.text = weapon.weaponName;
        infoText.text = $"Dano: {weapon.damage}\nAlcance: {weapon.range}\nCadencia: {weapon.fireRate}\nCapacidade: {weapon.maxBullets}\nCusto: {weapon.cost}";
        
        buyButton.onClick.AddListener(TryBuyWeapon);
        //UpdateButtonState();
    }

    void Update()
    {
        UpdateButtonState();
    }

    public void TryBuyWeapon()
    {
        Debug.Log("Tentando comprar: " + weapon.weaponName);
        if (ShopManager.instance == null)
        {
            Debug.LogError("ShopManager.instance é NULL! Certifique-se de que o ShopManager está na cena.");
            return;
        }
        
        ShopManager.instance.BuyWeapon(weapon);
    }

    void UpdateButtonState()
    {
        bool canBuy = CurrencyManager.instance.totalCoins >= weapon.cost;
        buyButton.interactable = canBuy;
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryWeaponSlot : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button equipButton;
    
    private Weapon weapon;
    private static InventoryWeaponSlot currentlyEquipped;

    public void Initialize(Weapon newWeapon)
    {
        weapon = newWeapon;
        weaponIcon.sprite = weapon.weaponIcon;
        nameText.text = weapon.weaponName;
        infoText.text = $"Dano: {weapon.damage}\nAlcance: {weapon.range}\nCadencia: {weapon.fireRate}\nCapacidade: {weapon.maxBullets}\nCusto: {weapon.cost}";
        equipButton.onClick.AddListener(Equip);

        if (weapon == PlayerWeapon.instance.currentWeapon)
        {
            MarkAsEquipped();
            currentlyEquipped = this;
        }
    }

    public void Equip()
    {
        if (currentlyEquipped != null)
        {
            currentlyEquipped.equipButton.interactable = true; // Reativa o botão da arma anterior
            currentlyEquipped.equipButton.GetComponentInChildren<TMP_Text>().text = "Equipar"; // Atualiza o nome do botão
        }
        PlayerWeapon.instance.EquipWeapon(weapon);
        currentlyEquipped = this;

        MarkAsEquipped();
    }

    private void MarkAsEquipped()
    {
        equipButton.interactable = false;
        equipButton.GetComponentInChildren<TMP_Text>().text = "Equipada";
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryWeaponSlot : MonoBehaviour
{
    [SerializeField] Image weaponIcon;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text infoText;
    [SerializeField] Button equipButton;
    
    private Weapon weapon;

    public void Initialize(Weapon newWeapon)
    {
        weapon = newWeapon;
        weaponIcon.sprite = weapon.weaponIcon;
        nameText.text = weapon.weaponName;
        infoText.text = $"Dano: {weapon.damage}\nAlcance: {weapon.range}\nCadencia: {weapon.fireRate}\nCapacidade: {weapon.maxBullets}\nCusto: {weapon.cost}";
        equipButton.onClick.AddListener(Equip);
    }

    public void Equip()
    {
        PlayerWeapon.instance.EquipWeapon(weapon);
    }
}
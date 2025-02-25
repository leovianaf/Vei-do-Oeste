using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeShopManager : MonoBehaviour
{
    public static UpgradeShopManager instance;
    public static bool IsUpgradeShopOpen { get; private set; }
    public GameObject upgradePanel;
    public UpgradeItem[] upgradeItems;

    [Header("UI References")]
    public TMP_Text[] itemNames;
    public TMP_Text[] currentLevels;
    public TMP_Text[] nextCosts;
    public Button[] upgradeButtons;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            ToggleShop();
    }

    public void ToggleShop()
    {
        upgradePanel.SetActive(!upgradePanel.activeSelf);
        Time.timeScale = upgradePanel.activeSelf ? 0 : 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < upgradeItems.Length; i++)
        {
            itemNames[i].text = upgradeItems[i].itemName;
            currentLevels[i].text = $"Nível: {upgradeItems[i].currentLevel}/4";
            
            if (upgradeItems[i].currentLevel >= 4)
            {
                nextCosts[i].text = "MAX";
                upgradeButtons[i].interactable = false;
            }
            else
            {
                nextCosts[i].text = $"Valor: {upgradeItems[i].costs[upgradeItems[i].currentLevel].ToString()}";
                upgradeButtons[i].interactable = CurrencyManager.instance.totalCoins >= upgradeItems[i].costs[upgradeItems[i].currentLevel];
            }
        }
    }

    public void BuyUpgrade(int index)
    {
        UpgradeItem item = upgradeItems[index];
        
        if (item.currentLevel >= 4) return;
        
        int cost = item.costs[item.currentLevel];
        if (CurrencyManager.instance.SpendCoins(cost))
        {
            item.currentLevel++;
            ApplyUpgradeEffect(index);
            UpdateUI();
        }
    }

    void ApplyUpgradeEffect(int index)
    {
        switch (index)
        {
            case 0: // Chapéu
                CameraController.instance.UpdateVisibility(upgradeItems[0].currentLevel);
                break;
            case 1: // Colete
                PlayerHealth.instance.damageReduction = 0.10f * upgradeItems[1].currentLevel;
                break;
            case 2: // Bota
                PlayerMovement.instance.UpdateSpeed(0.05f * upgradeItems[2].currentLevel);
                break;
        }
    }
}

[System.Serializable]
public class UpgradeItem
{
    public string itemName;
    public int[] costs;
    [HideInInspector] public int currentLevel = 0;
}
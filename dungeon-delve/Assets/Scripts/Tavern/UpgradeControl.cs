using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControl : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI TableText;
    [SerializeField] private TextMeshProUGUI CostReductionText;
    [SerializeField] private Button ShopButton;

    [SerializeField] private GameObject upgradeMenu;

    //potietially upgrade shop for t2 equipment and items

    private void Start()
    {
        upgradeMenu.SetActive(false);
        SetLayerMask();
        UpdateTableText();
        UpdateCostReductionText();
        if (TavernData.shopPurchased)
        {
            ShopButton.interactable = false;
        }
    }

    public void OnTableClick()
    {
        if (TavernUpgradeFunctions.TryAddTable())
        {
            UpdateTableText();
            FindAnyObjectByType<GoldDisplay>().UpdateGoldText();
            //relevant scene updates
            FindAnyObjectByType<HeroGen>().SetTableActive(TavernData.tables - 1);
        }
    }

    public void OnCostReductionClick()
    {
        if (TavernUpgradeFunctions.TryAddCostReduction())
        {
            UpdateCostReductionText();
            FindAnyObjectByType<GoldDisplay>().UpdateGoldText();
            //relevant scene updates
            //Figure out how to trigger cost reduction at runtime
        }
    }

    public void OnShopClick()
    {
        if (TavernUpgradeFunctions.TryAddShop())
        {
            ShopButton.interactable = false;
            FindAnyObjectByType<GoldDisplay>().UpdateGoldText();
        }
    }

    private void UpdateTableText()
    {
        TableText.text = "Buy Table - " + TavernUpgradeFunctions.TableCost() + "G";
    }
    private void UpdateCostReductionText()
    {
        //update this so that the word changes and the tavern gets new meshes
        CostReductionText.text = "Buy Grog - " + TavernUpgradeFunctions.CostReductionCost() + "G";
    }

    public void Interact()
    {
        if (!upgradeMenu.activeSelf)
        {
            upgradeMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            HighlightedUIManager.SelectUIGameObject(ShopButton.gameObject);
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        upgradeMenu.SetActive(false);
    }
    private void SetLayerMask()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour, IInteractable
{
    [SerializeField] private float markup = 1.5f;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject equipmentMenu;
    [SerializeField] private GameObject eqButtonPrefab;
    [SerializeField] private Transform buttonParent;
    //in theory these could be one list because they won't be used at the same time
    private Button[] sellButtons = new Button[Equipment.inventorySize];
    private Button[] buyButtons;
    private Vector3 buttonParentStartPosition;

    private void Start()
    {
        buttonParentStartPosition = buttonParent.position;
        shopMenu.SetActive(false);
        SetLayerMask();
        buyButtons = new Button[DataFiles.Eq.Length];
    }
    public void OnSellMenuClicked()
    {
        equipmentMenu.SetActive(true);
        OpenSellMenu();
    }

    public void OnBuyMenuClicked()
    {
        equipmentMenu.SetActive(true);
        OpenBuyMenu();
    }

    public void OnBackButtonClicked()
    {
        equipmentMenu.SetActive(false);
        ClearButtons();
    }


    private void OpenSellMenu()
    {
        int count = 0;
        foreach(Equipment eq in Equipment.eq_inventory)
        {
            if(eq == null)
            {
                continue;
            }
            GameObject eqButton = Instantiate(eqButtonPrefab, buttonParent);//instatiate button with object
            sellButtons[count] = eqButton.GetComponent<Button>();
            sellButtons[count].onClick.AddListener(() => SellItem(eq, eqButton));
            sellButtons[count].GetComponent<ShopButtonControl>().Initialize(eq, eq.GetGoldValue());
            count++;
        }
    }

    private void SellItem(Equipment eq, GameObject button)
    {
        PlayerData.AddGoldDirect(eq.GetGoldValue());
        Equipment.RemoveEq(eq);
        FindAnyObjectByType<GoldDisplay>()?.UpdateGoldText();
        Destroy(button);
    }

    private void OpenBuyMenu()
    {
        for(int count = 1; count < DataFiles.Eq.Length; count++)
        {
            Equipment eq = new Equipment(count);
            GameObject eqButton = Instantiate(eqButtonPrefab, buttonParent);//instatiate button with object
            buyButtons[count] = eqButton.GetComponent<Button>();
            buyButtons[count].onClick.AddListener(() => TryBuyItem(eq, count));
            buyButtons[count].GetComponent<ShopButtonControl>().Initialize(eq, GetBuyPrice(eq));
        }
    }

    private void TryBuyItem(Equipment eq, int buttonCount)
    {
        if (PlayerData.GetGold() > GetBuyPrice(eq))
        {
            //Debug.Log("Bought " + eq.GetName());
            Equipment.AddEq(eq);
            PlayerData.SpendGold(GetBuyPrice(eq));
            FindAnyObjectByType<GoldDisplay>()?.UpdateGoldText();
        }
        //signal player that they don't have enough money
    }

    private int GetBuyPrice(Equipment eq)
    {
        return Mathf.CeilToInt(eq.GetGoldValue() * markup);
    }
    private void ClearButtons()
    {
        buttonParent.position = buttonParentStartPosition;
        for (int i = 0; i < sellButtons.Length; i++)
        {
            if (sellButtons[i] == null)
            {
                continue;
            }
            Destroy(sellButtons[i].gameObject);
            sellButtons[i] = null;
        }
        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (buyButtons[i] == null)
            {
                continue;
            }
            Destroy(buyButtons[i].gameObject);
            buyButtons[i] = null;
        }
    }

    public void Interact()
    {
        if (!shopMenu.gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            shopMenu.gameObject.SetActive(true);
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        shopMenu.gameObject.SetActive(false);
    }
    private void SetLayerMask()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO;
    public int quantity;
    public Image itemImage;
    public TMP_Text quantityText;
    private InventoryManager inventoryManager;
    private static ShopManager activeShop;
    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }
    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }
    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }
    private void HandleShopStateChanged(ShopManager shopManager, bool isOpen)
    {
        activeShop = isOpen ? shopManager : null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (activeShop != null)
                {
                    // block selling key items
                    if (itemSO.isKeyItem)
                        return;

                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }
                else
                {
                    // block using key items
                    if (itemSO.isKeyItem)
                        return;

                    if (StatsManager.Instance == null)
                    {
                        Debug.LogError("StatsManager is missing from this scene!");
                        return;
                    }

                  
                    if (itemSO.currentHealth > 0 && itemSO.currentHealth + StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth + itemSO.maxHealth)
                    {
                        Debug.Log("Already at full health!");
                        itemSO.currentHealth = itemSO.maxHealth;

                        inventoryManager.UseItem(this);
                    }

                    inventoryManager.UseItem(this);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                // block dropping key items
                if (itemSO.isKeyItem)
                    return;

                inventoryManager.DropItem(this);
            }
        }
    }
    public void UpdateUI()
    {
        if (quantity <= 0)
            itemSO = null;
        if (itemSO != null)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
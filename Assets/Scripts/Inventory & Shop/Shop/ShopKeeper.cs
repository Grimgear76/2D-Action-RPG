using UnityEngine;
using System.Collections.Generic;
using System;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;
    public Animator anim;
    public ShopManager shopManager;
    [SerializeField] private List<ShopItems> shop1;
    [SerializeField] private List<ShopItems> shop2;
    [SerializeField] private List<ShopItems> shop3;
    public static event Action<ShopManager, bool> OnShopStateChanged;
    private bool playerInRange;
    private bool isShopOpen;

    private CanvasGroup GetShopCanvasGroup()
    {
        GameObject shopCanvas = GameObject.Find("ShopCanvas");
        if (shopCanvas != null)
        {
            CanvasGroup cg = shopCanvas.GetComponent<CanvasGroup>();
            if (cg != null) return cg;
            Debug.LogWarning("ShopKeeper: 'ShopCanvas' found but has no CanvasGroup component.");
        }
        else
        {
            Debug.LogWarning("ShopKeeper: Could not find GameObject named 'ShopCanvas' in the scene.");
        }
        return null;
    }

    private void Awake()
    {
        if (shopManager == null)
        {
            shopManager = FindFirstObjectByType<ShopManager>();
            if (shopManager == null)
                Debug.LogWarning("ShopKeeper: Could not find a ShopManager in the scene.");
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                CanvasGroup shopCanvasGroup = GetShopCanvasGroup();
                if (shopCanvasGroup == null) return;

                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    currentShopKeeper = this;
                    isShopOpen = true;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                    OpenShop1();
                }
                else
                {
                    Time.timeScale = 1;
                    currentShopKeeper = null;
                    isShopOpen = false;
                    OnShopStateChanged?.Invoke(shopManager, false);
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;
                    shopCanvasGroup.interactable = false;
                }
            }
        }
    }

    public void OpenShop1() => shopManager.PopulateShopItems(shop1);
    public void OpenShop2() => shopManager.PopulateShopItems(shop2);
    public void OpenShop3() => shopManager.PopulateShopItems(shop3);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}
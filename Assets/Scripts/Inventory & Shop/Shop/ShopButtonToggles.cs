using UnityEngine;

public class ShopButtonToggles : MonoBehaviour
{

    public void OpenShop1()
    {
        if(ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenShop1();
        }
    }
    public void OpenShop2()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenShop2();
        }
    }
    public void OpenShop3()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenShop3();
        }
    }

}

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener //для получения сообщений из Unity Purchasing
{
    public static IAPManager instance;
    [SerializeField] private GameObject btnNoAds;
    [SerializeField] private GameObject btnVip;
    public Text MoneyText;
    public ShopManager shopManager;
    //[SerializeField] private GameObject btnVip_afterBuy;
    //[SerializeField] private GameObject vipBanner;

    IStoreController m_StoreController;

    private string removeadertaisments = "com.tenmesti.jumpingball.removeadvertaisments";
    private string money100 = "com.tenmesti.jumpingball.money100";
    private string money200 = "com.tenmesti.jumpingball.money200";
    private string money500 = "com.tenmesti.jumpingball.money500";
    private string money1000 = "com.tenmesti.jumpingball.money1000";
    private string money2000 = "com.tenmesti.jumpingball.money2000";
    private string unlockallcharacters = "com.tenmesti.jumpingball.unlockallcharacters";

    void Start()
    {
       GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("NoReboot");
       if(gameObjects.Length > 1)
        { 
            Destroy(gameObjects[0].gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        InitializePurchasing();
        
        //if (PlayerPrefs.HasKey("firstStart") == false)
        //{
        //    PlayerPrefs.SetInt("firstStart", 1);
        //    RestoreMyProduct();
        //}

        RestoreVariable();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(removeadertaisments, ProductType.NonConsumable);
        builder.AddProduct(unlockallcharacters, ProductType.NonConsumable);
        builder.AddProduct(money100, ProductType.Consumable);
        builder.AddProduct(money200, ProductType.Consumable);
        builder.AddProduct(money500, ProductType.Consumable);
        builder.AddProduct(money1000, ProductType.Consumable);
        builder.AddProduct(money2000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    void RestoreVariable()
    {
        if (PlayerPrefs.HasKey("removeadvertaisments"))
        {
            btnNoAds.SetActive(false);
        }

        if (PlayerPrefs.HasKey("unlockallcharacters"))
        {
            btnVip.SetActive(false);
            //btnVip_afterBuy.SetActive(true);
        }

        //if (PlayerPrefs.HasKey("ads") && PlayerPrefs.HasKey("vip"))
        //{
            //vipBanner.SetActive(true);
        //}
    }

    public void BuyProduct(string productName)
    {
        m_StoreController.InitiatePurchase(productName);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        if (product.definition.id == removeadertaisments)
        {
            Product_RemoveAdvertaisments();
        }
        if (product.definition.id == money100)
        {
            Product_AddMoney(100);
        }
        if (product.definition.id == money200)
        {
            Product_AddMoney(200);
        }
        if (product.definition.id == money500)
        {
            Product_AddMoney(500);
        }
        if (product.definition.id == money1000)
        {
            Product_AddMoney(1000);
        }
        if (product.definition.id == money2000)
        {
            Product_AddMoney(2000);
        }
        if (product.definition.id == unlockallcharacters)
        {
            Product_UnlockAllCharacters();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        return PurchaseProcessingResult.Complete;
    }

    private void Product_RemoveAdvertaisments()
    {
        PlayerPrefs.SetInt("removeadvertaisments", 1);
        btnNoAds.SetActive(false);

        //AdsCore.S.StopAllCoroutines();
        //AdsCore.S.HideBanner();

        //if (PlayerPrefs.HasKey("vip"))
            //vipBanner.SetActive(true);
    }

    private void Product_UnlockAllCharacters()
    {
        for (int i = 0; i < shopManager.characterShop.Length; i++)
        {
            shopManager.characterShop[i].IsUnlocked = true;
            shopManager.UpdateUI();
        }
        btnVip.SetActive(false);
        AchievementManager.instance.Complete(2, 0);
        PlayerPrefs.SetInt("unlockallcharacters", 1);
    }
    private void Product_AddMoney(int amount)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + amount);
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        //throw new NotImplementedException();
    }


    //public void RestoreMyProduct()
    //{
    //    if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(noads).hasReceipt)
    //    {
    //        Product_NoAds();
    //    }

    //    if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(vip).hasReceipt)
    //    {
    //        Product_VIP();
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSpaceshipMenu : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //SelectShip Display Reference
    private SelectSpaceship displaySpaceship;

    //Shop System Reference
    private ShopSystem shopManager;

    //Spaceship Parts Text Reference
    private Text bodyText;
    private Text boostText;
    private Text weaponText;

    //Spaceship Parts Names Array
    private string[] modelsBody;
    private string[] modelsBoost;
    private string[] modelsWeapon;

    //Spaceship Parts Array Index
    private int indexBody;
    private int indexBoost;
    private int indexWeapon;

    //Selected Spaceship Parts
    private string currentSelectedBody;
    private string currentSelectedBoost;
    private string currentSelectedWeapon;

    //Shop Button's
    private GameObject bodyBuyButton;
    private GameObject boostBuyButton;
    private GameObject weaponBuyButton;

    //Shop Text's
    private Text bodyPriceText;
    private Text boostPriceText;
    private Text weaponPriceText;


    private void Start()
    {
        //Get References
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();
        displaySpaceship = GameObject.Find("Customization").GetComponent<SelectSpaceship>();
        shopManager = GameObject.Find("ShopMenu").GetComponent<ShopSystem>();

        bodyText = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        boostText = transform.GetChild(0).GetChild(4).GetComponent<Text>();
        weaponText = transform.GetChild(0).GetChild(7).GetComponent<Text>();

        bodyBuyButton = shopManager.gameObject.transform.GetChild(0).gameObject;
        boostBuyButton = shopManager.gameObject.transform.GetChild(2).gameObject;
        weaponBuyButton = shopManager.gameObject.transform.GetChild(4).gameObject;

        bodyPriceText = shopManager.gameObject.transform.GetChild(1).GetComponent<Text>();
        boostPriceText = shopManager.gameObject.transform.GetChild(3).GetComponent<Text>();
        weaponPriceText = shopManager.gameObject.transform.GetChild(5).GetComponent<Text>();

        //Initialize Arrays
        modelsBody = new string[] { "A", "B", "C", "D", "E" };
        modelsBoost = new string[] { "A", "B", "C", "D", "E" };
        modelsWeapon = new string[] { "A", "B", "C", "D", "E" };

        //Set Initial Body Index
        for (int index = 0; index < modelsBody.Length; index++)
        {
            if (modelsBody[index] == repository.GetSelectedBodyPart())
            {
                indexBody = index;
            }
        }

        //Set Initial Boost Index
        for (int index = 0; index < modelsBoost.Length; index++)
        {
            if (modelsBoost[index] == repository.GetSelectedBoostPart())
            {
                indexBoost = index;
            }
        }

        //Set Initial Weapon Index
        for (int index = 0; index < modelsWeapon.Length; index++)
        {
            if (modelsWeapon[index] == repository.GetSelectedWeaponPart())
            {
                indexWeapon = index;
            }
        }

        //Set Initial Spaceship Selected Parts
        currentSelectedBody = modelsBody[indexBody];
        currentSelectedBoost = modelsBoost[indexBoost];
        currentSelectedWeapon = modelsWeapon[indexWeapon];

        //Initial Update Display Spaceship
        UpdateBodyModel();
        UpdateBoostModel();
        UpdateWeaponModel();

        //Deactivate Shop Buttons
        ActivationBuyButtonBody(0);
        ActivationBuyButtonBoost(0);
        ActivationBuyButtonWeapon(0);

        //Change Shop Text
        SetBodyText(0);
        SetBoostText(0);
        SetWeaponText(0);
    }

    #region Buttons Select Spaceship

    #region Spaceship Body
    public void NextBodyModel()
    {
        indexBody = (indexBody + 1) % modelsBody.Length;

        UpdateBodyModel();
        UpdateShopBody(modelsBody[indexBody]);
    }

    public void PreviousBodyModel()
    {
        indexBody = (indexBody - 1) % modelsBody.Length;
        if (indexBody == -1)
        {
            indexBody = modelsBody.Length - 1;
        }

        UpdateBodyModel();
        UpdateShopBody(modelsBody[indexBody]);
    }

    private void UpdateBodyModel()
    {
        bodyText.text = modelsBody[indexBody];
        displaySpaceship.UpdateBody(modelsBody[indexBody]);

        UpdateSelectedBody();
    }
    #endregion

    #region Spaceship Boost
    public void NextBoostModel()
    {
        indexBoost = (indexBoost + 1) % modelsBoost.Length;

        UpdateBoostModel();
        UpdateShopBoost(modelsBoost[indexBoost]);
    }

    public void PreviousBoostModel()
    {
        indexBoost = (indexBoost - 1) % modelsBoost.Length;
        if (indexBoost == -1)
        {
            indexBoost = modelsBoost.Length - 1;
        }

        UpdateBoostModel();
        UpdateShopBoost(modelsBoost[indexBoost]);
    }

    private void UpdateBoostModel()
    {
        boostText.text = modelsBoost[indexBoost];
        displaySpaceship.UpdateBoost(modelsBoost[indexBoost]);

        UpdateSelectedBoost();
    }
    #endregion

    #region Spaceship Weapon
    public void NextWeaponModel()
    {
        indexWeapon = (indexWeapon + 1) % modelsWeapon.Length;

        UpdateWeaponModel();
        UpdateShopWeapon(modelsWeapon[indexWeapon]);
    }

    public void PreviousWeaponModel()
    {
        indexWeapon = (indexWeapon - 1) % modelsWeapon.Length;
        if (indexWeapon == -1)
        {
            indexWeapon = modelsWeapon.Length - 1;
        }

        UpdateWeaponModel();
        UpdateShopWeapon(modelsWeapon[indexWeapon]);
    }

    private void UpdateWeaponModel()
    {
        weaponText.text = modelsWeapon[indexWeapon];
        displaySpaceship.UpdateWeapon(modelsWeapon[indexWeapon]);

        UpdateSelectedWeapon();
    }
    #endregion

    #region Selection Management

    #region Selected Body
    private void UpdateSelectedBody()
    {
        if (repository.GetPurchasedBodyParts().Contains(modelsBody[indexBody]))
        {
            currentSelectedBody = modelsBody[indexBody];
        }
    }
    #endregion

    #region Selected Boost
    private void UpdateSelectedBoost()
    {
        if (repository.GetPurchasedBoostParts().Contains(modelsBoost[indexBoost]))
        {
            currentSelectedBoost = modelsBoost[indexBoost];
        }
    }
    #endregion

    #region Selected Weapon
    private void UpdateSelectedWeapon()
    {
        if (repository.GetPurchasedWeaponParts().Contains(modelsWeapon[indexWeapon]))
        {
            currentSelectedWeapon = modelsWeapon[indexWeapon];
        }
    }
    #endregion

    #endregion

    public void BackToPersonalMenu()
    {
        #region Body Selection Final Update
        //Set Body Index
        for (int index = 0; index < modelsBody.Length; index++)
        {
            if (modelsBody[index] == currentSelectedBody)
            {
                indexBody = index;
            }
        }

        bodyText.text = currentSelectedBody;
        displaySpaceship.UpdateBody(currentSelectedBody);

        UpdateShopBody(currentSelectedBody);

        repository.SetSelectedBodyPart(currentSelectedBody);
        #endregion

        #region Boost Selection Final Update
        //Set Boost Index
        for (int index = 0; index < modelsBoost.Length; index++)
        {
            if (modelsBoost[index] == currentSelectedBoost)
            {
                indexBoost = index;
            }
        }

        boostText.text = currentSelectedBoost;
        displaySpaceship.UpdateBoost(currentSelectedBoost);

        UpdateShopBoost(currentSelectedBoost);

        repository.SetSelectedBoostPart(currentSelectedBoost);
        #endregion

        #region Weapon Selection Final Update
        //Set Weapon Index
        for (int index = 0; index < modelsWeapon.Length; index++)
        {
            if (modelsWeapon[index] == currentSelectedWeapon)
            {
                indexWeapon = index;
            }
        }

        weaponText.text = currentSelectedWeapon;
        displaySpaceship.UpdateWeapon(currentSelectedWeapon);

        UpdateShopWeapon(currentSelectedWeapon);

        repository.SetSelectedWeaponPart(currentSelectedWeapon);
        #endregion
    }

    #endregion

    #region Shop Management

    #region Body Section
    private void ActivationBuyButtonBody(int price)
    {
        if(price != 0)
        {
            bodyBuyButton.SetActive(true);
        }
        else
        {
            bodyBuyButton.SetActive(false);
        }
        
    }

    private void SetBodyText(int price)
    {
        if(price == 0)
        {
            bodyPriceText.text = "Owned";
        }
        else
        {
            bodyPriceText.text = "Price : " + price.ToString();
        }
    }

    private void UpdateShopBody(string currentItem)
    {
        int price = shopManager.UpdateSelectedBody(currentItem);

        ActivationBuyButtonBody(price);
        SetBodyText(price);
    }
    #endregion

    #region Boost Section
    private void ActivationBuyButtonBoost(int price)
    {
        if (price != 0)
        {
            boostBuyButton.SetActive(true);
        }
        else
        {
            boostBuyButton.SetActive(false);
        }
    }

    private void SetBoostText(int price)
    {
        if (price == 0)
        {
            boostPriceText.text = "Owned";
        }
        else
        {
            boostPriceText.text = "Price : " + price.ToString();
        }
    }

    private void UpdateShopBoost(string currentItem)
    {
        int price = shopManager.UpdateSelectedBoost(currentItem);

        ActivationBuyButtonBoost(price);
        SetBoostText(price);
    }
    #endregion

    #region Weapon Section
    private void ActivationBuyButtonWeapon(int price)
    {
        if (price != 0)
        {
            weaponBuyButton.SetActive(true);
        }
        else
        {
            weaponBuyButton.SetActive(false);
        }
    }

    private void SetWeaponText(int price)
    {
        if (price == 0)
        {
            weaponPriceText.text = "Owned";
        }
        else
        {
            weaponPriceText.text = "Price : " + price.ToString();
        }
    }

    private void UpdateShopWeapon(string currentItem)
    {
        int price = shopManager.UpdateSelectedWeapon(currentItem);

        ActivationBuyButtonWeapon(price);
        SetWeaponText(price);
    }
    #endregion

    #endregion

    #region Shop Buttons

    #region Body Section
    public void BuyBodyButton() 
    {
        shopManager.BuyBody(modelsBody[indexBody]);

        UpdateShopBody(modelsBody[indexBody]);

        UpdateSelectedBody();
    }
    #endregion

    #region Boost Section
    public void BuyBoostButton()
    {
        shopManager.BuyBoost(modelsBoost[indexBoost]);

        UpdateShopBoost(modelsBoost[indexBoost]);

        UpdateSelectedBoost();
    }
    #endregion

    #region Weapon Section
    public void BuyWeaponButton()
    {
        shopManager.BuyWeapon(modelsWeapon[indexWeapon]);

        UpdateShopWeapon(modelsWeapon[indexWeapon]);

        UpdateSelectedWeapon();
    }
    #endregion

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //Available Items for Purchase
    private List<string> bodyAvailable;
    private List<string> boostAvailable;
    private List<string> weaponAvailable;

    //Purchased Items
    private List<string> bodyPurchased;
    private List<string> boostPurchased;
    private List<string> weaponPurchased;

    //Items Prices
    private Dictionary<string, int> bodyPrices;
    private Dictionary<string, int> boostPrices;
    private Dictionary<string, int> weaponPrices;

    private void Start()
    {
        //Get Reference
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();

        //Set What's Purchased and What's Available for Purchase
        bodyPurchased = repository.GetPurchasedBodyParts();
        boostPurchased = repository.GetPurchasedBoostParts();
        weaponPurchased = repository.GetPurchasedWeaponParts();

        bodyAvailable = repository.GetAvailableBodyParts();
        boostAvailable = repository.GetAvailableBoostParts();
        weaponAvailable = repository.GetAvailableWeaponParts();

        //Initialize Dictionaries
        bodyPrices = new Dictionary<string, int>();
        boostPrices = new Dictionary<string, int>();
        weaponPrices = new Dictionary<string, int>();

        //Set Original Body Prices
        bodyPrices.Add("A", 0);
        bodyPrices.Add("B", 10);
        bodyPrices.Add("C", 25);
        bodyPrices.Add("D", 50);
        bodyPrices.Add("E", 100);

        //Set Original Boost Prices
        boostPrices.Add("A", 0);
        boostPrices.Add("B", 10);
        boostPrices.Add("C", 25);
        boostPrices.Add("D", 50);
        boostPrices.Add("E", 100);

        //Set Original Weapons Prices
        weaponPrices.Add("A", 0);
        weaponPrices.Add("B", 10);
        weaponPrices.Add("C", 25);
        weaponPrices.Add("D", 50);
        weaponPrices.Add("E", 100);

        //Update Body Prices
        foreach(string part in bodyPurchased)
        {
            bodyPrices[part] = 0;
        }

        //Update Boost Prices
        foreach (string part in boostPurchased)
        {
            boostPrices[part] = 0;
        }

        //Update Weapon Prices
        foreach (string part in weaponPurchased)
        {
            weaponPrices[part] = 0;
        }
    }

    #region Body Section
    //Return Body Price if Available. If not, returns 0 (Free / Owned)
    public int UpdateSelectedBody(string currentSelected)
    {
        if (bodyAvailable.Contains(currentSelected))
        {
            return GetPriceBody(currentSelected);
        }

        return 0;
    }

    //Returns Body Price
    private int GetPriceBody(string currentSelected)
    {
        return bodyPrices[currentSelected];
    }

    //Buy Body Part
    public void BuyBody(string toBeBought)
    {
        if (repository.GetCoins() >= bodyPrices[toBeBought])
        {
            repository.SetCoins(repository.GetCoins() - bodyPrices[toBeBought]);

            bodyPurchased.Add(toBeBought);
            bodyAvailable.Remove(toBeBought);
            bodyPrices[toBeBought] = 0;

            repository.AddPurchasedBodyPart(toBeBought);
            repository.RemoveAvailableBodyPart(toBeBought);
        }
    }
    #endregion

    #region Boost Section
    //Return Boost Price if Available. If not, returns 0 (Free / Owned)
    public int UpdateSelectedBoost(string currentSelected)
    {
        if (boostAvailable.Contains(currentSelected))
        {
            return GetPriceBoost(currentSelected);
        }

        return 0;
    }

    //Returns Boost Price
    private int GetPriceBoost(string currentSelected)
    {
        return boostPrices[currentSelected];
    }

    //Buy Boost Part
    public void BuyBoost(string toBeBought)
    {
        if (repository.GetCoins() >= boostPrices[toBeBought])
        {
            repository.SetCoins(repository.GetCoins() - boostPrices[toBeBought]);

            boostPurchased.Add(toBeBought);
            boostAvailable.Remove(toBeBought);
            boostPrices[toBeBought] = 0;

            repository.AddPurchasedBoostPart(toBeBought);
            repository.RemoveAvailableBoostPart(toBeBought);
        }
    }
    #endregion

    #region Weapon Section
    //Return Weapon Price if Available. If not, returns 0 (Free / Owned)
    public int UpdateSelectedWeapon(string currentSelected)
    {
        if (weaponAvailable.Contains(currentSelected))
        {
            return GetPriceWeapon(currentSelected);
        }

        return 0;
    }

    //Returns Weapon Price
    private int GetPriceWeapon(string currentSelected)
    {
        return weaponPrices[currentSelected];
    }

    //Buy Weapon Part
    public void BuyWeapon(string toBeBought)
    {
        if(repository.GetCoins() >= weaponPrices[toBeBought])
        {
            repository.SetCoins(repository.GetCoins() - weaponPrices[toBeBought]);

            weaponPurchased.Add(toBeBought);
            weaponAvailable.Remove(toBeBought);
            weaponPrices[toBeBought] = 0;

            repository.AddPurchasedWeaponPart(toBeBought);
            repository.RemoveAvailableWeaponPart(toBeBought);
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPERIMENTAL_ShopManager : MonoBehaviour
{
    private Text finalModel;

    private Text textBodyModel;
    private Text textBoostModel;
    private Text textWeaponModel;

    private string[] modelsBody;
    private string[] modelsBoost;
    private string[] modelsWeapon;

    private int modelsBodyIndex;
    private int modelsBoostIndex;
    private int modelsWeaponIndex;

    EXPERIMENTAL_ShopDisplay display;

    private void Start()
    {
        display = new EXPERIMENTAL_ShopDisplay();

        textBodyModel = transform.Find("CurrentModelBody").GetComponent<Text>();
        textBoostModel = transform.Find("CurrentModelBoost").GetComponent<Text>();
        textWeaponModel = transform.Find("CurrentModelWeapon").GetComponent<Text>();
        
        modelsBody = new string[] { "A", "B", "C", "D", "E"};
        modelsBoost = new string[] { "A", "B", "C", "D", "E"};
        modelsWeapon = new string[] { "A", "B", "C", "D", "E"};

        modelsBodyIndex = 0;
        modelsBoostIndex = 0;
        modelsWeaponIndex = 0;

        UpdateBodyModel();
        UpdateBoostModel();
        UpdateWeaponModel();
    }

    //-----------------Body Related---------------------------------------
    public void NextBodyModel()
    {
        modelsBodyIndex = (modelsBodyIndex + 1) % modelsBody.Length;
        UpdateBodyModel();
    }

    public void PreviousBodyModel()
    {
        modelsBodyIndex = (modelsBodyIndex - 1) % modelsBody.Length;
        if(modelsBodyIndex == -1)
        {
            modelsBodyIndex = modelsBody.Length - 1;
        }
        UpdateBodyModel();
    }

    private void UpdateBodyModel()
    {
        textBodyModel.text = modelsBody[modelsBodyIndex];
        display.UpdateBody(modelsBody[modelsBodyIndex]);
    }
    //--------------------------------------------------------------------

    //-----------------Boost Related--------------------------------------
    public void NextBoostModel()
    {
        modelsBoostIndex = (modelsBoostIndex + 1) % modelsBoost.Length;
        UpdateBoostModel();
    }
    
    public void PreviousBoostModel()
    {
        modelsBoostIndex = (modelsBoostIndex - 1) % modelsBoost.Length;
        if (modelsBoostIndex == -1)
        {
            modelsBoostIndex = modelsBoost.Length - 1;
        }
        UpdateBoostModel();
    }

    private void UpdateBoostModel()
    {
        textBoostModel.text = modelsBoost[modelsBoostIndex];
        display.UpdateBoost(modelsBoost[modelsBoostIndex]);
    }
    //--------------------------------------------------------------------

    //-----------------Weapon Related-------------------------------------
    public void NextWeaponModel()
    {
        modelsWeaponIndex = (modelsWeaponIndex + 1) % modelsWeapon.Length;
        UpdateWeaponModel();
    }
    
    public void PreviousWeaponModel()
    {
        modelsWeaponIndex = (modelsWeaponIndex - 1) % modelsWeapon.Length;
        if (modelsWeaponIndex == -1)
        {
            modelsWeaponIndex = modelsWeapon.Length - 1;
        }
        UpdateWeaponModel();
    }

    private void UpdateWeaponModel()
    {
        textWeaponModel.text = modelsWeapon[modelsWeaponIndex];
        display.UpdateWeapon(modelsWeapon[modelsWeaponIndex]);
    }
    //--------------------------------------------------------------------
}

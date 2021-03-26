using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_ShopDisplay
{
    private Transform finalModel;

    private string body;
    private string boost;
    private string weapon;

    private string modelSelection;

    private GameObject currentModel;

    public EXPERIMENTAL_ShopDisplay()
    {
        Start();
    }

    private void Start()
    {
        finalModel = GameObject.Find("Customization").transform;

        body = "A";
        boost = "A";
        weapon = "A";

        modelSelection = body + boost + weapon;

        currentModel = finalModel.Find(modelSelection).gameObject;
        currentModel.SetActive(true);
    }

    public void UpdateBody(string selection)
    {
        body = selection;
        modelSelection = body + boost + weapon;

        UpdateSpaceship();
    }

    public void UpdateBoost(string selection)
    {
        boost = selection;
        modelSelection = body + boost + weapon;

        UpdateSpaceship();
    }

    public void UpdateWeapon(string selection)
    {
        weapon = selection;
        modelSelection = body + boost + weapon;

        UpdateSpaceship();
    }

    public void UpdateSpaceship()
    {
        currentModel.SetActive(false);
        currentModel = finalModel.Find(modelSelection).gameObject;
        currentModel.SetActive(true);
    }
}

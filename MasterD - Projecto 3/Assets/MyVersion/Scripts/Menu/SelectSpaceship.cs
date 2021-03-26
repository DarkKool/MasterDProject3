using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSpaceship : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //Selection Strings
    private string body;
    private string boost;
    private string weapon;

    //Final Model String
    private string modelSelection;

    //Current Model GameObject
    private GameObject currentModel;

    private void Awake()
    {
        //Get Reference
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();

        //Set Starting Spaceship Parts
        body = repository.GetSelectedBodyPart();
        boost = repository.GetSelectedBoostPart();
        weapon = repository.GetSelectedWeaponPart();

        //Set Model Selection Name
        modelSelection = body + boost + weapon;

        //Get Current Model with Model Name
        currentModel = transform.Find(modelSelection).gameObject;
        
        //Deactivate All Models
        foreach(Transform spaceships in transform)
        {
            spaceships.gameObject.SetActive(false);
        }

        //Set Current Model Active
        currentModel.SetActive(true);
    }

    #region Update Spaceship Parts
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
    #endregion

    private void UpdateSpaceship()
    {
        currentModel.SetActive(false);
        currentModel = transform.Find(modelSelection).gameObject;
        currentModel.SetActive(true);
    }
}

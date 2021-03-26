using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHistoryManager : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //Menu Manager Reference
    private MenuManager menuManager;

    //Individual Game Session List Parent Reference
    private GameObject gameSessionList;

    //Game Session Details Reference
    private static GameObject gameSessionDetails;

    //Individual Game Session Prefab Reference
    private GameObject gameSessionHolderPrefab;

    //List Holding Game History 
    private List<GameHistory> gameHistories;

    // Start is called before the first frame update
    void Start()
    {
        //Get References
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

        gameSessionList = GameObject.Find("GameHistoryIndividualList");

        gameSessionDetails = transform.GetChild(8).gameObject;

        gameSessionHolderPrefab = Resources.Load<GameObject>("History/GameSessionHolder");

        //Initialize Game Histories List
        gameHistories = new List<GameHistory>();

        //Instantiate Game Sessions
        for(int index = LoadGameSessions(); index > 0; index--)
        {
            GameObject individualGameSession = Instantiate(gameSessionHolderPrefab, gameSessionList.transform);
            
            individualGameSession.GetComponent<RectTransform>().anchoredPosition = new Vector3(6, 60 - ((index - 1) * 42));
            individualGameSession.GetComponent<GameHistory>().SetInformation(LoadIndividualGameSession(index - 1));

            gameHistories.Add(individualGameSession.GetComponent<GameHistory>());
        }

        //Deactivate Game History Details
        gameSessionDetails.SetActive(false);
    }

    private int LoadGameSessions()
    {
        return repository.GetNumberOfGameSessions();
    }

    private List<string> LoadIndividualGameSession(int index)
    {
        return repository.GetGameSession(index);
    }

    public void SelectGameHistory(GameHistory gameSession)
    {
        gameSessionDetails.SetActive(true);

        gameSessionDetails.transform.GetChild(1).GetComponent<Text>().text = gameSession.GetInformation()[0];
        gameSessionDetails.transform.GetChild(3).GetComponent<Text>().text = gameSession.GetInformation()[1];
        gameSessionDetails.transform.GetChild(5).GetComponent<Text>().text = gameSession.GetInformation()[2];
        gameSessionDetails.transform.GetChild(7).GetComponent<Text>().text = gameSession.GetInformation()[3];
        gameSessionDetails.transform.GetChild(9).GetComponent<Text>().text = gameSession.GetInformation()[4];
        gameSessionDetails.transform.GetChild(11).GetComponent<Text>().text = gameSession.GetInformation()[5];
    }

    public void BackFromHistoryMenu()
    {
        gameSessionDetails.SetActive(false);

        menuManager.BackToPersonalMenu();
    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public UIDocument uiDocument;
    public GameObject leagueSelectUI;
    public GameObject leagueSummaryUI;
    public GameObject addNewLeagueUI;
    private bool sideBarVisible = false;
    private bool onHomeScreen = true;

    void Start()
    {
        leagueSelectUI.SetActive(false);
        uiDocument.gameObject.SetActive(false);
    }

    void Update()
    {
        // Toggle the UI visibility when Escape is pressed AND the UI is not already visible
        if (Input.GetKeyDown(KeyCode.Escape) && onHomeScreen)
        {
            sideBarVisible = !sideBarVisible;
            uiDocument.gameObject.SetActive(sideBarVisible);
        }
    }

    public void OpenLeagueSelectUI()
    {
        leagueSelectUI.SetActive(true);
        uiDocument.gameObject.SetActive(false);
        onHomeScreen = false;
    }

    public void CloseLeagueSelectUI()
    {
        sideBarVisible = false;
        leagueSelectUI.SetActive(false);
        uiDocument.gameObject.SetActive(false);
        onHomeScreen = true;
    }

    public void OpenLeagueSummaryCenterUI()
    {
        leagueSelectUI.SetActive(false);
        leagueSummaryUI.SetActive(true);

    }

    public void OpenAddNewLeagueScreen()
    {
        leagueSelectUI.SetActive(false);
        addNewLeagueUI.SetActive(true);
    }

    public void CloseAddNewLeagueScreen()
    {
        leagueSelectUI.SetActive(true);
        addNewLeagueUI.SetActive(false);
    }
}

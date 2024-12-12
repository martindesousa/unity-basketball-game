using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeagueListUIManager : MonoBehaviour
{
    public GameObject leagueItemPrefab;
    public GameObject addItemPrefab;
    public Transform contentPanel;
    public List<League> leagues;
    public Color color1 = Color.black; // First color
    public Color color2 = Color.gray;  // Second color

    private UIManager uiManager;

    void Start()
    {
        // Find the UIManager instance in the scene
        GameObject uiManagerObject = GameObject.Find("UIManager");
        if (uiManagerObject == null)
        {
            Debug.LogError("UIManager not found in the scene.");
        }

        uiManager = uiManagerObject.GetComponent<UIManager>();

        PopulateLeagueList();
    }

    void PopulateLeagueList()
    {
        for (int i = 0; i < leagues.Count; i++)
        {
            League league = leagues[i];
            GameObject newLeagueItem = Instantiate(leagueItemPrefab, contentPanel);

            // Alternate color for every other item's backdrop
            Color backdropColor = (i % 2 == 0) ? color1 : color2;
            newLeagueItem.transform.GetComponent<Image>().color = backdropColor;
            newLeagueItem.transform.Find("LeagueIcon").GetComponent<Image>().sprite = league.leagueIcon;

            Transform horizontalScaler = newLeagueItem.transform.Find("HorizontalScaler");
            horizontalScaler.Find("LeagueName").GetComponent<TextMeshProUGUI>().text = league.leagueName;
            horizontalScaler.Find("LeagueYear").GetComponent<TextMeshProUGUI>().text = "(" + league.year.ToString() + ")";
            LayoutRebuilder.ForceRebuildLayoutImmediate(horizontalScaler.GetComponent<RectTransform>());

            Button button = newLeagueItem.GetComponent<Button>();
            button.onClick.AddListener(() => OpenLeagueByID(league.leagueID));
        }

        GameObject addItem = Instantiate(addItemPrefab, contentPanel);
        Button addButton = addItem.GetComponent<Button>();
        addButton.onClick.AddListener(() => AddNewLeague());
    }

    public void OpenLeagueByID(int leagueID)
    {
        League selectedLeague = leagues.Find(league => league.leagueID == leagueID);
        if (selectedLeague != null)
        {
            OpenLeague(selectedLeague);
        }
        else
        {
            Debug.LogError("League with ID " + leagueID + " not found.");
        }
    }

    public void OpenLeague(League selectedLeague)
    {
        uiManager.OpenLeagueSummaryCenterUI();
        GameObject summaryScreen = GameObject.Find("UILeagueSummaryScreen");
        LeagueSummaryCenterUIManager leagueSummaryCenterUI = summaryScreen.GetComponent<LeagueSummaryCenterUIManager>();
        leagueSummaryCenterUI.Display(selectedLeague);
    }

    public void AddNewLeague()
    {
        uiManager.OpenAddNewLeagueScreen();
    }
}

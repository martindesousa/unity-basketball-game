using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeagueSummaryCenterUIManager : MonoBehaviour
{
    public Transform teamListContentPanel;
    public GameObject teamItemPrefab;

    public void Display(League league)
    {
        DisplayTeams(league);
    }
    
    public void DisplayTeams(League league)
    {
        foreach (Transform child in teamListContentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (BallTeam team in league.teams)
        {
            GameObject teamItem = Instantiate(teamItemPrefab, teamListContentPanel);
            TextMeshProUGUI textMesh = teamItem.GetComponent<TextMeshProUGUI>();
            textMesh.enabled = true;
            textMesh.text = team.teamName;
        }
    }
}

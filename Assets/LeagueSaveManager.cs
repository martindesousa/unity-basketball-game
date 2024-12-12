using System.IO;
using UnityEngine;

public class LeagueManager : MonoBehaviour
{
    public League league;

    public void SaveLeague()
    {
        string json = JsonUtility.ToJson(league, true);
        File.WriteAllText(Application.dataPath + "/Resources/league.json", json);
    }

    public void LoadLeague()
    {
        string json = File.ReadAllText(Application.dataPath + "/Resources/league.json");
        JsonUtility.FromJsonOverwrite(json, league);
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallPlayer
{
    public string playerName;
    public int playerID;
    public string position;
    public int jerseyNumber;
    // Add other player-specific fields
}

[System.Serializable]
public class BallTeam
{
    public string teamName;
    public int teamID;
    public string coachName;
    public BallPlayer[] players;
    // Add other team-specific fields
}

[System.Serializable]
[CreateAssetMenu(fileName = "League", menuName = "League")]
public class League : ScriptableObject
{
    public string leagueName;
    public int leagueID;
    public Sprite leagueIcon;
    public int year;
    public BallTeam[] teams;
    public BallPlayer[] players;
}

[System.Serializable]
public class LeagueList
{
    public List<League> leagues;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsManager : MonoBehaviour
{

    public static LeaderboardsManager sharedInstance;

    public List<Leaderboard> m_Leaderboards;
    public int m_UserScore;

    private void Awake()
    {
        sharedInstance = this;
        m_Leaderboards = new List<Leaderboard>();
    }

    public void AddLeaderboard(Leaderboard leaderboard)
    {
        if(m_UserScore > 0)
        {
            for(int i = 0; i < leaderboard.GetCount(); i++) 
            {
                if(leaderboard.GetLeaderboardEntryAtIndex(i).Score == m_UserScore)
                {
                    leaderboard.GetLeaderboardEntryAtIndex(i).IsUserScore = true;
                    break;
                }
            }
        }

        m_Leaderboards.RemoveAll(p => p.Name == leaderboard.Name);

        m_Leaderboards.Add(leaderboard);
    }

    public Leaderboard GetLeaderboardByName(string name)
    {
        for(int i = 0; i < m_Leaderboards.Count; i++) 
        {
            if (m_Leaderboards[i].Name == name)
            {
                return m_Leaderboards[i];
            }
            
        }
        return null;
    }

    public int GetCount()
    {
        return m_Leaderboards.Count;
    }

}

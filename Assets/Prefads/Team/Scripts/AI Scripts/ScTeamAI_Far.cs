using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////
/// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
/// Author : 	Miguel Angel Fernandez Graciani
/// Date :	2021-02-07
/// Observations :
///     - THIS IS AN ARTIFICIAL INTELLIGENT SCRIPT
///     - You must implement the necessary algorithm
///     - You must Change it to define the artiicial intelligence of this Team
/// </summary>
public class ScTeamAI_Far : MonoBehaviour {

    // Use this for initialization
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Start () {

        GameObject[] leaders = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        List<GameObject> enemyMinions = new List<GameObject>();
        GameObject[] profits = GameObject.FindGameObjectsWithTag("Profit");
        GameObject TeamLeader = new GameObject();
        ScPlayerAI_Far playerAI = new ScPlayerAI_Far();
        foreach (GameObject leader in leaders)
        {
            if (leader.GetComponent<ScPlayerControl>().Team == "Far")
            {
                TeamLeader = leader;
                playerAI = TeamLeader.GetComponent<ScPlayerAI_Far>();
                playerAI.setProfits(profits);
            }
        }
        int i = 0;
        foreach (GameObject minion in minions)
        {
            if (minion.GetComponent<ScMinionControl>().Team == "Far")
            {
                minion.GetComponent<ScMinionAI_Far>().setTUID(i);
                minion.GetComponent<ScMinionAI_Far>().setLeader(TeamLeader);
                i++;
            }
            else
            {
                enemyMinions.Add(minion);
            }
        }

        playerAI.setEnemyMinions(enemyMinions);

    }  // FIn de - void Start()

    // Update is called once per frame
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Update () {

    }  // FIn de - void Update()

}  // Fin de - public class ScTeamAI_Far : MonoBehaviour {

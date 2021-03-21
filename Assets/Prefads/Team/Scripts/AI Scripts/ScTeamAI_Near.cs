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
public class ScTeamAI_Near : MonoBehaviour {

    // Use this for initialization
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Start()
    {
        GameObject[] leaders = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
        GameObject TeamLeader = new GameObject();
        foreach (GameObject leader in leaders)
        {
            if (leader.GetComponent<ScPlayerControl>().Team=="Near")
            {
                TeamLeader = leader;
            }
        }
        int i = 0;
        foreach(GameObject minion in minions){
            if (minion.GetComponent<ScMinionControl>().Team == "Near")
            {
                minion.GetComponent<ScMinionAI_Near>().setTUID(i);
                minion.GetComponent<ScMinionAI_Near>().setLeader(TeamLeader);
                i++;
            }
        }
    }  // FIn de - void Start()

    // Update is called once per frame
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Update()
    {

    }  // FIn de - void Update()

}  // Fin de - public class ScTeamAI_Near : MonoBehaviour {
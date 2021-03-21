using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////
/// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
/// Author : 	Miguel Angel Fernandez Graciani
/// Date :	2021-02-07
/// Observations :
///     - THIS IS AN ARTIFICIAL INTELLIGENT SCRIPT
///     - You must call to the "public void moveOn(Vector3 directionForce, float movUnits)" in "ScMinionControl" to move the minion
///     - You must Change it to define the artiicial intelligence of this minion
/// </summary>
public class ScMinionAI_Far : MonoBehaviour {

    protected DateTime date_lastChamge;  // 
    protected double periodMilisec;

    public Vector3 movement;  // Direction of the force that will be exerted on the gameobject
    public float minionsMovUnits;  //  Amount of force that will be exerted on the gameobject


    // Use this for initialization
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Start () {

        date_lastChamge = DateTime.Now; // We initialize the date value
        periodMilisec = 1000f;  // We change each "periodoMiliseg"/1000 seconds

        movement = new Vector3(0.0f, 0.0f, 0.0f); // We initialize the date value
        minionsMovUnits = 1f; // We initialize the date value
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

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////
    /// ///////////  FixedUpdate()
    /// Author : 	Miguel Angel Fernandez Graciani
    /// Date :	2021-02-07
    /// Observations :
    ///     - THIS IS AN ARTIFICIAL INTELLIGENT SCRIPT
    ///     - You must Change it to define the artiicial intelligence of this player
    ///     - This one is only an example to manage the player
    /// </summary>
    void FixedUpdate()
    {
        // Every "timeWhitoutChange_ms" milliseconds we modify the value of "movement" and "minionsMovUnits"
        DateTime dateNow = DateTime.Now; 
        TimeSpan timeWhitoutChange = dateNow - date_lastChamge;

        double timeWhitoutChange_ms = timeWhitoutChange.TotalMilliseconds;

        if (timeWhitoutChange_ms > periodMilisec)
        {
            // We calculate the direction and quantity of movement
            // We obtain "movement" and "minionsMovUnits" randonly
            float move_X = Random.Range(-1.0f, 1f);
            float move_Z = Random.Range(-1f, 1f);
            float minionsMovUnits = Random.Range(0.0f, 1f);

            minionsMovUnits = minionsMovUnits * ScGameGlobalData.maxMinionsMovUnits;
            movement = new Vector3(move_X, 0.0f, move_Z);

            date_lastChamge = dateNow;  // We actualizate date_lastChamge
        }

        // CALLING TO THIS FUNCTION YOU CAN MANAGE THE ELEMENT WITH THE ARTIFICIAL INTELLIGENCE THAT YOU MUST DEVELOP
        GetComponent<ScMinionControl>().moveOn(movement, minionsMovUnits);
    }  // Fin de - void FixedUpdate()

}  // Fin de - public class ScMinionAI_Far : MonoBehaviour {

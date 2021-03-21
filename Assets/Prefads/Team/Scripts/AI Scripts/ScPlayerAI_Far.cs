using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////
/// ///////////  ARTIFICIAL INTELLIGENCE  player Script 
/// Author : 	Miguel Angel Fernandez Graciani
/// Date :	2021-02-07
/// Observations :
///     - THIS IS AN ARTIFICIAL INTELLIGENT SCRIPT
///     - You must call to the "public void moveOn(Vector3 directionForce, float movUnits)" in "ScPlayerControl" to move the player 
///     - You must Change it to define the artiicial intelligence of this player
/// </summary>
public class ScPlayerAI_Far : MonoBehaviour {

    DateTime date_lastChamge;
    protected double periodMilisec;

    public Vector3 movement;  // Direction of the force that will be exerted on the gameobject
    public float playersMovUnits;  //  Amount of force that will be exerted on the gameobject

    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Start()
    {
        date_lastChamge = DateTime.Now; // We initialize the date value
        periodMilisec = 1000f;  // We change each "periodoMiliseg"/1000 seconds

        movement = new Vector3(0.0f, 0.0f, 0.0f); // We initialize the date value
        playersMovUnits = 1f; // We initialize the date value
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

        if (ScGameGlobalData.Team_Far_Control == "manual")
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //        float moveHorizontal = 0;
            //        float moveVertical = 0;
            //        if (Input.GetKeyDown("i")) { moveVertical = 1f; }
            //        if (Input.GetKeyDown("k")) { moveVertical = -1f; }
            //        if (Input.GetKeyDown("j")) { moveHorizontal = -1f; }
            //        if (Input.GetKeyDown("l")) { moveHorizontal = 1f; }

            // We calculate the direction and quantity of movement
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            playersMovUnits = 25f;
        } // Fin de - if (ScGameGlobalData.Team_Far_Control == "manual")
        else if (ScGameGlobalData.Team_Far_Control == "randon")
        {
            if (timeWhitoutChange_ms > periodMilisec)
            {
                // We calculate the direction and quantity of movement
                // We obtain "movement" and "minionsMovUnits" randonly
                float move_X = Random.Range(-1.0f, 1f);
                float move_Z = Random.Range(-1f, 1f);
                playersMovUnits = Random.Range(0.0f, 1f);

                playersMovUnits = playersMovUnits * ScGameGlobalData.maxPlayersMovUnits;
                movement = new Vector3(move_X, 0.0f, move_Z);

                date_lastChamge = dateNow;  // We actualizate date_lastChamge
            }
        } // Fin de - else if (ScGameGlobalData.Team_Far_Control == "randon")
        else if (ScGameGlobalData.Team_Far_Control == "ai")
        {
            Debug.Log("From ScPlayerAI_Far => FixedUpdate => AI is not programated");
        }// Fin de - else if (ScGameGlobalData.Team_Far_Control == "randon")
        else { Debug.Log("From ScPlayerAI_Far => FixedUpdate => Error 001"); }

        // CALlING TO THIS FUNCTION YOU CAN MANAGE THE ELEMENT WITH THE ARTIFICIAL INTELLIGENCE THAT YOU MUST DEVELOP
        GetComponent<ScPlayerControl>().moveOn(movement, playersMovUnits);
    }  // Fin de - void FixedUpdate()

}  // Fin de - public class ScPlayerAI_Far : MonoBehaviour {

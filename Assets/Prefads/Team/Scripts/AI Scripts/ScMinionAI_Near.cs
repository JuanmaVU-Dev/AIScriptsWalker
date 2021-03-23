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
public class ScMinionAI_Near : MonoBehaviour {

    protected DateTime date_lastChamge;  // 
    protected double periodMilisec;

    public Vector3 movement;  // Direction of the force that will be exerted on the gameobject
    public float minionsMovUnits;  //  Amount of force that will be exerted on the gameobject

    private int TUID;
    private Vector2 ai_orbitalOffset;
    private GameObject leader;

    // Use this for initialization
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Start()
    {

        date_lastChamge = DateTime.Now; // We initialize the date value
        periodMilisec = 1500f;  // We change each "periodoMiliseg"/1000 seconds

        movement = new Vector3(0.0f, 0.0f, 0.0f); // We initialize the date value
        minionsMovUnits = 1f; // We initialize the date value

        float ia_angle = (360f / ScGameGlobalData.numOfMinions) * TUID;
        ai_orbitalOffset = new Vector2(Mathf.Sin(ia_angle), Mathf.Cos(ia_angle));
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
        if (ScGameGlobalData.Team_Near_Control == "randon")
        {
            if (timeWhitoutChange_ms > periodMilisec)
            {
                // We calculate the direction and quantity of movement
                // We obtain "movement" and "minionsMovUnits" randonly
                float move_X = Random.Range(-1.0f, 1f);
                float move_Z = Random.Range(-1f, 1f);
                minionsMovUnits = Random.Range(0.0f, 1f);

                minionsMovUnits = minionsMovUnits * ScGameGlobalData.maxMinionsMovUnits;
                movement = new Vector3(move_X, 0.0f, move_Z);

                date_lastChamge = dateNow;  // We actualizate date_lastChamge
            }
        }
        else if (ScGameGlobalData.Team_Near_Control == "ai")
        {
            float move_X = gameObject.transform.position.x - (leader.transform.position.x+(ai_orbitalOffset.x*10f));
            float move_Z = gameObject.transform.position.z - (leader.transform.position.z + (ai_orbitalOffset.y * 10f));
            movement = new Vector3(move_X, 0.0f, move_Z).normalized;

            float distance= Vector3.Distance(gameObject.transform.position, leader.transform.position);
            minionsMovUnits = Vector3.Distance(gameObject.transform.position, leader.transform.position);
            if (distance>5f)
            {
                minionsMovUnits = 1 * ScGameGlobalData.maxMinionsMovUnits;
            }
            else
            {
                minionsMovUnits = (distance/5f) * ScGameGlobalData.maxMinionsMovUnits;
                minionsMovUnits = easeInOutBack(minionsMovUnits);
                //minionsMovUnits = Mathf.Sin((minionsMovUnits * (float) Math.PI) / 2);
            }
            
            //Debug.Log("From ScPlayerAI_Far => FixedUpdate => AI is not programated");
        }// Fin de - else if (ScGameGlobalData.Team_Far_Control == "randon")
        // CALLING TO THIS FUNCTION YOU CAN MANAGE THE ELEMENT WITH THE ARTIFICIAL INTELLIGENCE THAT YOU MUST DEVELOP
        GetComponent<ScMinionControl>().moveOn(movement, minionsMovUnits);
    }  // Fin de - void FixedUpdate()

    public void setTUID(int id)
    {
        TUID = id;
    }

    float easeInOutBack(float speed)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        return speed < 0.5
          ? (float)(Math.Pow(2 * speed, 2) * ((c2 + 1) * 2 * speed - c2)) / 2
          : (float)(Math.Pow(2 * speed - 2, 2) * ((c2 + 1) * (speed * 2 - 2) + c2) + 2) / 2;
    }

    public void setLeader(GameObject leader)
    {
        this.leader = leader;
    }
}  // Fin de - public class ScMinionAI_Near : MonoBehaviour {
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
public class ScPlayerAI_Near : MonoBehaviour {

    DateTime date_lastChamge;
    protected double periodMilisec;

    public Vector3 movement;  // Direction of the force that will be exerted on the gameobject
    public float playersMovUnits;  //  Amount of force that will be exerted on the gameobject

    private GameObject[] profits;
    List<GameObject> enemyMinions;

    float m_MaxDistance;
    bool m_HitDetect;

    Collider m_Collider;
    RaycastHit m_Hit;

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


        m_MaxDistance = 300.0f;
        m_Collider = GetComponent<Collider>();
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

        if (ScGameGlobalData.Team_Near_Control == "manual")
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
        }  // Fin de - if (ScGameGlobalData.Team_Far_Control == "manual")
        else if (ScGameGlobalData.Team_Near_Control == "randon")
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
        else if (ScGameGlobalData.Team_Near_Control == "ai")
        {
            GameObject nearestProfit = getNearestProfit();

            GameObject nearestEnemyMinion = getNearestEnemy();

            movement = -(nearestProfit.transform.position - transform.position).normalized;

            playersMovUnits = 25f;

            m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, -movement, out m_Hit, transform.rotation, m_MaxDistance);
            if (m_HitDetect)
            {
                //Output the name of the Collider your Box hit
                
                if (m_Hit.transform.tag == "Minion" && m_Hit.transform.gameObject.GetComponent<ScMinionControl>().Team == "Far")
                {
                    Debug.Log("------------------Hit minion enemy------------------------");
                    movement = -movement;
                    playersMovUnits = 25f;
                }
            }

            if (Vector3.Distance(transform.position, nearestEnemyMinion.transform.position) < 6f){
                movement = (transform.position - nearestEnemyMinion.transform.position);
                playersMovUnits = 25f;
            }

        } // Fin de - else if (ScGameGlobalData.Team_Far_Control == "randon")
        else { Debug.Log("From ScPlayerAI_Near => FixedUpdate => Error 001"); }

        // CALlING TO THIS FUNCTION YOU CAN MANAGE THE ELEMENT WITH THE ARTIFICIAL INTELLIGENCE THAT YOU MUST DEVELOP
        GetComponent<ScPlayerControl>().moveOn(movement, playersMovUnits);
    }  // Fin de - void FixedUpdate()

    private GameObject getNearestProfit()
    {
        GameObject nearestProfit = profits[0];
        float profitDistance = Vector3.Distance(this.transform.position, profits[0].transform.position);
        foreach (GameObject profit in profits)
        {
            if (Vector3.Distance(this.transform.position, profit.transform.position) < profitDistance)
            {
                nearestProfit = profit;
                profitDistance = Vector3.Distance(this.transform.position, profit.transform.position);
            }
        }

        return nearestProfit;
    }

    private GameObject getNearestEnemy()
    {
        GameObject nearestEnemy = enemyMinions.ToArray()[0];
        float enemyDistance = Vector3.Distance(this.transform.position, nearestEnemy.transform.position);
        foreach (GameObject enemy in enemyMinions)
        {
            if (Vector3.Distance(this.transform.position, enemy.transform.position) < enemyDistance)
            {
                nearestEnemy = enemy;
                enemyDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
            }
        }

        return nearestEnemy;
    }

    public void setProfits(GameObject[] profits)
    {
        this.profits = profits;
    }


    public void setEnemyMinions(List<GameObject> enemyMinions)
    {
        this.enemyMinions = enemyMinions;
    }

    float InOutQuadBlend(float t)
    {
        if (t <= 0.5f)
            return 2.0f * t * t;
        t -= 0.5f;
        return 2.0f * t * (1.0f - t) + 0.5f;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, -movement * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + -movement * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, -movement * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + -movement * m_MaxDistance, transform.localScale);
        }
    }

}  // Fin de - public class ScPlayerAI_Near : MonoBehaviour {
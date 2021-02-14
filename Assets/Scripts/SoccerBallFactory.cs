using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoccerBallFactory 
{
    // step 1. private static instance
    private static SoccerBallFactory m_instance = null;

    // prefab references
    private GameObject soccerBall;

    // step 2. make constructor private
    private SoccerBallFactory()
    {
        _Initialize();
    }

    // step 3. make a pubic static creational method for class access
    public static SoccerBallFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new SoccerBallFactory();
        }

        return m_instance;
    }

    private void _Initialize()
    {
        // 4. create a Resources folder
        // 5. move our Prefabs folder into the Resources folder
        soccerBall = Resources.Load("Prefabs/SoccerBall") as GameObject;
    }

    public GameObject CreateSoccerBall()
    {
        GameObject tempSoccerBall = MonoBehaviour.Instantiate(soccerBall);
        tempSoccerBall.SetActive(false);
        return tempSoccerBall;
    }
}

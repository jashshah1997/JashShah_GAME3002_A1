using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoccerBallManager
{
    // step 1. create a private static instance
    private static SoccerBallManager m_instance = null;
    public int goal_count = 0;

    // step 2. make our default constructor private
    private SoccerBallManager()
    {
        
    }

    // step 3. make a public static creational method for class access
    public static SoccerBallManager Instance()
    {
        if (m_instance == null)
        {
            m_instance = new SoccerBallManager();
        }
        return m_instance;
    }
    
    public int MaxBullets { get; set; }

    private Queue<GameObject> m_soccerBallPool;
    public GameObject activeBall;

    public void Init(int max_soccerballs = 50)
    {   // step 4 initialize class variables and start the bullet pool build
        MaxBullets = max_soccerballs;
        _BuildBulletPool();
        goal_count = 0;
    }

    public void IncreaseGoals()
    {
        goal_count++;
    }

    public int GetGoalCount()
    {
        return goal_count;
    }

    public int GetMaximumGoals()
    {
        return MaxBullets;
    }

    private void _BuildBulletPool()
    {
        // create empty Queue structure
        m_soccerBallPool = new Queue<GameObject>();

        for (int count = 0; count < MaxBullets; count++)
        {
            var tempBall = SoccerBallFactory.Instance().CreateSoccerBall();
            m_soccerBallPool.Enqueue(tempBall);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion direction)
    {
        var newBullet = m_soccerBallPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        newBullet.GetComponent<Rigidbody>().rotation = direction;
        activeBall = newBullet;
        return newBullet;
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation, float initial_velocity)
    {
        var newBullet = m_soccerBallPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        newBullet.GetComponent<Rigidbody>().rotation = rotation;
        Vector3 v = new Vector3(initial_velocity, 0, 0);
        newBullet.GetComponent<Rigidbody>().velocity = rotation * v;
        activeBall = newBullet;
        return newBullet;
    }

    public bool HasRemaningSoccerBalls()
    {
        return m_soccerBallPool.Count > 0;
    }

    public void ReturnSoccerBalls(GameObject returnedBall)
    {
        //returnedBall.SetActive(false);
        //m_soccerBallPool.Enqueue(returnedBall);
        returnedBall.GetComponent<SoccerBallController>().m_is_active = false;
    }

    public int RemainingSoccerBalls()
    {
        return m_soccerBallPool.Count;
    }
}

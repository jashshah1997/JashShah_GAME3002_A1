using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoccerBallController: MonoBehaviour
{
    public bool m_in_goal = false;
    public bool m_is_active = true;
    private float start_time;

    void Start()
    {
        start_time = Time.time;     
    }


    // Update is called once per frame
    void Update()
    {
        _CheckBounds();
        if ((Time.time - start_time) > 6)
        {
            SoccerBallManager.Instance().ReturnSoccerBalls(gameObject);
        }

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 1e-1)
        {
            SoccerBallManager.Instance().ReturnSoccerBalls(gameObject);
        } 
    }

    private void _CheckBounds()
    {
        if (transform.position.y < -1)
        {
            SoccerBallManager.Instance().ReturnSoccerBalls(gameObject);
        }
    }

    public void InGoal()
    {
        if (!m_in_goal)
        {
            m_in_goal = true;
            SoccerBallManager.Instance().IncreaseGoals();
            m_is_active = false;
        }
    }

}

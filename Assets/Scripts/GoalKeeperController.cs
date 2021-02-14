using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoalKeeperController: MonoBehaviour
{

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        var active_ball = SoccerBallManager.Instance().activeBall;
        if (active_ball == null)
        {
            return;
        }

        var ball_tf = active_ball.transform.position;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3((ball_tf.x - gameObject.transform.position.x) * ParameterManager.GAME_DIFFICULTY, 0.0f, 0.0f);
        var curr_pos = gameObject.transform.position;
        if (curr_pos.x > 10)
        {
            gameObject.transform.position = new Vector3(10.0f, curr_pos.y, curr_pos.z);
        } else if (gameObject.transform.position.x < -10)
        {
            gameObject.transform.position = new Vector3(-10.0f, curr_pos.y, curr_pos.z);
        }
    }

}

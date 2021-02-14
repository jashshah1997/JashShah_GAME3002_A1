using UnityEngine;
using UnityEngine.UI;

public class NetController : MonoBehaviour
{
    Dropdown m_Dropdown;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            other.gameObject.GetComponent<SoccerBallController>().InGoal();
        }
    }
}
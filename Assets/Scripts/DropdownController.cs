using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    Dropdown m_Dropdown;

    void Start()
    {
        ParameterManager.GAME_DIFFICULTY = 2.0f;
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown change)
    {
        ParameterManager.GAME_DIFFICULTY = 2.0f * change.value;
    }
}
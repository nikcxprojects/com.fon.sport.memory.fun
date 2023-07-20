using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsText : MonoBehaviour
{

    [SerializeField] private string playerPrefsname;
    
    void Start()
    {
        GetComponent<Text>().text += $" {PlayerPrefs.GetInt(playerPrefsname)} LVL";
    }

}

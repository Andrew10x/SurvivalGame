using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InValues : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Axe"))
        {
            PlayerPrefs.SetInt("Axe", 0);
        }
        if (!PlayerPrefs.HasKey("Revolver"))
        {
            PlayerPrefs.SetInt("Revolver", -1);
        }
        if (!PlayerPrefs.HasKey("Shotgun"))
        {
            PlayerPrefs.SetInt("Shotgun", -1);
        }
        if (!PlayerPrefs.HasKey("AsRiffle"))
        {
            PlayerPrefs.SetInt("AsRiffle", -1);
        }
        if (!PlayerPrefs.HasKey("coinCount"))
        {
            PlayerPrefs.SetInt("coinCount", 0);
        }
        /*PlayerPrefs.SetInt("coinCount", 10000);
        PlayerPrefs.SetInt("Axe", 0);
        PlayerPrefs.SetInt("Revolver", -1);
        PlayerPrefs.SetInt("Shotgun", -1);
        PlayerPrefs.SetInt("AsRiffle", -1);*/
    }
}

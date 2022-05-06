using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public Weapon[] wepArr;

    public void reset()
    {
        PlayerPrefs.SetInt("coinCount", 10000);
        PlayerPrefs.SetInt("Axe", 0);
        PlayerPrefs.SetInt("Revolver", -1);
        PlayerPrefs.SetInt("Shotgun", -1);
        PlayerPrefs.SetInt("AsRiffle", -1);

        for(int i=0; i<wepArr.Length; i++)
        {
            wepArr[i].drawWeapon();
        }
    }
}

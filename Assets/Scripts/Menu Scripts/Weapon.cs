
using UnityEngine;
using UnityEngine.UI;

public class Weapon: MonoBehaviour
{
    public Text damageText;
    public Text buyText;
    public Text countText;
    public Text TotalCountText;
    public GameObject coinCont;
    public MyWeapon mw;
    public int wNumber;
    string wName;
    public void Start()
    {
        if (wNumber == 0)
        {
            wName = "Axe";
            mw = new Axe();
        }
        else if (wNumber == 1)
        {
            wName = "Revolver";
            mw = new Revolver();
        }
        else if (wNumber == 2)
        {
            wName = "Shotgun";
            mw = new Shotgun();
        }
        else
        {
            wName = "AsRiffle";
            mw = new AsRiffle();
        }

        drawWeapon();
    }

    public void buyOrUpgradeWeapon()
    {
        int weaponLevel = PlayerPrefs.GetInt(wName);

        int coins = PlayerPrefs.GetInt("coinCount");
        if (weaponLevel >= 3 || mw.weaponCount[weaponLevel + 1] > coins)
        {
            return;
        }
        PlayerPrefs.SetInt(wName, weaponLevel + 1);
        PlayerPrefs.SetInt("coinCount", coins - mw.weaponCount[weaponLevel + 1]);
        Debug.Log(wName + " " + weaponLevel);
        drawWeapon();
        Debug.Log(weaponLevel);

    }

    public void drawWeapon()
    {
        TotalCountText.text = (PlayerPrefs.GetInt("coinCount")).ToString();
        int weaponLevel = PlayerPrefs.GetInt(wName);
        if (weaponLevel == -1)
        {
            coinCont.SetActive(true);
            damageText.text = mw.weaponDamage[0].ToString();
            buyText.text = "Buy";
            countText.text = mw.weaponCount[0].ToString();

        }
        else
        {
            damageText.text = mw.weaponDamage[weaponLevel].ToString();
            if (weaponLevel < 3)
            {
                buyText.text = "Upgrade";
                countText.text = mw.weaponCount[weaponLevel + 1].ToString();
            }
            else
            {
                buyText.text = "Max Upgrade";
                coinCont.SetActive(false);
            }
        }
    }
}

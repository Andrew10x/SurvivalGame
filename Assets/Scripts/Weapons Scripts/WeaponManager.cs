
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

	void Start () {
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
	}

	void Update () {

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerPrefs.GetInt("Revolver") != -1) {
            TurnOnSelectedWeapon(1);
        }
    
        if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerPrefs.GetInt("Shotgun") != -1) {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && PlayerPrefs.GetInt("AsRiffle") != -1) {
            TurnOnSelectedWeapon(3);
        }

    }

    void TurnOnSelectedWeapon(int weaponIndex) {

        if (current_Weapon_Index == weaponIndex)
            return;

        // turn of the current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);

        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        // store the current selected weapon index
        current_Weapon_Index = weaponIndex;

    }

    public WeaponHandler GetCurrentSelectedWeapon() {
        return weapons[current_Weapon_Index];
    }

}



































using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;
    private float st = 0f;
    public float fireRate = 15f;
    private float nextTimeToFire;
    private float damage = 50f;

    private MyWeapon wp = new Axe();
    private MyWeapon wa = new Axe();
    private MyWeapon wr = new Revolver();
    private MyWeapon ws = new Shotgun();
    private MyWeapon war = new AsRiffle();

    private Animator zoomCameraAnim;
    private GameObject crosshair;
    private Camera mainCam;

    void Awake() {

        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT)
                                  .transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        mainCam = Camera.main;
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
    }

	void Update () {
        WeaponShoot();
        ZoomInAndOut();
    }

    public void incSt()
    {
        if(st <100)
            st += 10f;
    }

    void WeaponShoot() {

        // if we have assault riffle
        if(weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE) {
            wp = war;
            damage = wp.getDamage();

            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                 BulletFired();

            }


            // if we have a regular weapon that shoots once
        } else {

            if(Input.GetMouseButtonDown(0)) {

                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    wp = wa;
                    damage = wp.getDamage();
                }
                else if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.REVOLVER_TAG) {
                    wp = wr;
                    damage = wp.getDamage();
                }
                else if (weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.SHOTGUN_TAG)
                {
                    wp = ws;
                    damage = wp.getDamage();

                }
                Debug.Log(damage);

                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET) {

                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    Invoke("BulletFired", 0.35f);

                }
            } 
        }
    } 

    void ZoomInAndOut() {

        // we are going to aim with our camera on the weapon
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {


            if(Input.GetMouseButtonDown(1)) {

                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);

            }

            if (Input.GetMouseButtonUp(1)) {

                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);

            }

        } // if we need to zoom the weapon

    }

    void BulletFired() {

        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

            if(hit.transform.tag == Tags.ENEMY_TAG) {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }

        }

    }

}
































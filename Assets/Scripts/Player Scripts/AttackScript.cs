
using UnityEngine;

public class AttackScript : MonoBehaviour {

    public float damage = 2f;
    public float radius = 1f;
    public bool isAxe;
    public LayerMask layerMask;
	
	void Update () {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if(hits.Length > 0) {
            if (isAxe)
            {
                MyWeapon wp = new Axe();
                damage = wp.getDamage();
            }
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);

        }

	}

}





























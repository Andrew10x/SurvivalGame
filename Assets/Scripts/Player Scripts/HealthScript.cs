using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;

    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool isPlayer, isBoar, isCannibal;
    private bool is_Dead;
    void Awake()
    {
        if(isBoar || isCannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        else if (isPlayer)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;
        health -= damage;

        if(isPlayer)
        {

        }
        if(isCannibal || isBoar)
        {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if(health <= 0)
        {
            PlayerDied();
            is_Dead = true;
        }

    }

    void PlayerDied()
    {
        if (isCannibal)
        {
            print("Hello");
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 500f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;
        }

        if (isBoar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemyAnim.Dead();
        }

        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for(int i=0; i<enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke(nameof(TurnOffGameObject), 1.5f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);    
    }
}

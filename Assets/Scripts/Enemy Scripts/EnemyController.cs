using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent nav_Agent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;
    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    private void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        nav_Agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }
    void Start()
    {
        enemy_State = EnemyState.PATROL;
        patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;
        current_Chase_Distance = chase_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }
        else if(enemy_State == EnemyState.CHASE)
        {
            Chase();
        }
        else if(enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        nav_Agent.isStopped = false;
        nav_Agent.speed = walk_Speed;

        patrol_Timer += Time.deltaTime;

        if(patrol_Timer >= patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }

        if(nav_Agent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        if(Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;
        }
    }

    void Chase()
    {
        nav_Agent.isStopped = false;
        nav_Agent.speed = run_Speed;
        nav_Agent.SetDestination(target.position);

        if (nav_Agent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }

        if(Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            patrol_Timer = patrol_For_This_Time;
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }



    }

    void Attack()
    {
        nav_Agent.velocity = Vector3.zero;
        nav_Agent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();
            attack_Timer = 0f;
        }

        if(Vector3.Distance(transform.position, target.position) >
            attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        nav_Agent.SetDestination(navHit.position);
    }
}

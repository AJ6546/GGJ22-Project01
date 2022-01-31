using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Vector3 targetPos;
    [SerializeField] GameObject target;
    [SerializeField] List<PlayerController> players = new List<PlayerController>();
    CooldownTimer cd;
    Animator anim;
    [SerializeField]
    float chaseDistance = 10f, stoppingDistance = 1, distanceToTarget,
        damagingDistance = 1;
    [SerializeField] int damage;
    NavMeshAgent navMeshAgent;
    [SerializeField] float cooldown = 10, nextAttack = 0;
    [SerializeField] bool p1 = true, p2 = true,p3 = true;
    [SerializeField] GameObject enemy1, enemy2;
    void Start()
    {
        cd = GetComponent<CooldownTimer>();
        anim = GetComponent<Animator>();
        players = GetAllPlayers();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private List<PlayerController> GetAllPlayers()
    {
        List<PlayerController> temp = new List<PlayerController>();
        foreach (PlayerController ec in FindObjectsOfType<PlayerController>())
        {
            temp.Add(ec);
        }
        return temp;
    }
    private PlayerController FindNearestPlayer()
    {
        float minDist = Mathf.Infinity;
        PlayerController nearestEnemy = null;
        foreach (PlayerController player in players)
        {
            float dist = FindDistance(player);
            if (minDist > dist)
            {
                nearestEnemy = player;
                minDist = dist;
            }
        }
        return nearestEnemy;
    }

    private float FindDistance(PlayerController player)
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }
    void Update()
    {
        if (GetComponent<Health>().isDead) return;
        target = FindNearestPlayer().gameObject;
        targetPos = target.transform.position;
        if (AtAttackingDistance())
        {
            navMeshAgent.isStopped = true;
            if (nextAttack < Time.time)
            {
                Attack();
                nextAttack = cooldown + Time.time;
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
            if (AtChasingDistance())
                navMeshAgent.SetDestination(targetPos);
        }
        float hf = GetComponent<Health>().GetHealthFactor();
        if(hf<=0.75)
        {
            if (p1)
                Phase();
            p1 = false;
        }
        if (hf <= 0.5)
        {
            if (p2)
                Phase();
            p2 = false;
        }
        if (hf <= 0.25)
        {
            if (p3)
                Phase();
            p3= false;
        }
    }
    public bool AtAttackingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= stoppingDistance;
    }
    public bool AtChasingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= chaseDistance;
    }
    public bool AtDamagingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= damagingDistance;
    }
    public void Attack()
    {
        int i = Random.Range(1, 5);
        if (i == 1 || i == 3)
            damage = 10;
        else if (i == 2)
            damage = 12;
        else
            damage = 15;
        anim.SetTrigger("Attack" + i);               
    }
    void Hit()
    {
        if (AtDamagingDistance())
            target.GetComponent<Health>().TakeDamage(damage);
    }
    void Phase()
    {
        transform.position = new Vector3(Random.Range(0, 100), 0.1f, Random.Range(0, 100));
        Vector3 spawnPos = new Vector3(Random.Range(0, 100), 0.1f, Random.Range(0, 100));
        Instantiate(enemy1, spawnPos, enemy1.transform.rotation);
        spawnPos = new Vector3(Random.Range(0, 100), 0.1f, Random.Range(0, 100));
        Instantiate(enemy2, spawnPos, enemy2.transform.rotation);
    }
}

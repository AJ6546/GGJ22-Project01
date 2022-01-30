using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_BW : MonoBehaviour
{
    [SerializeField] float stoppingDistance = 5f, distanceToTarget;
    [SerializeField] Vector3 targetPos;
    [SerializeField] List<PlayerController> players = new List<PlayerController>();
    [SerializeField] PlayerController player;
    NavMeshAgent navMeshAgent;
    public string s = "";
    [SerializeField] Projectiles ammo;
    [SerializeField] CooldownTimer cd;
    public Transform instantiatorTransform;
    [SerializeField] float fwSpawnPos;
    [SerializeField] float upSpawnPos;
    [SerializeField] float lftSpawnPos;
    Animator anim;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        players = GetPlayers();
        cd = GetComponent<CooldownTimer>();
        anim = GetComponent<Animator>();
        foreach (PlayerController p in players)
        {
            if(p.s==s)
            {
                player = p;
            }
        }
    }

    void Update()
    {
        if (GetComponent<Health>().isDead) return;
        targetPos = player.transform.position;
        transform.LookAt(player.transform);
        if (AtAttackingDistance())
        {
            navMeshAgent.isStopped = true;
            Attack();
        }
        else
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(targetPos);
        }
    }
    private List<PlayerController> GetPlayers()
    {
        List<PlayerController> temp = new List<PlayerController>();
        foreach (PlayerController pc in FindObjectsOfType<PlayerController>())
        {
            temp.Add(pc);
        }
        return temp;
    }
    public bool AtAttackingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= stoppingDistance;
    }
    void Attack()
    {
        if (cd.nextAttackTime["EnemyAttack"] < Time.time &&  player.GetComponent<Health>().GetHealthFactor()>=0)
        {
            anim.SetTrigger("Attack_Defend");
            cd.nextAttackTime["EnemyAttack"] = cd.cooldownTimer["EnemyAttack"] + (int)Time.time;
        }
    }
    void Hit()
    {
        Vector3 spawnPos = instantiatorTransform.position + instantiatorTransform.forward * fwSpawnPos +
            instantiatorTransform.up * upSpawnPos + instantiatorTransform.right * lftSpawnPos;
        Projectiles projectileInstance = Instantiate(ammo,
        spawnPos, ammo.transform.rotation);
        projectileInstance.SetInstantiator(transform);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float stoppingDistance = 5f, distanceToTarget;
    [SerializeField] Vector3 targetPos;
    [SerializeField] PlayerController p;
    [SerializeField] List<PlayerController> players = new List<PlayerController>();
    NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        players = GetPlayers();
    }

    void Update()
    {
        targetPos = FindNearestPlayer().transform.position;
        
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
    public bool AtAttackingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= stoppingDistance;
    }

    public PlayerController FindNearestPlayer()
    {
        float minDist = Mathf.Infinity;
        PlayerController nearestPlayer = null;
        foreach (PlayerController player in players)
        {
            float dist = FindDistance(player);
            if(minDist > dist)
            {
                nearestPlayer = player;
                minDist = dist;
            }
        }
        return nearestPlayer;
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
    private float FindDistance(PlayerController player)
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
    void Attack()
    {
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}

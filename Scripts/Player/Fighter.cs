using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    [SerializeField] FixedButton attack_healButton;
    [SerializeField] GameObject fill;
    CooldownTimer cd;
    [SerializeField] int healPoints,damage;
    Animator anim;
    [SerializeField] Health otherPlayer;
    [SerializeField] float attackRange=1f;
    [SerializeField] GameObject target;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] UnityEvent attack_heal;
    void Start()
    {
        cd = GetComponent<CooldownTimer>();
        anim = GetComponent<Animator>();
        
    }

    private List<GameObject> GetAllEnemies()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (EnemyController_BW ec in FindObjectsOfType<EnemyController_BW>())
        {
            if(!ec.GetComponent<Health>().isDead) 
                temp.Add(ec.gameObject);
        }
        foreach (EnemyController ec in FindObjectsOfType<EnemyController>())
        {
            if (!ec.GetComponent<Health>().isDead)
                temp.Add(ec.gameObject);
        }
        return temp;
    }
    private GameObject FindNearestEnemy()
    {
        float minDist = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float dist = FindDistance(enemy);
            if (minDist > dist)
            {
                nearestEnemy = enemy;
                minDist = dist;
            }
        }
        return nearestEnemy;
    }

    private float FindDistance(GameObject enemy)
    {
        return Vector3.Distance(enemy.transform.position, transform.position);
    }

    void Update()
    {
        enemies = GetAllEnemies();
        if (FindNearestEnemy() != null)
            target = FindNearestEnemy();
        Refill();
        if (GetComponent<PlayerController>().s == "b")
        {
            if (cd.nextAttackTime["Heal"]
            < Time.time && (attack_healButton.Pressed || Input.GetKeyDown("2")))
            {
                attack_heal.Invoke();

                anim.SetTrigger("Attack_Heal");
                cd.nextAttackTime["Heal"] = cd.cooldownTimer["Heal"] + (int)Time.time;
                fill.GetComponent<Image>().fillAmount = 1;
            }
        }
        if (GetComponent<PlayerController>().s == "w")
        {
            if (cd.nextAttackTime["Attack2"]
            < Time.time && (attack_healButton.Pressed || Input.GetKeyDown("0")))
            {
                attack_heal.Invoke();
                anim.SetTrigger("Attack_Heal");
                cd.nextAttackTime["Attack2"] = cd.cooldownTimer["Attack2"] + (int)Time.time;
                fill.GetComponent<Image>().fillAmount = 1;
            }
        }
    }
    void Hit2()
    {
        if(GetComponent<PlayerController>().s == "b")
        {
            otherPlayer.RefillHealth(healPoints);
            GetComponent<Health>().RefillHealth(healPoints);
        }
        if (GetComponent<PlayerController>().s == "w")
        {
            if(InAttackRange())
            {
                target.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    bool InAttackRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    #region Refill
    void Refill()
    {
        if (fill.GetComponent<Image>().fillAmount > 0)
        {
            if (GetComponent<PlayerController>().s == "w")
            {
                fill.GetComponent<Image>().fillAmount -=
                  (1.0f / cd.cooldownTimer["Attack2"]) * Time.deltaTime;
                fill.GetComponentInChildren<Text>().text =
                    (cd.nextAttackTime["Attack2"] - (int)Time.time).ToString();
            }
            if (GetComponent<PlayerController>().s == "b")
            {
                fill.GetComponent<Image>().fillAmount -=
               (1.0f / cd.cooldownTimer["Heal"]) * Time.deltaTime;
                fill.GetComponentInChildren<Text>().text =
                    (cd.nextAttackTime["Heal"] - (int)Time.time).ToString();
            }
        }
        else { fill.GetComponentInChildren<Text>().text = ""; }
    }
    #endregion
}

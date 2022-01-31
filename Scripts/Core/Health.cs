using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 100, healthpoints;
    Animator anim;
    [SerializeField] GameObject obj;
    [SerializeField] GameManager gm;
    public bool isDead;
    void Start()
    {
        healthpoints = startHealth;
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
    }
    void Update()
    {

    }
    public void TakeDamage(int dmg)
    {
        if (isDead) return;
        healthpoints -= dmg;
        
        if (Mathf.Max(healthpoints, 0) == 0)
        {
            anim.SetTrigger("Death");
            isDead = true;
        }
    }
    void Death()
    {
        if(tag=="Enemy")
        {
            if (SceneManager.GetActiveScene().buildIndex == 11 && !FindObjectOfType<EnemyController>().GetComponent<Health>().isDead)
                return;
            if (SceneManager.GetActiveScene().buildIndex<6 || gm.killCount>= gm.reqkillCount)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, 1, transform.position.z);
                Instantiate(obj, spawnPos, obj.transform.rotation);
            }
            else
            {
                FindObjectOfType<GameManager>().killCount += 1;
                if(!GetComponent<EnemyController>())
                    StartCoroutine(Respawn());
            }
        }
        if(tag=="Player")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
    public float GetHealthFactor()
    {
        return healthpoints / startHealth;
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(10);
        transform.position = new Vector3(Random.Range(0, 100), 2, Random.Range(0, 100));
        anim.SetTrigger("Resurrect");
        healthpoints = startHealth;
    }
    public void RefillHealth(int healPoints)
    {
        if(healthpoints+ healPoints <= startHealth)
        {
            healthpoints += healPoints;
        }
        else
        {
            healthpoints = startHealth;
        }
    }
}

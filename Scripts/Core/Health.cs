using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 100, healthpoints;
    Animator anim;
    [SerializeField] GameObject obj;
    
    void Start()
    {
        healthpoints = startHealth;
        anim = GetComponent<Animator>();
    }
    void Update()
    {

    }
    public void TakeDamage(int dmg)
    {
        healthpoints -= dmg;
        if (Mathf.Max(healthpoints, 0) == 0)
        {
            anim.SetTrigger("Death");
        }
    }
    void Death()
    {
        if(tag=="Enemy")
        {
            Vector3 spawnPos = new Vector3(transform.position.x , 1, transform.position.z );
            Instantiate(obj, spawnPos, obj.transform.rotation);
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
}

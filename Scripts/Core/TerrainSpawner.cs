using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainSpawner : MonoBehaviour
{
    [SerializeField] TerrainSpawner terr1, terr2;
    TerrainSpawner terr;
    [SerializeField] float offset = 5;
    public string s;
    [SerializeField] float destroyAfter;
    [SerializeField] int num;
    int buildIndex;
    [SerializeField] GameObject obj;
    [SerializeField] string side="";
    void Start()
    {
        num = Random.Range(1, 100);
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (num % 10 == 0 && destroyAfter != Mathf.Infinity)
        {
            Vector3 spawnPos =new Vector3(transform.position.x+2.5f, 1, transform.position.z+2.5f);
            Instantiate(obj, spawnPos, obj.transform.rotation);
        }
    }
    void Update()
    {
        
    }
    IEnumerator Spawn(float waitTime)
    {
        num = Random.Range(1, 100);
        yield return new WaitForSeconds(waitTime);
        if(num%2==0)
            terr = terr1;
        else
            terr = terr2;
       

        Vector3 spawnPos = GetSpawnPose();

        TerrainSpawner newTerr = Instantiate(terr, spawnPos, terr.transform.rotation);
        if (newTerr.s==s)
            Destroy(gameObject, destroyAfter);
        else
        {
            StartCoroutine(Spawn(6));
            Destroy(newTerr.gameObject, 5);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (other.GetComponent<PlayerController>().s == s)
            { StartCoroutine(Spawn(1)); }
            else
            {
                if(destroyAfter!=Mathf.Infinity)
                    Destroy(gameObject, 1f);
            }
        }
    }
    Vector3 GetSpawnPose()
    {
        Vector3 spawnPos=new Vector3(0,0,0);
        switch(buildIndex)
        {
            case 2:
                spawnPos = transform.position + transform.forward * offset;
                break;
            case 3:
                if (Random.Range(1, 100) % 2 == 0)
                    offset = -offset;
                spawnPos = transform.position + transform.forward * offset;
                break;
            case 4:
                int num = Random.Range(1, 100);
                if (num < 33)
                    spawnPos = transform.position + transform.forward * offset;
                else if (num > 33 && num < 66)
                    spawnPos = transform.position + transform.forward * -offset;
                else
                {
                    if (side == "l")
                        spawnPos = transform.position + transform.right * -offset;
                    else
                        spawnPos = transform.position + transform.right * offset;
                }
                    
                break;
            case 10:
                spawnPos = transform.position + transform.forward * offset;
                break;
        }
        return spawnPos;
    }
}

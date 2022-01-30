using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Light : MonoBehaviour
{
    int buildIndex = 0;
    string score = "";
    [SerializeField] float destroyAfter, min, max, cooldown=10,nextPos,height=10;
   
    private void Start()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        if(buildIndex==1)
        {
            nextPos = cooldown + (int)Time.time;
        }
        else if (buildIndex == 5)
        {
            nextPos = cooldown + (int)Time.time;
        }
        else
        {
            Destroy(gameObject, destroyAfter);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            if (!score.Contains(other.GetComponent<PlayerController>().s))
            {
                FindObjectOfType<GameManager>().score += other.GetComponent<PlayerController>().s;
                if (buildIndex == 1)
                {
                    transform.position = new Vector3(Random.Range(min, max), transform.position.y, Random.Range(min, max));
                    nextPos = cooldown + (int)Time.time;
                }
                else if(buildIndex==5)
                {
                    transform.position = new Vector3(Random.Range(min, max), height, Random.Range(min, max));
                    nextPos = cooldown + (int)Time.time;
                }
                else
                {
                    Destroy(gameObject, destroyAfter);
                }
            }
        }
    }
    private void Update()
    {
        score = FindObjectOfType<GameManager>().score;
        if (score.Contains("w") && score.Contains("b"))
        {
            if (buildIndex != 11)
                SceneManager.LoadScene(buildIndex + 1);
            else
                FindObjectOfType<GameManager>().ShowCredits();
        }
        if((buildIndex == 1 || buildIndex==5 )&& Time.time>nextPos)
        {
            transform.position = new Vector3(Random.Range(min, max), transform.position.y, Random.Range(min, max));
            nextPos = cooldown + (int)Time.time;
        }
    }
}

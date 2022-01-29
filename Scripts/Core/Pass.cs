using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pass : MonoBehaviour
{
    [SerializeField] string score = "";
    int buildIndex = 0;
    private void Start()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            if(!score.Contains(other.GetComponent<PlayerController>().s))
                score += other.GetComponent<PlayerController>().s;
        }
    }
    private void Update()
    {
        if (score == "wb" || score =="bw")
        {
            Debug.Log("Pass");
            SceneManager.LoadScene(buildIndex + 1);
        }
    }
}

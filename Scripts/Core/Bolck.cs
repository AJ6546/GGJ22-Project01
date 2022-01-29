using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolck : MonoBehaviour
{
    [SerializeField] float destroyAfter = 25f;
    [SerializeField] string s = "";
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyAfter);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() && other.GetComponent<PlayerController>().s!=s)
        {
            Destroy(gameObject,0.1f);
        }
    }
}

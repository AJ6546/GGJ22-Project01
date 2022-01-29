using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] float speed=10f,destroyAfter=5f;
    [SerializeField] Transform instantiator=null;
    [SerializeField] Vector3 targetPos;
    void Start()
    {
    }
    public void SetInstantiator(Transform instantiator)
    {
        this.instantiator = instantiator;
        targetPos = instantiator.position + instantiator.forward * 100 + transform.up + transform.right;
    }
    void Update()
    {
        Destroy(gameObject, destroyAfter);
        if (instantiator != null)
        {
            transform.position=Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}

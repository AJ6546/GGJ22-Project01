using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        transform.forward = FindObjectOfType<Camera>().transform.forward;
    }
}


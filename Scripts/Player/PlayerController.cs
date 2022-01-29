using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;

    [SerializeField] FixedButton jumpButton;
    [SerializeField] FixedTouchField touchField;
    public string s;
    [SerializeField] ThirdPersonUserControl control;
    
    [SerializeField] Camera camera;
    [SerializeField] float cameraAngle, cameraSpeed = 0.2f, rotOffset;
    [SerializeField] Vector3 cameraOffset;
    
    void Awake()
    {
        control = GetComponent<ThirdPersonUserControl>();
    }



    void Update()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera_" + s).GetComponent<Camera>();
        cameraAngle += touchField.TouchDist.x * cameraSpeed;
        camera.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * rotOffset - camera.transform.position, Vector3.up);
        control.m_Jump = jumpButton.Pressed || Input.GetKey("space");
        control.hInput = fixedJoystick.Horizontal;
        control.vInput = fixedJoystick.Vertical;
    }
}

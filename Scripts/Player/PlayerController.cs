using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Vector3 startPos;
    [SerializeField] FixedButton jumpButton, recoverButton;
    [SerializeField] FixedTouchField touchField;
    public string s;
    [SerializeField] ThirdPersonUserControl control;
    
    [SerializeField] Camera camera;
    [SerializeField] float cameraAngle, cameraSpeed = 0.2f, rotOffset;
    [SerializeField] Vector3 cameraOffset;
    
    void Awake()
    {
        startPos = transform.position;
        control = GetComponent<ThirdPersonUserControl>();
    }



    void Update()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera_" + s).GetComponent<Camera>();
        cameraAngle += touchField.TouchDist.x * cameraSpeed;
        camera.transform.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * rotOffset - camera.transform.position, Vector3.up);
        if(s=="b")
        {
            control.m_Jump = Input.GetKey("space") || jumpButton.Pressed;
        }
        if(s=="w")
        {
            control.m_Jump = Input.GetKey("up") || jumpButton.Pressed;
        }
        control.hInput = fixedJoystick.Horizontal;
        control.vInput = fixedJoystick.Vertical;
        if(transform.position.y<=-5)
        {
            transform.position = startPos;
        }
        if (recoverButton.Pressed)
        {
            transform.position = new Vector3(transform.position.x+2f, 2, transform.position.z+2f);
        }
    }
}

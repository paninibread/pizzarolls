using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementOld : MonoBehaviour
{
    public float speed = 10f;
    public float rot = 65f;

    public float G = 9.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position += G * Time.deltaTime * -transform.up;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += speed * Time.deltaTime * transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += speed * Time.deltaTime * -transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += speed * Time.deltaTime * transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += speed * Time.deltaTime * -transform.right;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0,-rot,0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, rot, 0) * Time.deltaTime);
        }
    }
}

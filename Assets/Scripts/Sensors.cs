using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensors : MonoBehaviour
{
    bool isSensorActive;
    public RaycastHit hit;
    private Vector3 prevPos;

    private void OnEnable()
    {
        CombatSystemEventHandler.StartSensors += OnStartSensors;
    }
    private void OnDisable()
    {
        CombatSystemEventHandler.StartSensors -= OnStartSensors;
    }
    private void OnStartSensors(bool state)
    {
        prevPos = this.transform.position;
        isSensorActive = state;
    }
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (isSensorActive)
        {
            WeaponSensorDetection();
        }
    }
    private void WeaponSensorDetection()
    {
        Vector3 direction = prevPos - this.transform.position;
        if (Physics.Raycast(this.transform.position, direction, out hit, 2))
        {
            Debug.DrawRay(this.transform.position, direction, Color.yellow, 2);
            CombatSystemEventHandler.TriggerHitEvent(this.transform.gameObject);
        }
        else
        {
            Debug.DrawRay(this.transform.position, direction, Color.white, 2);
        }
        prevPos = this.transform.position;
    }
}

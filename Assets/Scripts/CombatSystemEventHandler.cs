using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class CombatSystemEventHandler
{
    public static event UnityAction<GameObject> HitEvent;
    public static void TriggerHitEvent(GameObject sensorObject) => HitEvent?.Invoke(sensorObject);

    public static event UnityAction<bool> StartSensors;
    public static void TriggerStartSensors(bool state) => StartSensors?.Invoke(state);

    public static event UnityAction<bool> BasicAttackAnimationState;
    public static void TriggerBasicAttackAnimation(bool state) => BasicAttackAnimationState?.Invoke(state);
}
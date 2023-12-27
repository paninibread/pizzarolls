using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystemAnimationEventHelper : MonoBehaviour
{
    public void BasicAttackAnimationStart()
    {
        CombatSystemEventHandler.TriggerBasicAttackAnimation(true);
    }
    public void BasicAttackAnimationStop()
    {
        CombatSystemEventHandler.TriggerBasicAttackAnimation(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAction : MonoBehaviour
{

    [SerializeField]
    private GunSelector gunSelector;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && gunSelector)
        {
            gunSelector.activeGun.Shoot();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class GunSelector : MonoBehaviour
{
    [SerializeField]
    private GunType Gun;
    [SerializeField]
    private Transform GunParent;
    [SerializeField]
    private List<GunScriptableObj> Guns;

    [Space]
    public GunScriptableObj activeGun;

    private void Start()
    {
        GunScriptableObj gun = Guns.Find(gun => gun.type == Gun);
        if (gun == null)
        {
            Debug.Log("duuno what ya shootin");
            return;
        }

        activeGun = gun;
        gun.Spawn(GunParent, this);
    }
}

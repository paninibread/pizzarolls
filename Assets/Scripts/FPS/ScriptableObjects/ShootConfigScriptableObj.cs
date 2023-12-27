using UnityEngine;

[CreateAssetMenu(fileName = "ShootConfig", menuName ="Gun/Shoot Configuration", order =2)]

public class ShootConfigScriptableObj : ScriptableObject
{
    public Vector3 spread = new Vector3(.1f, .1f, .1f);
    public float FireRate = .25f;
    
}

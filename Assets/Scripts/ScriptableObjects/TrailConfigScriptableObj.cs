using UnityEngine;
[CreateAssetMenu(fileName ="TrailConfig", menuName ="Gun/Gun Trail Configuration", order = 4)]
public class TrailConfigScriptableObj : ScriptableObject
{
    public Material trailMaterial;
    public AnimationCurve widthCurve;
    public float duration = .5f;
    public float minVertexDistance = .1f;
    public Gradient color;

    public float missDistance = 100f;
    public float simulationSpeed = 100f;
}

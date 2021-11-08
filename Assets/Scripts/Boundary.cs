using UnityEngine;

[CreateAssetMenu(fileName = "New Bound", menuName = "Custom/New Bound")]
public class Boundary : ScriptableObject
{
    public float leftBound;
    public float rightBound;
    public float TopBound;
    public float BottomBound;
}

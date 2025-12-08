using UnityEngine;

public class Letter : MonoBehaviour
{
    public StateGroup State;
    public enum StateGroup
    {
        Clip,
        Chamber,
        Ground
    }
    public int Index;
}

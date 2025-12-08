using UnityEngine;

public class Spin : MonoBehaviour
{
    public bool On;
    public float Angle;
    public float MaximumAngle; 
    public float Frequency;
    
    void Update()
    {
        if (!On) return;
        
        float Range = (MaximumAngle - Angle) / 2;
        
        transform.localRotation = Quaternion.Euler(0, Mathf.Sin(
            Frequency * API.Time()) * Range + (Angle + Range), 0);
    }
}

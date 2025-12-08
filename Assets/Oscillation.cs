using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public bool On;
    public float Range;
    public float Speed;
    
    Vector3 Position;

    float Seed;
    
    void Start()
    {
        Position = transform.localPosition;
        Seed = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (!On) return;
        
        float Oscillation = Mathf.Sin(Speed * API.Time() + Seed) * Range;
        transform.localPosition = Position + new Vector3(0, Oscillation, 0);
    }
}

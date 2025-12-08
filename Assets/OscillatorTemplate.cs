using UnityEngine;

public class OscillatorTemplate : MonoBehaviour
{
    // The type of effect to apply
    public enum EffectType { Bounce, Rotate }
    public EffectType effectType = EffectType.Bounce;

    // The maximum range of the movement (e.g., 0.5 units up/down, or 10 degrees rotation)
    public float range = 0.5f;

    // The speed/frequency of the oscillation
    public float speed = 2f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    // These offsets will be set randomly in Start()
    private float offsetX;
    private float offsetY;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        
        offsetX = Random.Range(0f, 100f);
        offsetY = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Calculate the oscillation value using a sine wave
        // Time.time is used to make the oscillation continuous
        // The value will range from -1 to 1
        float oscillation = Mathf.Sin(Time.time * speed + offsetX);

        // Scale the oscillation by the desired range
        float finalMovement = oscillation * range;

        if (effectType == EffectType.Bounce)
        {
            // 1. Bounce Up and Down (Y-axis)
            // Apply the offset to the local Y position
            transform.localPosition = initialPosition + new Vector3(0f, finalMovement, 0f);
        }
        else if (effectType == EffectType.Rotate)
        {
            // 2. Rotate Back and Forth (Z-axis is common for 2D objects)
            // Apply the offset as an angle
            transform.localRotation = initialRotation * Quaternion.Euler(0f, 0f, finalMovement);
        }
    }
}
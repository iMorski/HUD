using UnityEngine;

public class Shaker : MonoBehaviour
{
    // The strength of the shake (higher value = bigger shake)
    public float shakeMagnitude = 0.1f;
    
    // The rate/speed of the shake
    public float shakeSpeed = 1f;

    // These offsets will be set randomly in Start()
    private float offsetX;
    private float offsetY;

    private Vector3 originalPosition;

    void Start()
    {
        // 1. Initialize the unique seed offsets (The Fix!)
        // Use Random.Range to generate large, unique starting points on the Perlin Noise map
        offsetX = Random.Range(0f, 100f);
        offsetY = Random.Range(0f, 100f);
        
        // Store the starting position
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the input value using the unique offsets
        // By adding the unique offsets, each object samples a different region 
        // of the infinite Perlin Noise field, resulting in different movement.
        float timeX = Time.time * shakeSpeed + offsetX;
        float timeY = Time.time * shakeSpeed + offsetY;
        
        // Use Perlin Noise for smooth, random-like movement
        // The second parameter can be constant (e.g., 0f) or time-based
        float xOffset = (Mathf.PerlinNoise(timeX, 0f) * 2f - 1f) * shakeMagnitude;
        float yOffset = (Mathf.PerlinNoise(0f, timeY) * 2f - 1f) * shakeMagnitude;
        
        // Apply the offset relative to the original position
        transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
    }
}
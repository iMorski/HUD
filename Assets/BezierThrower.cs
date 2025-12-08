using UnityEngine;
using System.Collections; // Needed for Coroutines

public class BezierThrower : MonoBehaviour
{
    // === Public Inputs ===
    public Transform palmTransform;          // P0: Start point (The character's hand)
    public GameObject cubeToThrow;          // The cube GameObject to move
    //public LayerMask groundLayer;            // The layer mask for ground detection
    public float peakHeight = 3.0f;          // The height of the Control Point above the midpoint
    public float throwDuration = 0.5f;       // How fast the cube should travel (in seconds)

    // === Private References ===
    private Camera mainCamera;
    private bool isThrowing = false;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No Main Camera found! Please ensure a camera is tagged 'MainCamera'.");
        }
        // Ensure the cube is initially at the palm's position
        cubeToThrow.transform.position = palmTransform.position;
    }

    void Update()
    {
        // Only start a new throw if we aren't currently throwing
        if (Input.GetMouseButtonDown(0) && !isThrowing)
        {
            TryThrowCube();
        }
    }

    void TryThrowCube()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f)) //, groundLayer))
        {
            Vector3 targetPosition = hit.point; // P2: The end point

            // P0: Start position is the palm
            Vector3 startPosition = palmTransform.position;

            // Calculate the Control Point (P1)
            Vector3 controlPoint = CalculateControlPoint(startPosition, targetPosition, peakHeight);

            // Start the smooth movement Coroutine
            StartCoroutine(MoveAlongBezierCurve(startPosition, controlPoint, targetPosition, throwDuration));
        }
    }

    /// <summary>
    /// Calculates the Control Point (P1) for the Quadratic Bézier Curve.
    /// It is positioned at the midpoint of P0 and P2, raised by 'peakHeight'.
    /// </summary>
    private Vector3 CalculateControlPoint(Vector3 p0, Vector3 p2, float height)
    {
        // 1. Find the horizontal midpoint between start and end
        Vector3 midpoint = Vector3.Lerp(p0, p2, 0.5f);

        // 2. Raise the midpoint vertically
        // The Y coordinate is the highest Y of the start/end point, plus the desired peak height.
        float maxY = Mathf.Max(p0.y, p2.y) + height;

        // 3. Construct the Control Point (P1)
        return new Vector3(midpoint.x, maxY, midpoint.z);
    }
    
    // --- The Core Bézier Movement ---
    
    IEnumerator MoveAlongBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float duration)
    {
        isThrowing = true;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Calculate the t value (0 to 1)
            float t = timeElapsed / duration;

            // Calculate the position on the Quadratic Bézier curve
            Vector3 newPosition = GetQuadraticBezierPosition(p0, p1, p2, t);

            // Update the cube's position
            cubeToThrow.transform.position = newPosition;

            timeElapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the cube lands exactly on the target position at the end
        cubeToThrow.transform.position = p2;
        isThrowing = false;
        
        // OPTIONAL: Add a small bounce or reset the cube here
        Debug.Log("Cube landed at target.");
    }

    /// <summary>
    /// Quadratic Bézier Curve formula: B(t) = (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2
    /// </summary>
    private Vector3 GetQuadraticBezierPosition(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        // The standard formula:
        float oneMinusT = 1f - t;
        
        // (1-t)^2 * P0
        Vector3 term1 = oneMinusT * oneMinusT * p0;
        
        // 2(1-t)t * P1
        Vector3 term2 = 2f * oneMinusT * t * p1;
        
        // t^2 * P2
        Vector3 term3 = t * t * p2;

        return term1 + term2 + term3;
    }
}
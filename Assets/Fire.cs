using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ClipManager ClipManager;
    public Camera Camera;
    public Letter Letter;
    public Transform LetterPoint;
    public float Height = 3.0f;
    public float FallHeight = 0.525f;
    public float Duration = 0.5f;

    void Update()
    {
        if (!Letter || !Input.GetMouseButtonDown(0)) return;
        
        Ray Ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Physics.Raycast(Ray, out Hit))
        {
            Vector3 FallPoint = new Vector3(Hit.point.x, FallHeight, Hit.point.z);
            Vector3 MiddlePoint = GetMiddlePoint(LetterPoint.position, FallPoint);

            StartCoroutine(MoveAlongBezierCurve(
                LetterPoint.position, MiddlePoint, FallPoint));
            
            ClipManager.Release();
        }
    }

    Vector3 GetMiddlePoint(Vector3 From, Vector3 To)
    {
        Vector3 MiddlePoint = Vector3.Lerp(From, To, 0.5f);

        return new Vector3(MiddlePoint.x, Mathf.Max(
            From.y, To.y) + Height, MiddlePoint.z);
    }
    
    IEnumerator MoveAlongBezierCurve(Vector3 From, Vector3 Middle, Vector3 To)
    {
        Letter Letter = this.Letter;
        this.Letter = null;
        
        Letter.transform.SetParent(null);
        
        float Time = 0;

        while (Time < Duration)
        {
            Letter.transform.position = GetQuadraticBezierPosition(
                From, Middle, To, Time / Duration);;

            Time = Time + API.DTime();
            
            yield return null;
        }

        Letter.transform.position = To;
        Letter.State = Letter.StateGroup.Ground;
    }

    private Vector3 GetQuadraticBezierPosition(Vector3 From, Vector3 Middle, Vector3 To, float Time)
    {
        return (1 - Time) * (1 - Time) * From + 2 * (1 - Time) * Time * Middle + Time * Time * To;
    }
}

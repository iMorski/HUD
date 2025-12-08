using UnityEngine;

public class Aim : MonoBehaviour
{
    public Camera Camera;

    void Update()
    {
        Ray Ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Physics.Raycast(Ray, out Hit))
        {
            Vector3 Point = Hit.point;
            
            transform.position = new Vector3(
                Point.x, 0.01f, Point.z);
        }
    }
}

using UnityEngine;

public class Unit : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;

    void Update()
    {
        Vector3 Input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"),
            .0f, UnityEngine.Input.GetAxisRaw("Vertical"));
        
        transform.position = transform.position + Speed * API.DTime() * Input;
        
        if (!Zero(Input)) transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.LookRotation(Input), RotationSpeed * API.DTime());
    }

    bool Zero(Vector3 Vector)
    {
        return !(Vector != new Vector3());
    }
}

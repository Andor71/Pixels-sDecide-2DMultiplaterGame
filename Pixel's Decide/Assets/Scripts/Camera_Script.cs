using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public Transform player;

    [Range(1,10)]
    public float Speed;
    public Vector3 offsetCamera;


    void LateUpdate()
    {
        Vector3 locedPosition = player.position + offsetCamera;
        Vector3 delayedPosition = Vector3.Lerp(transform.position,locedPosition,Speed*Time.deltaTime);
        transform.position = delayedPosition;

    }


}

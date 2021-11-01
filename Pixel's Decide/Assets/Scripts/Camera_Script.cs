using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    Transform player;

    [Range(1,10)]
    public float Speed;
    public Vector3 offsetCamera;

    public void getPLayer(Transform playerT)
    {
        Debug.Log("Got player info :" + playerT);
        player = playerT;
    }

    void LateUpdate()
    {
        if(player != null)
        {
            Vector3 locedPosition = player.position + offsetCamera;
            Vector3 delayedPosition = Vector3.Lerp(transform.position,locedPosition,Speed*Time.deltaTime);
            transform.position = delayedPosition;
        }
        else
        {
            Debug.Log("There is no PLayer to lock to");
        }

    }


}

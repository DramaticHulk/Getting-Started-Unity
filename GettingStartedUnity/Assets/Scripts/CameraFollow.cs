using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 0f, -15f);

    
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}

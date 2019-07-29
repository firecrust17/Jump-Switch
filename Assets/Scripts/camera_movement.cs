using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public Transform playerTform;
    public Vector3 offset;
    void Update() {
        // Debug.Log(playerTform.position);
        transform.position = playerTform.position + offset;
    }


}

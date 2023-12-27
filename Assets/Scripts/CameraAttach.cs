using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttach : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 0f, 0f); // Adjust the offset as needed

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of the GameObject based on the player's position
            transform.position = playerTransform.position + offset;
        }
    }
}

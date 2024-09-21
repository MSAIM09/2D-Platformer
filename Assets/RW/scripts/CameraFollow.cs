using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetObject;
    public float horizontalFollowSpeed = 20f;
    public float verticalFollowSpeed = 15f;

    private Vector3 targetPosition;

    private void LateUpdate()
    {
        targetPosition = targetObject.transform.position;
        targetPosition.z = transform.position.z;

        // Calculate the new camera position for smooth follow
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, horizontalFollowSpeed * Time.deltaTime);

        // Adjust the vertical follow speed based on the player's vertical movement
        float verticalSpeedModifier = Mathf.Clamp01(Mathf.Abs(targetObject.GetComponent<Rigidbody2D>().velocity.y));
        newPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, verticalFollowSpeed * verticalSpeedModifier * Time.deltaTime);

        transform.position = newPosition;
    }
}


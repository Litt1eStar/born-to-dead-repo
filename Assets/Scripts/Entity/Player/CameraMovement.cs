using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public float threshold;
    public float smoothness = 5f;

    private void FixedUpdate()
    {
        Vector3 mousePos = InputManager.Instance.GetMouseWorldPosition();
        Vector3 targetPos = (player.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);
        targetPos.z = -0.3f;

        // Use Mathf.Lerp for smooth interpolation
        this.transform.position = Vector3.Lerp(transform.position, targetPos, smoothness * Time.deltaTime);
    }
}

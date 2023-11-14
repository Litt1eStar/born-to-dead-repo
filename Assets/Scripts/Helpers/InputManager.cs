using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 v = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        v.z = 0f;
        return v;
    }
    public Vector3 GetMouseWorldPositoinWithZ() => GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    public Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) => GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    public Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public Vector2 KeyboardMovementInput(float xPos, float yPos)
    {
        xPos = Input.GetAxisRaw("Horizontal");
        yPos = Input.GetAxisRaw("Vertical");
        return new Vector2(xPos, yPos);
    }
}

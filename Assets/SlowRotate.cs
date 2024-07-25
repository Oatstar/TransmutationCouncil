using UnityEngine;

public class SlowRotate : MonoBehaviour
{
    // Speed of rotation in degrees per second
    public float rotationSpeed = 10f;

    void Update()
    {
        // Calculate the rotation angle
        float angle = rotationSpeed * Time.unscaledDeltaTime;

        // Apply the rotation to the Z axis
        transform.Rotate(0f, 0f, angle);
    }
}
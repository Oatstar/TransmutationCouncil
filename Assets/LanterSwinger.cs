using UnityEngine;

public class LanternSwinger : MonoBehaviour
{
    // Amplitude of the swing in degrees
    public float swingAmplitude = 15f;

    // Speed of the swing
    public float swingSpeed = 1f;

    // Center rotation (optional)
    public float centerRotation = 0f;

    void Update()
    {
        // Calculate the oscillating rotation angle using Mathf.Sin
        float rotationAngle = centerRotation + swingAmplitude * Mathf.Sin(Time.unscaledTime * swingSpeed);

        // Apply the rotation to the lantern
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGraphicController : MonoBehaviour
{
    public Image circleGlow;
    public Image circleExtraGlow;
    public ParticleSystem glowParticles;

    public static CircleGraphicController instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartGlow()
    {
        if (circleGlow != null && glowParticles != null && circleExtraGlow != null)
        {
            StartCoroutine(GlowRoutine());
        }
        else
        {
            Debug.LogError("circleGlow, circleExtraGlow Image component or glowParticles is not assigned.");
        }
    }

    private IEnumerator GlowRoutine()
    {
        // Initial setup
        Color circleGlowColor = circleGlow.color;
        circleGlowColor.a = 0f;
        circleGlow.color = circleGlowColor;

        Color extraGlowColor = circleExtraGlow.color;
        extraGlowColor.a = 0f;
        circleExtraGlow.color = extraGlowColor;

        // Start the ParticleSystem
        glowParticles.Play();

        // Ramp up alpha of circleGlow to 1 in 1 second
        float rampUpTime = 1.0f;
        for (float t = 0; t < rampUpTime; t += Time.deltaTime)
        {
            circleGlowColor.a = Mathf.Lerp(0f, 1f, t / rampUpTime);
            circleGlow.color = circleGlowColor;
            yield return null;
        }
        circleGlowColor.a = 1f;
        circleGlow.color = circleGlowColor;

        // Set circleExtraGlow alpha to 1 instantly
        extraGlowColor.a = 1f;
        circleExtraGlow.color = extraGlowColor;

        // Stop the ParticleSystem after 1 second
        glowParticles.Stop();

        // Fade out alpha of circleGlow to 0 in 5 seconds
        float fadeOutTime = 3.0f;
        float extraGlowFadeOutTime = 0.5f;
        for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
        {
            circleGlowColor.a = Mathf.Lerp(1f, 0f, t / fadeOutTime);
            circleGlow.color = circleGlowColor;

            // Fade out alpha of circleExtraGlow to 0 in 2 seconds
            if (t < extraGlowFadeOutTime)
            {
                extraGlowColor.a = Mathf.Lerp(1f, 0f, t / extraGlowFadeOutTime);
                circleExtraGlow.color = extraGlowColor;
            }

            yield return null;
        }
        circleGlowColor.a = 0f;
        circleGlow.color = circleGlowColor;

        extraGlowColor.a = 0f;
        circleExtraGlow.color = extraGlowColor;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleGraphicController : MonoBehaviour
{
    public Image circleGlow;
    public Image circleExtraGlow;
    public ParticleSystem glowParticles;

    public ParticleSystem ballParticles1;
    public ParticleSystem ballParticles2;
    public Image ballGlowImage1;
    public Image ballGlowImage2;

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

        // Initial setup for ballGlowImage1 and ballGlowImage2
        Color ballGlowColor1 = ballGlowImage1.color;
        ballGlowColor1.a = 0f;
        ballGlowImage1.color = ballGlowColor1;

        Color ballGlowColor2 = ballGlowImage2.color;
        ballGlowColor2.a = 0f;
        ballGlowImage2.color = ballGlowColor2;

        // Start the ParticleSystem
        glowParticles.Play();

        // Ramp up alpha of circleGlow to 1 in 1 second
        float rampUpTime = 1.0f;
        for (float t = 0; t < rampUpTime; t += Time.deltaTime)
        {
            circleGlowColor.a = Mathf.Lerp(0f, 1f, t / rampUpTime);
            circleGlow.color = circleGlowColor;

            // Start ramping up the ballGlowImages when circleGlowColor.a reaches 0.8
            if (circleGlowColor.a >= 0.8f)
            {
                float normalizedTime = (circleGlowColor.a - 0.8f) / 0.2f; // Normalize between 0 and 1
                ballGlowColor1.a = Mathf.Lerp(0f, 1f, normalizedTime);
                ballGlowImage1.color = ballGlowColor1;

                ballGlowColor2.a = Mathf.Lerp(0f, 1f, normalizedTime);
                ballGlowImage2.color = ballGlowColor2;
            }

            yield return null;
        }


        // Ensure the final alpha is set to 1
        circleGlowColor.a = 1f;
        circleGlow.color = circleGlowColor;

        ballGlowColor1.a = 1f;
        ballGlowImage1.color = ballGlowColor1;

        ballGlowColor2.a = 1f;
        ballGlowImage2.color = ballGlowColor2;

        // Set circleExtraGlow alpha to 1 instantly
        extraGlowColor.a = 1f;
        circleExtraGlow.color = extraGlowColor;

        // Stop the ParticleSystem after 1 second
        glowParticles.Stop();

        ballParticles1.Play();
        ballParticles2.Play();

        ballGlowImage1.color = new Color(1, 1, 1, 0);
        ballGlowImage2.color = new Color(1, 1, 1, 0);

        //ballParticles1.Stop();
        //ballParticles1.Stop();

        // Fade out alpha of circleGlow to 0 in 5 seconds
        float fadeOutTime = 3.0f;
        float extraGlowFadeOutTime = 0.5f;
        for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
        {
            circleGlowColor.a = Mathf.Lerp(1f, 0f, t / fadeOutTime);
            circleGlow.color = circleGlowColor;

            // Fade out alpha of circleExtraGlow to 0 in 0.5 seconds
            if (t < extraGlowFadeOutTime)
            {
                extraGlowColor.a = Mathf.Lerp(1f, 0f, t / extraGlowFadeOutTime);
                circleExtraGlow.color = extraGlowColor;
            }

            yield return null;
        }

        // Ensure the final alpha is set to 0
        circleGlowColor.a = 0f;
        circleGlow.color = circleGlowColor;

        extraGlowColor.a = 0f;
        circleExtraGlow.color = extraGlowColor;

        ballGlowColor1.a = 0f;
        ballGlowImage1.color = ballGlowColor1;

        ballGlowColor2.a = 0f;
        ballGlowImage2.color = ballGlowColor2;
    }
}

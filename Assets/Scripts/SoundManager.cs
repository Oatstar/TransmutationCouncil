using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource basicClick;
    public AudioSource transmuteSound;
    public AudioSource transmuteSuccess;
    public AudioSource transmuteFailed;
    public AudioSource combatFinished;
    public AudioSource toggleCombat;
    public AudioSource placeItem;

    private bool isMuted = false;

    void Start()
    {
        // Initialize all audio sources to unmuted if needed
        UnmuteAllSounds();
    }


    public void PlayTransmuteSound()
    {
        if (!isMuted) transmuteSound.Play();
    }


    public void PlayBasicClick()
    {
        if (!isMuted) basicClick.Play();
    }

    public void PlayTransmuteSuccess()
    {
        if (!isMuted) transmuteSuccess.Play();
    }

    public void PlayTransmuteFailed()
    {
        if (!isMuted) transmuteFailed.Play();
    }

    public void PlayCombatFinished()
    {
        if (!isMuted) combatFinished.Play();
    }

    public void PlayToggleCombat()
    {
        if (!isMuted) toggleCombat.Play();
    }

    public void PlayPlaceItem()
    {
        if (!isMuted) placeItem.Play();
    }

    public void ToggleSounds()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            MuteAllSounds();
        }
        else
        {
            UnmuteAllSounds();
        }
    }

    private void MuteAllSounds()
    {
        basicClick.mute = true;
        transmuteSuccess.mute = true;
        transmuteFailed.mute = true;
        combatFinished.mute = true;
        toggleCombat.mute = true;
        placeItem.mute = true;
    }

    private void UnmuteAllSounds()
    {
        basicClick.mute = false;
        transmuteSuccess.mute = false;
        transmuteFailed.mute = false;
        combatFinished.mute = false;
        toggleCombat.mute = false;
        placeItem.mute = false;
    }
}
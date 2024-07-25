using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] musicTracks;
    private int currentTrackIndex = -1;
    private bool isPlaying = false;
    public static MusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Optionally, you can call StartMusic() automatically when the game starts
        StartMusic();
    }

    public void StartMusic()
    {
        if (isPlaying || musicTracks.Length == 0)
            return;

        isPlaying = true;
        currentTrackIndex = Random.Range(0, musicTracks.Length);
        StartCoroutine(PlayNextTrack());
    }

    private IEnumerator PlayNextTrack()
    {
        while (isPlaying)
        {
            musicTracks[currentTrackIndex].Play();
            yield return new WaitForSeconds(musicTracks[currentTrackIndex].clip.length);

            musicTracks[currentTrackIndex].Stop();
            yield return new WaitForSeconds(3.0f);

            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        }
    }

    public void PlayTrack(int id)
    {
        if (id < 0 || id >= musicTracks.Length)
        {
            Debug.LogError("Track ID is out of range.");
            return;
        }

        StopMusic(); // Stop any currently playing music
        currentTrackIndex = id;
        musicTracks[currentTrackIndex].Play();
        isPlaying = true;
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        foreach (var track in musicTracks)
        {
            track.Stop();
        }
        isPlaying = false;
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            StopMusic();
        }
        else
        {
            StartMusic();
        }
    }
}
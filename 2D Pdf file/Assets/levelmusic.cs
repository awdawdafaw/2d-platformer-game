using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    public AudioSource levelMusic;   // Assign Track 3 here
    public AudioSource gameOverMusic; // Assign Track 9 here

    private void Start()
    {
        PlayLevelMusic();
    }

    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            gameOverMusic.Stop(); // Ensure game over music is not playing
            levelMusic.Play();
        }
    }

    public void PlayGameOverMusic()
    {
        if (levelMusic.isPlaying)
        {
            levelMusic.Stop(); // Stop level music first
        }

        if (!gameOverMusic.isPlaying)
        {
            gameOverMusic.Play(); // Play game over music only if it's not already playing
        }
    }

    public void StopAllMusic()
    {
        levelMusic.Stop();
        gameOverMusic.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text endText;

    protected bool playing;
    protected bool gamePaused;
    public bool gameWon;

    public AudioClip[] clips;
    public int[] repeats;

    public int repeatNumber;
    public int clipNumber;

    public int numEnemies;

    public WindController wind;
    private AudioSource au;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        gamePaused = false;
        gameWon = false;
        au = GetComponent<AudioSource>();
        au.clip = clips[0];
        au.Play();
        repeatNumber = 1;
        clipNumber = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!playing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P) && !gamePaused)
            {
                pauseGame();
                gamePaused = true;
            }
            else if (Input.GetKeyDown(KeyCode.P) && gamePaused)
            {
                resumeGame();
                gamePaused = false;
            }

            if(numEnemies <= 0)
            {
                gameWon = true;
                playing = false;
            }
        }
        if (!au.isPlaying)
        {
            if(repeatNumber >= repeats[clipNumber])
            {
                clipNumber = (clipNumber + 1) % clips.Length;
                repeatNumber = 0;
            }
            au.clip = clips[clipNumber];
            au.Play();
            repeatNumber += 1;
        }
    }

    

    public void endGame()
    {
        playing = false;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }
}

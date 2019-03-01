using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //
    // Parameters
    //
    public int numEnemies;
    protected bool defaultSpawnBehaviour = true;
    public int stage;

    //
    // Game State
    //
    protected bool playing;
    protected bool gamePaused;
    public bool gameWon;

    protected float timer;

    //
    // Audio
    //
    public AudioClip[] clips;
    public int[] repeats;

    protected int repeatNumber;
    protected int clipNumber;

    //
    // Components
    //
    private AudioSource au;
    
    //
    // World
    //
    public WindController wind;
    public Text endText;

    //
    // SpawnBehaviour
    // 
    public GameObject[] spawnLocations;
    public GameObject[] spawnEnemies;
    public int[] spawnRefs;
    public float[] spawnDelays;
    protected bool[] spawnState;
        

    // Start is called before the first frame update
    protected void Start()
    {
        playing = true;
        gamePaused = false;
        gameWon = false;
        au = GetComponent<AudioSource>();
        au.clip = clips[0];
        au.Play();
        repeatNumber = 1;
        clipNumber = 0;
        defaultSpawnBehaviour = true;
        timer = 0;
        spawnState = new bool[spawnDelays.Length];
        wind = FindObjectOfType<WindController>();
    }
    
    // Update is called once per frame
    protected void Update()
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

    protected void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (!defaultSpawnBehaviour)
        {
            return;
        }

        for(int x = 0; x < spawnDelays.Length; x++)
        {
            if(spawnDelays[x] >= timer && !spawnState[x])
            {
                Instantiate(spawnEnemies[spawnRefs[x]], spawnLocations[x].transform.position, spawnLocations[x].transform.rotation);
                spawnState[x] = true;
            }
        }

    }

    public virtual void PullTrigger(int id)
    {

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

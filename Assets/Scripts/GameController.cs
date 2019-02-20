using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int spawnTime;
    public float lastSpawn;
    public int score;
    public GameObject asteroid1;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    public float radiusSpawn;
    public float spawnWait;

    public Text scoreText;
    public Text endText;

    private bool playing;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        endText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawn + spawnTime)
        {
            lastSpawn = Time.time;
            //StartCoroutine(SpawnWave());
        }
        UpdateScore();
        if (!playing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            endText.text = "You Lost. Press 'R' to play again";
        }
        
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnWave()
    {
        int n = (int)Random.Range(1, 7);
        for (int x = 0; x < n && playing; x++)
        {
            Instantiate(asteroid1, new Vector3(spawnPosition.x + Random.Range(-radiusSpawn, radiusSpawn), spawnPosition.y, spawnPosition.z), spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
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

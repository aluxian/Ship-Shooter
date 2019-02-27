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

    public WindController wind;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        //windText.text = "Wind Bearing, Speed:" + wind.direction.ToString("D3") + ", " + wind.windPower.ToString("F3");
        if (Time.time > lastSpawn + spawnTime)
        {
            lastSpawn = Time.time;
            //StartCoroutine(SpawnWave());
        }

        if (!playing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            //endText.text = "You Lost. Press 'R' to play again";
        }
        
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

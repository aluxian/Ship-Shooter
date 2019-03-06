using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameOnEsc : MonoBehaviour
{
    public GameObject gamePauseHud;

    protected bool wasPressedLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Escape) != wasPressedLastFrame)
        {
            if (Time.timeScale == 0)
            {
                onResumeClicked();
            }
            else
            {
                gamePauseHud.SetActive(true);
                Time.timeScale = 0;
            }
        }

        wasPressedLastFrame = Input.GetKey(KeyCode.Escape);
    }

    public void onResumeClicked()
    {
        gamePauseHud.SetActive(false);
        Time.timeScale = 1;
    }

    public void onMenuClicked()
    {
        SceneManager.LoadScene("IntroMenu");
    }

    public void onNextClicked()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void onLevelSelectClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}

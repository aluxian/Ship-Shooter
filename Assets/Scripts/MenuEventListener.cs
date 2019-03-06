using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEventListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }

    public void onTutClicked()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void onLev1Clicked()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void onMenuClicked()
    {
        SceneManager.LoadScene("IntroMenu");
    }
}

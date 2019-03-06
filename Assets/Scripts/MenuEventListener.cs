using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEventListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartClicked()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }
}

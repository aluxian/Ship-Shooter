

using UnityEngine;

public class StoryPromptController : MonoBehaviour
{
    public GameObject story1;
    public GameController gameController;

    protected Story currentStory = Story.None;
    protected Story desiredStory = Story.None;
    

    // Start is called before the first frame update
    void Start()
    {
        desiredStory = Story.IntroBackStory;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStory != desiredStory)
        {
            if (desiredStory == Story.None)
            {
                story1.SetActive(false);
                gameController.resumeGame();
            }

            if (desiredStory == Story.IntroBackStory)
            {
                story1.SetActive(true);
                gameController.pauseGame();
            }

            currentStory = desiredStory;
        }
        
        if (currentStory != Story.None && Input.GetMouseButtonDown(0))
        {
            desiredStory = Story.None;

            if (currentStory == Story.IntroBackStory)
            {
                gameController.IntroStoryDismissed();
            }
        }
    }

    public enum Story
    {
        None,
        IntroBackStory,
    }
}

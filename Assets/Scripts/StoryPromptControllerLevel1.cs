

using UnityEngine;

public class StoryPromptControllerLevel1 : MonoBehaviour
{
    public GameObject story1;
    public GameController gameController;

    protected Story currentStory = Story.None;
    protected Story desiredStory = Story.None;


    // Start is called before the first frame update
    void Start()
    {
        
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

            if (desiredStory == Story.EndStory)
            {
                story1.SetActive(true);
                gameController.pauseGame();
            }

            currentStory = desiredStory;
        }

        if (currentStory != Story.None && Input.GetMouseButtonDown(0))
        {
            desiredStory = Story.None;

            if (currentStory == Story.EndStory)
            {
                gameController.EndStoryDismissed();
            }
        }
    }

    public void ShowEndStory()
    {
        desiredStory = Story.EndStory;
    }

    public enum Story
    {
        None,
        EndStory,
    }
}

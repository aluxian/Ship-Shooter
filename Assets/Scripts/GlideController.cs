using UnityEngine;

/// <summary>
/// source: https://answers.unity.com/questions/635535/how-to-move-a-gameobject-with-an-animation.html
/// </summary>
public class GlideController : MonoBehaviour
{
    public float speed;

    private Vector3 source;
    private Vector3 destination;

    private float timeStarted;
    private float animationTime = 0.5f;

    public Vector3 initialPosition;

    void Start()
    {
        initialPosition = gameObject.transform.position;
        // Set the destination to be the object's position so it will not start off moving
        SetDestination(gameObject.transform.position);
    }

    void Update()
    {
        // If the object is not at the target destination
        if (destination != gameObject.transform.position)
        {
            // Move towards the destination each frame until the object reaches it
            IncrementPosition();
        }
    }

    void IncrementPosition()
    {
        float progress = (Time.time - timeStarted) / animationTime;
        progress = EasingFunctions.EaseInOutCubic(0, 1, progress);
        // Calculate the next position
        float delta = (speed * progress) * Time.deltaTime;
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);

        // Move the object to the next position
        gameObject.transform.position = nextPosition;
    }

    // Set the destination to cause the object to smoothly glide to the specified location
    public void SetDestination(Vector3 value)
    {
        source = gameObject.transform.position;
        destination = value;
        timeStarted = Time.time;
    }

    public void SetPosition(Vector3 value)
    {
        gameObject.transform.position = value;
        source = value;
        destination = value;
    }
}

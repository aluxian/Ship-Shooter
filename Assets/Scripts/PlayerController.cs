using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    //
    // Physics
    //
    public float rotationModifier;
    public Vector2 sailModifier; // (sails down, sails up)
    public float thrustModifier;
    public float sailAngle;
    public float sailMin;
    public float sailMax;

    public float sailAngleMin;
    public float sailAngleMax;

    public float shipSpeed;
    public float maxSpeed;

    public float sailPosition;

    public float tiltModifier;
    public float tiltResetModifier;
    public float tiltMax;

    public int health;
    public int maxHealth;

    //
    // Shots
    //
    public GameObject shot;
    public Transform[] shotSpawnsPort;
    public Transform[] shotSpawnsStarboard;
    public float reloadTime;

    public float dragCoefficient; 
    
    private float nextFirePort;
    private float nextFireStarboard;

    private GameObject WindDirection;

    //
    // Components
    //

    private Rigidbody rb;
    private AudioSource auShot;

    //
    // World
    //

    [SerializeField]
    public Boundary boundary;

    public WindController wind;

    private Transform meshHolder;
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auShot = GetComponent<AudioSource>();
        meshHolder = this.gameObject.transform.Find("MeshHolder").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFirePort)
        {
            nextFirePort = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsPort[x].position, shotSpawnsPort[x].rotation);
                auShot.Play();
                gameController.IncreaseScore(3); // just to test the score update works
            }
        }

        if (Input.GetButton("Fire2") && Time.time > nextFireStarboard)
        {
            nextFireStarboard = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsStarboard[x].position, shotSpawnsStarboard[x].rotation);
                auShot.Play();
                gameController.IncreaseScore(3); // just to test the score update works
            }
        }
    }

    // FixedUpdate is called before physics operations
    void FixedUpdate()
    {
        float userRudder = Input.GetAxis("Horizontal");
        float userSails = Input.GetAxis("Vertical");

        if (userSails > 0)
        {
            sailPosition += sailModifier[0] * userSails;
        }
        else if(userSails <= 0)
        {
            sailPosition += sailModifier[1] * userSails;
        }

        
        if(sailPosition > sailMax)
        {
            sailPosition = sailMax;
        }
        else if(sailPosition < sailMin)
        {
            sailPosition = sailMin;
        }

        if(sailAngle > sailAngleMax)
        {
            sailAngle = sailAngleMax;
        }
        else if(sailAngle < sailAngleMin)
        {
            sailAngle = sailAngleMin;
        }
        shipSpeed -= shipSpeed * dragCoefficient;



        shipSpeed += Mathf.Abs(thrustModifier * sailPosition * wind.windPower / wind.windMax);

        if (shipSpeed < 0)
        {
            shipSpeed = 0;
        }

        if (shipSpeed > maxSpeed)
        {
            shipSpeed = maxSpeed;
        }

        Vector3 theRotation = meshHolder.rotation.eulerAngles;
        for (int x = 0; x < 3; x++)
        {

            if (theRotation[x] > 180)
            {
                theRotation[x] -= 360;
            }
            else if (theRotation[x] < -180)
            {
                theRotation[x] += 360;
            }
        }

        if (Mathf.Abs(theRotation.z) < tiltResetModifier)
        {
            meshHolder.rotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Abs(theRotation.z) * -1 * Mathf.Sign(theRotation.z)));
        }
        else
        {
            meshHolder.rotation *= Quaternion.Euler(new Vector3(0, 0, tiltResetModifier * -1 * Mathf.Sign(theRotation.z)));
        }
        meshHolder.rotation *= Quaternion.Euler(new Vector3(0, 0, tiltModifier * userRudder));
        if (theRotation.z > tiltMax)
        {
            meshHolder.rotation = Quaternion.Euler(new Vector3(theRotation.x, theRotation.y, tiltMax));
        }
        else if(theRotation.z < -tiltMax)
        {
            meshHolder.rotation = Quaternion.Euler(new Vector3(theRotation.x, theRotation.y, -tiltMax));
        }

        

        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, userRudder * rotationModifier * shipSpeed * Mathf.Clamp(maxSpeed / shipSpeed,0.0f,5.0f), 0)));
        rb.MovePosition(rb.position + new Vector3(shipSpeed * thrustModifier * Mathf.Sin(Mathf.Deg2Rad * rb.rotation.eulerAngles.y), 0, shipSpeed * thrustModifier * Mathf.Cos(Mathf.Deg2Rad * rb.rotation.eulerAngles.y)));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
            // play some sounds and make an explosion or something
            // or maybe play an animation as well, whatever really
            gameController.endGame();
        }
    }
}

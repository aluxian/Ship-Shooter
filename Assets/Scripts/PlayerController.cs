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

    //
    // Shots
    //
    public GameObject shot;
    public Transform[] shotSpawnsPort;
    public Transform[] shotSpawnsStarboard;
    public float reloadTime;

    public float dragCoefficient; // Multiplication performed with drag - smaller is more drag! Can only be in range 0-1
    
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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auShot = GetComponent<AudioSource>();
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
            }
        }

        if (Input.GetButton("Fire2") && Time.time > nextFireStarboard)
        {
            nextFireStarboard = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsStarboard[x].position, shotSpawnsStarboard[x].rotation);
                auShot.Play();
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
        

        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, userRudder * rotationModifier * shipSpeed, 0)));
        rb.MovePosition(rb.position + new Vector3(shipSpeed * thrustModifier * Mathf.Sin(Mathf.Deg2Rad * rb.rotation.eulerAngles.y), 0, shipSpeed * thrustModifier * Mathf.Cos(Mathf.Deg2Rad * rb.rotation.eulerAngles.y)));
    }
}

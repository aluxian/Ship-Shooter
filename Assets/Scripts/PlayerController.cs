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
    public Vector2 thrustModifier; // (power up, power down)

    public float shipSpeed;

    public float tiltModifier;

    //
    // Shots
    //
    public GameObject shot;
    public Transform[] shotSpawnsPort;
    public Transform[] shotSpawnsStarboard;
    public float reloadTime;

    private float nextFirePort;
    private float nextFireStarboard;

    //
    // Components
    //

    private Rigidbody shipRigidBody;
    private AudioSource shootAudioSource;

    //
    // World
    //

    [SerializeField]
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        shipRigidBody = GetComponent<Rigidbody>();
        shootAudioSource = GetComponent<AudioSource>();
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
                shootAudioSource.Play();
            }
        }

        if (Input.GetButton("Fire2") && Time.time > nextFireStarboard)
        {
            nextFireStarboard = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsStarboard[x].position, shotSpawnsStarboard[x].rotation);
                shootAudioSource.Play();
            }
        }
    }

    // FixedUpdate is called before physics operations
    void FixedUpdate()
    {
        float userRudder = Input.GetAxis("Horizontal");
        float userThrust = Input.GetAxis("Vertical");

        if (userThrust > 0)
        {
            shipRigidBody.AddRelativeForce(new Vector3(0, 0, thrustModifier[0] * userThrust));
        }
        else
        {
            shipRigidBody.AddRelativeForce(new Vector3(0, 0, thrustModifier[1] * userThrust));
        }

        shipRigidBody.AddTorque(new Vector3(0, rotationModifier * shipRigidBody.velocity.magnitude * userRudder, 0));
    }
}

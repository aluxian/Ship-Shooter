using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    public Transform[] shotSpawnsPort;
   
    public Transform[] shotSpawnsStarboard;
    public float reloadTime;

    private float nextFirePort;
    private float nextFireStarboard;

    //
    // Components
    //

    private AudioSource au;
    private Rigidbody rb;

    //
    // World
    //

    [SerializeField]
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFirePort)
        {
            nextFirePort = Time.time + reloadTime;
            for(int x = 0; x < shotSpawnsPort.Length; x++) {
                Instantiate(shot, shotSpawnsPort[x].position, shotSpawnsPort[x].rotation);
                au.Play();
            }
        }
        if (Input.GetButton("Fire2") && Time.time > nextFireStarboard)
        {
            nextFireStarboard = Time.time + reloadTime;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsStarboard[x].position, shotSpawnsStarboard[x].rotation);
                au.Play();
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
            rb.AddRelativeForce(new Vector3(0, 0, (thrustModifier[0] * userThrust)));
        }
        else
        {
            rb.AddRelativeForce(new Vector3(0, 0, (thrustModifier[1] * userThrust)));
        }

        rb.AddTorque(new Vector3(0,rotationModifier * rb.velocity.magnitude * userRudder, 0));
    }
}

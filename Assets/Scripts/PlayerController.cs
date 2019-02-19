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
    public float speedModifier; 
    public Vector2 rudderModifier; // (port, starboard)
    public Vector3 sailModifier; // (y angle, extension, contraction)
    public Vector2 anchorModifier; // (raise, lower)

    public float shipSpeed;
    public Vector2 shipVelocity; // (x,z)
    public Vector2 shipAcceleration; // (x,z)

    public float shipRotationVelocity;
    public float shipRotationAcceleration;

    public float rudderAngle; // 0 = native
    public float rudderSpeed;
    public Vector2 rudderLimits; // (port, starboard)
    
    public Vector2 sailsPosition; // (y angle,extension)
    public Vector3 sailsVelocity; // (y angle, extension)
    public Vector3 sailsAcceleration; // (y angle, extension)

    public float anchorPosition;
    public float anchorVelocity;
    public float anchorAcceleration;

    


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
        float userSteer = Input.GetAxis("Horizontal");
        float userSails = Input.GetAxis("Vertical");


        shipVelocity = new Vector2(
            shipSpeed * speedModifier * Mathf.Sin(rb.rotation.y),
            shipSpeed * speedModifier * Mathf.Cos(rb.rotation.y)
           );

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

    }
}

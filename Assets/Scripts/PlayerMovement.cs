using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //
    // Physics
    //
    public float rotationModifier;
    public float thrustModifier;
    public float dragCoefficient;

    // Physics Limits
    public float shipSpeed;
    public float maxSpeed;

    //
    // Gameplay
    //
    public float tiltModifier;
    public float tiltResetModifier;
    public float tiltMax;

    //
    // Sails
    //

    // Sails position
    public Vector2 sailModifier; // (sails down, sails up)
    public float sailPosition;
    public float sailMin;
    public float sailMax;

    //Sails angle
    /*
    public float sailAngle;
    public float sailAngleMin;
    public float sailAngleMax;
    */
    //Currently only using wind power and ignoring direction stuff

    //
    // World
    //
    
    // Foreign Components
    [SerializeField]
    public Boundary boundary;
    public GameController gameController;

    public WindController wind;


    // Own Components
    private Transform meshHolder; // needed to get rotation to work correctly
    private Rigidbody rb;
    
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshHolder = this.gameObject.transform.Find("MeshHolder").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the inputs
        float userRudder = Input.GetAxis("Horizontal");
        float userSails = Input.GetAxis("Vertical");

        //
        // Sail movement
        //
        if (userSails > 0)
        {
            sailPosition += sailModifier[0] * userSails;
        }
        else if (userSails <= 0)
        {
            sailPosition += sailModifier[1] * userSails;
        }

        if (sailPosition > sailMax)
        {
            sailPosition = sailMax;
        }
        else if (sailPosition < sailMin)
        {
            sailPosition = sailMin;
        }

        //
        // Ship Movement
        // 

        shipSpeed -= shipSpeed * dragCoefficient;
        shipSpeed += Mathf.Abs(thrustModifier * sailPosition * wind.windPower / wind.windMax);

        if (shipSpeed < 0)
        {
            shipSpeed = 0;
        }
        else if (shipSpeed > maxSpeed)
        {
            shipSpeed = maxSpeed;
        }


        //
        // Ship Tilting 
        //
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
        else if (theRotation.z < -tiltMax)
        {
            meshHolder.rotation = Quaternion.Euler(new Vector3(theRotation.x, theRotation.y, -tiltMax));
        }


        // 
        // Movement Application
        //
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, userRudder * rotationModifier * shipSpeed, 0)));
        rb.MovePosition(rb.position + new Vector3(shipSpeed * thrustModifier * Mathf.Sin(Mathf.Deg2Rad * rb.rotation.eulerAngles.y), 0, shipSpeed * thrustModifier * Mathf.Cos(Mathf.Deg2Rad * rb.rotation.eulerAngles.y)));

    }
}

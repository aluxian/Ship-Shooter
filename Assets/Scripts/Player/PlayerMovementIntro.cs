using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementIntro : MonoBehaviour
{
    // Constant speed
    public float shipSpeed;

    // Bobbing up and down while moving
    public Vector3 bobbing;
    public float yTranslationModifier;
 
    // Own Components
    protected Rigidbody rb;
    protected Vector3 startingRotation;
    protected float randPerlinY;
    protected bool hasFired;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingRotation = rb.rotation.eulerAngles;
        randPerlinY = Random.value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = bobbing.x * Mathf.Sin(Time.time + Mathf.PI / 2);
        float z = bobbing.z * (Mathf.PerlinNoise(Time.time, randPerlinY) * 2 - 1) * Mathf.Sin(Time.time);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(x, 0, z) * Time.deltaTime));
        rb.MovePosition(rb.position + new Vector3(shipSpeed, bobbing.x * Mathf.Sin(Time.time - Mathf.PI) * yTranslationModifier, 0));

        // fire
        if (!hasFired && rb.position.x > 0)
        {
            hasFired = true;
        }

        // loop
        if (rb.position.x > 350)
        {
            rb.MovePosition(new Vector3(-350, rb.position.y, rb.position.z));
            hasFired = false; // reset
        }
    }
}

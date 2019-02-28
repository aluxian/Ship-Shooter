using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //
    // Parameters
    //
    public float detectionRadius;
    public float rotationModifier;
    public float maxSpeed;
    public float idleSpeed;
    public bool active;
    public float rotationDelay;
    //
    // Timing
    //
    private float timer;
    private float nextTurn;
    //
    // Components
    //
    protected Rigidbody rb;

    //
    // World
    //
    public GameObject player;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        timer = 0;
        nextTurn = 0;
    }

    private void Update()
    {
        float separation = Vector3.Distance(transform.position, playerTransform.position);
        if(separation <= detectionRadius)
        {
            active = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (active)
        {
            rb.MovePosition(rb.position + new Vector3(maxSpeed *  Mathf.Sin(Mathf.Deg2Rad * rb.rotation.eulerAngles.y), 0, maxSpeed * Mathf.Cos(Mathf.Deg2Rad * rb.rotation.eulerAngles.y)));
            if(timer >= nextTurn)
            {
                float angle = Vector3.Angle(transform.rotation.eulerAngles, playerTransform.rotation.eulerAngles);
                rb.rotation = Quaternion.Euler(new Vector3(0, angle * rotationModifier, 0));
                timer = 0;
                nextTurn = rotationDelay;
            }
        }
    }
}

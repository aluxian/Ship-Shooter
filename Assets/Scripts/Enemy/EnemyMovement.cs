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
    public float timer;
    public float nextTurn;
    //
    // Components
    //
    protected Rigidbody rb;

    //
    // World
    //
    protected GameObject player;
    protected Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        timer = 0;
        nextTurn = 0;
    }

    void Update()
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
                float angle = Mathf.Atan2(playerTransform.position.x - transform.position.x, playerTransform.position.z - transform.position.z);
                //float angle = playerTransform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
                rb.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, angle * Mathf.Rad2Deg, transform.rotation.eulerAngles.z));
                //rb.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                //rb.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + playerTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                timer = 0;
                nextTurn = rotationDelay;
            }
        }
    }
}

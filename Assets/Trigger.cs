using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected GameController gameController;
    protected Death triggerDeath;
    public int id;
    public float timer;
    protected Rigidbody rb;
    protected bool rising;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        triggerDeath = GetComponent<Death>();
        triggerDeath.enabled = false;
        rb = GetComponent<Rigidbody>();
        timer = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.tag);
            gameController.PullTrigger(id);
            Death();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.tag);
            gameController.PullTrigger(id);
            Death();
        }
    }

    protected void FixedUpdate()
    {
        if (rising)
        {
            rb.position += new Vector3(0, 0.01f, 0);
            if(rb.position.y >= 0)
            {
                rb.position = new Vector3(rb.position.x, 0, rb.position.z);
                rising = false;
            }
        }
    }

    protected void Death()
    {
        GetComponent<Collider>().enabled = false;
        triggerDeath.enabled = true;
    }

    protected void Awake()
    {
        Start();
        rb.position -= new Vector3(0, 2, 0);
        rising = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health;
    public int attack;
    public float attackDelay;

    public Time lastAttack;

    public GameObject target;
    private Rigidbody rb;
    private AudioSource[] au;
    public GameObject player;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        au = gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            // do somethings
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}

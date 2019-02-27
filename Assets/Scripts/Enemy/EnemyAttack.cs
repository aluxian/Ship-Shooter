using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameController gameController;
    public GameObject explosion;
    public int strength;
    public float attackDelay;
    public float attackRadius;

    private float nextAttack;
    private float timer;

    protected PlayerHealth playerHealth;
    protected GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        timer = 0;
        nextAttack = 0;
        GameObject result = GameObject.FindWithTag("GameController");
        if (result != null)
        {
            gameController = result.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find the GameController");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        
        if (other.tag == "Player")
        {
            // PlayerController player = (PlayerController)other.gameObject;
            playerHealth.TakeDamage(strength);
            Instantiate(explosion, other.transform.position, transform.rotation);
            timer = 0;
        }
        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(timer >= nextAttack && distance <= attackRadius)
        {
            playerHealth.TakeDamage(strength);
            timer = 0;
            nextAttack = attackDelay;
        }
    }
}

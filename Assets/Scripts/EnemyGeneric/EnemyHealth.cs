using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //
    // Health
    //
    public int maxHealth;
    public int currentHealth;

    //
    // Damage
    //
    bool isDead;
    bool damaged;
    public GameObject explosion;

    public AudioClip deathClip;
    //
    // Components
    //
    //AudioSource playerAudio;
    EnemyMovement movement;
    Collider collider;
    //PlayerShooting playerShooting;



    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        movement = GetComponent<EnemyMovement>();
        collider = GetComponent<Collider>();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;


        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;


        Destroy(gameObject);
        
        Instantiate(explosion, transform.position, transform.rotation);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        
        if (other.tag == "PlayerProjectile")
        {
            TakeDamage(other.GetComponent<ProjectileAttack>().attackStrength);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }

    }
}

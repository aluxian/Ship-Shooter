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
    //PlayerShooting playerShooting;



    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        movement = GetComponent<EnemyMovement>();
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

        //playerShooting.DisableEffects ();


        Destroy(this);
        Instantiate(explosion, transform.position, transform.rotation);
        //playerShooting.enabled = false;
    }
}

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
    public AudioClip painSound;
    //
    // Components
    //
    //AudioSource playerAudio;
    EnemyMovement enemyMovement;
    EnemyAttack enemyAttack;
    Collider collider;
    AudioSource au;
    Death enemyDeath;
    GameController gameController;
    //PlayerShooting playerShooting;



    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        collider = GetComponent<Collider>();
        au = GetComponent<AudioSource>();
        enemyDeath = GetComponent<Death>();
        gameController = GameObject.FindObjectOfType<GameController>();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        enemyDeath.enabled = false;
    }

    protected void Update()
    {
        if (damaged)
        {
            au.PlayOneShot(painSound);
        }
        damaged = false;
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


        //Destroy(gameObject);
        enemyMovement.enabled = false;
        enemyAttack.enabled = false;
        enemyDeath.enabled = true;

        Instantiate(explosion, transform.position, transform.rotation);
        gameController.numEnemies -= 1;
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

    public void PlayPainSound()
    {
        au.PlayOneShot(painSound);
    }
}

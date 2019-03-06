using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
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

    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    //
    // World
    //
    public Slider healthSlider;
    private GameController gameController;

    //
    // Components
    //
    //AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Death playerDeath;
    //PlayerShooting playerShooting;

    private float respawnAfter;

    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerDeath = GetComponent<Death>();
        gameController = GameObject.FindObjectOfType<GameController>();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        healthSlider.value = currentHealth;
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if (respawnAfter != 0 && Time.time > respawnAfter)
        {
            isDead = false;
            playerMovement.enabled = true;
            playerMovement.reset();
            playerAttack.enabled = true;
            playerDeath.enabled = false;
            playerDeath.reset();
            respawnAfter = 0;
            currentHealth = 100;
            healthSlider.value = currentHealth;
        }
    }


    public void TakeDamage(int amount)
    {
        if (!isDead)
        {
            damaged = true;

            currentHealth -= amount;

        }
        healthSlider.value = currentHealth;


        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        playerDeath.enabled = true;
        //gameController.respawn();
        respawnAfter = Time.time + 3;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackIntro : MonoBehaviour
{
    //
    // Shots
    //
    public GameObject shot;
    public Transform[] shotSpawnsPort;

    //
    // Game Logic
    //
    protected bool hasFired;

    //
    // Components
    //
    protected Rigidbody rb;
    protected AudioSource auShot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auShot = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.x < 0)
        {
            hasFired = false;
        }

        if (!hasFired && rb.position.x > 0)
        {
            hasFired = true;
            for (int x = 0; x < shotSpawnsPort.Length; x++)
            {
                Instantiate(shot, shotSpawnsPort[x].position, shotSpawnsPort[x].rotation);
                auShot.Play();
            }
        }
    }
}

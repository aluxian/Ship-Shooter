using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenMovement : EnemyMovement
{
    //
    // Parameters
    //
    public float depth; // POSITIVE FLOAT
    public float raiseHeight; // maximum height
    public float[] delays; // to go up, to go down
    public float[] speeds; // going up, going down, going forwards

    //
    // Timing
    //
    private float nextMove;
    public float btimer;
    private bool moving;
    private float velocity;
    public bool emerged; // false = up, true = down

    void Start()
    {
        nextMove = delays[1];
        btimer = 0;
        moving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        btimer += Time.deltaTime;
        //
        // Raising/Lowering
        //

        if (btimer >= nextMove)
        {
            btimer = 0;
            if (emerged == false)
            {
                nextMove = delays[1];
                velocity = speeds[0] * Time.deltaTime;
                moving = true;
            }
            else if (emerged == true)
            {
                nextMove = delays[0];
                velocity = -speeds[1] * Time.deltaTime;
                moving = true;
            }
        }
        if (moving)
        {
            if(transform.position.y <= -depth && emerged)
            {
                moving = false;
                velocity = 0;
                transform.position = new Vector3(transform.position.x, -depth, transform.position.z);
                emerged = false;
            }
            else if(transform.position.y >= raiseHeight && !emerged)
            {
                moving = false;
                velocity = 0;
                transform.position = new Vector3(transform.position.x, raiseHeight, transform.position.z);
                emerged = true;
            }
            else
            {
                transform.position += new Vector3(0, velocity, 0);
            }
        }
    }
}

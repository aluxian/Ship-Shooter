using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovement : EnemyMovement
{
    public float diveDelay;
    public float diveDepth;
    public float diveTimer;
    public float nextDive;
    public float diveSpeedModifier;
    public float diveTime;
    public bool diving;
    public float raiseHeight;
    public float diveDistance;
    public bool emerged;

    private float distanceCovered;

    private Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        diveTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        diveTimer += Time.deltaTime;
        base.Update();
        if(diveTimer >= nextDive)
        {
            nextDive = diveDelay;
            diveTimer = 0;
            diving = true;
            float angle = Mathf.Atan2(playerTransform.position.x - transform.position.x, playerTransform.position.z - transform.position.z);
            rb.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, angle * Mathf.Rad2Deg, transform.rotation.eulerAngles.z));
            distanceCovered = 0;
        }
    }

    private void FixedUpdate()
    {
        if (diving)
        {
            rb.MovePosition(rb.position + new Vector3(maxSpeed * diveSpeedModifier * Mathf.Sin(Mathf.Deg2Rad * rb.rotation.eulerAngles.y), 0, maxSpeed * diveSpeedModifier * Mathf.Cos(Mathf.Deg2Rad * rb.rotation.eulerAngles.y)));
            distanceCovered += maxSpeed * diveSpeedModifier;
            if(distanceCovered >= diveDistance)
            {
                diving = false;
                distanceCovered = 0;
            }
        }
        else
        {
            base.FixedUpdate();
        }

        if(!diving && !emerged)
        {
            rb.MovePosition(rb.position + new Vector3(0, diveSpeedModifier * maxSpeed, 0));
            if(rb.position.y >= raiseHeight)
            {
                emerged = true;
                rb.position = new Vector3(rb.position.x, raiseHeight, rb.position.z);
                diveTimer = 0;
            }
        }
        else if(diving && emerged)
        {
            rb.MovePosition(rb.position + new Vector3(0, -diveSpeedModifier * maxSpeed, 0));
            if (rb.position.y <= diveDepth)
            {
                emerged = false;
                rb.position = new Vector3(rb.position.x, -diveDepth, rb.position.z);
                diveTimer = 0;
            }
        }
    }
}

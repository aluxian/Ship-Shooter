using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : EnemyMovement
{

    private EnemyIdleRotate idle;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        idle = gameObject.GetComponent<EnemyIdleRotate>();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (active)
        {
            idle.active = false;
        }
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

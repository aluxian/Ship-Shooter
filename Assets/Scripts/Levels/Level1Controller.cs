using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : GameController
{
    public int stage;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        base.defaultSpawnBehaviour = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

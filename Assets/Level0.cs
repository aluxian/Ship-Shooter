using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : GameController
{
    protected Trigger[] triggers;
    public int stage1Count = 0;
    protected int stage2Count = 0;
    public int stage1Target = 7;
    protected int stage2Target = 7;
    protected bool[] stageSetUp;

    public float level2_delay = 2;

    protected GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        base.defaultSpawnBehaviour = false;
        triggers = GameObject.FindObjectsOfType<Trigger>();
        stageSetUp = new bool[5];
        for (int x = 0; x < triggers.Length; x++)
        {
            //triggers[x].gameObject.GetComponent<Rigidbody>().Sleep();
            triggers[x].gameObject.SetActive(false);
        }
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(stage1Count >= stage1Target && stage == 0)
        {
            stage += 1;
        }
        else if(stage2Count >= stage2Target && stage == 1)
        {
            stage += 1;
        }
        if(stage == 0 && !stageSetUp[0])
        {
            for (int x = 8; x <= 14; x++)
            {
                //triggers[x].gameObject.GetComponent<Rigidbody>().Sleep();
                triggers[x].gameObject.SetActive(true);
                triggers[x].gameObject.GetComponent<Trigger>().Awake();
                stageSetUp[0] = true;
            }
        }
        else if(stage == 1 && !stageSetUp[1])
        {
            for(int x = 0; x <= 7; x++)
            {
                triggers[x].gameObject.SetActive(true);
                triggers[x].gameObject.GetComponent<Trigger>().Awake();
                stageSetUp[1] = true;
            }
        }
        else if(stage == 2 && !stageSetUp[2])
        {
            GameObject kraken = Instantiate(spawnEnemies[0], player.transform.position + new Vector3(20, 0, 10), Quaternion.Euler(new Vector3(0, 0, 0)));
            kraken.GetComponent<KrakenMovement>().Awake();
            stageSetUp[2] = true;
            numEnemies = 1;
        }
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void PullTrigger(int id)
    {
        if(id <= 6)
        {
            stage1Count += 1;
            //output some text or something to say that they've done that and should follow the path
        }
        else if(id >= 7 && id <= 14)
        {
            stage2Count += 1;
        }
    }
}

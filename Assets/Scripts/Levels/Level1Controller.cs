using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : GameController
{

    //public InfoMessageUIController infoMessageController;
    public GameObject levelEndHUD;
    public StoryPromptControllerLevel1 storyCtrl;

    //public AudioClip shiverSound;
    //public AudioClip treasureSound;

    //private bool shiverSoundPlayed;
    //private bool treasureSoundPlayed = true;

    //protected Trigger[] triggers;
    //public int stage1Count = 0;
    //protected int stage2Count = 0;
    //public int stage1Target = 7;
    //protected int stage2Target = 8;
    //protected bool[] stageSetUp;

    //public Trigger[] Stage0Triggers;
    //public Trigger[] Stage1Triggers;

    //public float level1_delay = 2;

    //private float shouldHideTooltipAt;

    //protected GameObject player;

    //private float showShootTooltipAt;
    private float endLevelAt;
    private float startLevelEndAt;

    //private GameObject kraken;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        base.defaultSpawnBehaviour = false;
        //triggers = GameObject.FindObjectsOfType<Trigger>();
        //stageSetUp = new bool[5];
        //for (int x = 0; x < triggers.Length; x++)
        //{
        //    //triggers[x].gameObject.GetComponent<Rigidbody>().Sleep();
        //    triggers[x].gameObject.SetActive(false);
        //}
        //player = GameObject.Find("Player");
        //infoMessageController.Hide();
    }

    private bool krak;

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (numEnemies == -3)
        {
            numEnemies = 0;
            startLevelEndAt = Time.time + 5;
        }

        if (startLevelEndAt != 0 && Time.time > startLevelEndAt)
        {
            storyCtrl.ShowEndStory();
            startLevelEndAt = 0;
        }

        /*if (stage1Count == 1 && !krak)
        {
            krak = true;
            stage = 2;
        }*/

        //if (stage1Count >= stage1Target && stage == 0)
        //{
        //    stage += 1;
        //}
        //else if (stage2Count >= stage2Target && stage == 1)
        //{
        //    stage += 1;

        //    au.clip = shiverSound;
        //    au.Play();

        //    infoMessageController.AnimateShow("Arr naw. What was that sound?");
        //    shouldHideTooltipAt = Time.time + 5;
        //    showShootTooltipAt = Time.time + 8;
        //}

        //if (stage == 0 && !stageSetUp[0])
        //{
        //    for (int x = 0; x < Stage0Triggers.Length; x++)
        //    {
        //        //triggers[x].gameObject.GetComponent<Rigidbody>().Sleep();
        //        Stage0Triggers[x].gameObject.transform.position = new Vector3(
        //            Stage0Triggers[x].gameObject.transform.position.x,
        //            -4,
        //            Stage0Triggers[x].gameObject.transform.position.z
        //        );
        //        Stage0Triggers[x].gameObject.SetActive(true);
        //        Stage0Triggers[x].gameObject.GetComponent<Trigger>().Awake();
        //        stageSetUp[0] = true;
        //    }
        //}
        //else if (stage == 1 && !stageSetUp[1])
        //{
        //    for (int x = 0; x < Stage1Triggers.Length; x++)
        //    {
        //        Stage1Triggers[x].gameObject.transform.position = new Vector3(
        //           Stage1Triggers[x].gameObject.transform.position.x,
        //           -4,
        //           Stage1Triggers[x].gameObject.transform.position.z
        //       );
        //        Stage1Triggers[x].gameObject.SetActive(true);
        //        Stage1Triggers[x].gameObject.GetComponent<Trigger>().Awake();
        //        stageSetUp[1] = true;
        //    }
        //}
        //else if (stage == 2 && !stageSetUp[2])
        //{
        //    kraken = Instantiate(spawnEnemies[0], player.transform.position + new Vector3(20, -30, 10), Quaternion.Euler(new Vector3(0, 0, 0)));
        //    kraken.GetComponent<KrakenMovement>().Awake();
        //    stageSetUp[2] = true;
        //    numEnemies = 1;
        //}

        //if (stage == 2 && numEnemies == 0 && endLevelAt == 0)
        //{
        //    print("kraken died");
        //    endLevelAt = Time.time + 8;
        //}

        //if (shouldHideTooltipAt != 0 && shouldHideTooltipAt < Time.time)
        //{
        //    infoMessageController.AnimateHide();
        //    shouldHideTooltipAt = 0;
        //    print("hiding tooltip");
        //}

        //if (showShootTooltipAt != 0 && showShootTooltipAt < Time.time)
        //{
        //    kraken.GetComponent<EnemyHealth>().PlayPainSound();
        //    infoMessageController.AnimateShow("Shoot with LMB/RMB for port/starboard.");
        //    shouldHideTooltipAt = Time.time + 5;
        //    showShootTooltipAt = 0;
        //}

        if (endLevelAt != 0 && endLevelAt < Time.time && !levelEndHUD.activeSelf)
        {
            levelEndHUD.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void EndStoryDismissed()
    {
        endLevelAt = Time.time + 1;
    }

    //public override void PullTrigger(int id)
    //{
    //    if (id <= 6)
    //    {
    //        stage1Count += 1;
    //        //output some text or something to say that they've done that and should follow the path
    //    }
    //    else if (id >= 7 && id <= 14)
    //    {
    //        stage2Count += 1;
    //    }
    //}

    //public override void IntroStoryDismissed()
    //{
    //    base.IntroStoryDismissed();
    //    infoMessageController.AnimateShow("Press W/S to control the sails and A/D to control the rudder.");
    //    shouldHideTooltipAt = Time.time + 5;
    //}
}

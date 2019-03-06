using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Rigidbody rb;
    public float depth;
    public float sinkSpeed;
    private AudioSource au;
    public AudioClip[] clips;
    public float[] delays;
    private bool[] played;
    public float timer;
    private bool sinking;

    public float destructionDelay;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        au = gameObject.GetComponent<AudioSource>();
        timer = 0;
        played = new bool[delays.Length];
        sinking = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        for (int x = 0; x < delays.Length; x++)
        {
            if (timer >= delays[x] && !played[x])
            {
                played[x] = true;
                au.PlayOneShot(clips[x]);
            }
        }
        if (sinking && rb.position.y >= -depth)
        {
            rb.position += new Vector3(0, -sinkSpeed, 0);
            if (rb.position.y <= -depth)
            {
                rb.position = new Vector3(rb.position.x, -depth, rb.position.z);
                sinking = false;
            }
        }

        if (timer >= destructionDelay)
        {
            Destroy(this.gameObject);
        }
    }

    public void reset()
    {
        Start();
    }
}

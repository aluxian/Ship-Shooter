using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementIntro : MonoBehaviour
{
    public float modifier;
    public Vector3 target;
    private Rigidbody rb;
    protected Vector3 speed;
    protected Vector3 initialScale;
    protected float initialZDist;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(target.x + (Random.value - 0.5f) * 100, target.y + (Random.value - 0.5f) * 100, target.z + (Random.value - 0.5f) * 100);
        rb = GetComponent<Rigidbody>();
        speed = (target - rb.position) * modifier;
        initialScale = rb.transform.localScale;
        rb.transform.localScale = new Vector3(0.00001f, 0.00001f, 0.00001f);
        initialZDist = target.z - rb.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + speed);
        rb.transform.localScale = initialScale * ((initialZDist - (target.z - rb.position.z)) / initialZDist);

        if (rb.position.z > 900)
        {
            Destroy(gameObject);
        }
    }
}

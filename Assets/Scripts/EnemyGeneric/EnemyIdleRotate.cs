using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleRotate : MonoBehaviour
{
    public Vector3 pivot;
    public float angularVelocity;
    public float radius;
    public bool active;

    protected Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            transform.RotateAround(pivot, Vector3.up, angularVelocity * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnactBoundary : MonoBehaviour
{
    //
    // Parameters
    //
    public float xmin, xmax, zmin, zmax;
    public float bounce;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = transform.position.x;
        float z = transform.position.z;
        
        if(x <= xmin)
        {
            Vector3 reflect1 = Vector3.Reflect(rb.rotation.eulerAngles, new Vector3(1, 0, 0));
            //Vector3 reflect2 = Vector3.Reflect(rb.position, new Vector3(0, 0, 1));
            
            //rb.position = reflect2;
            rb.rotation *= Quaternion.Euler(Mathf.Deg2Rad * reflect1);
            if(x < xmin)
            {
                rb.position = new Vector3(xmin + bounce, rb.position.y, rb.position.z);
            }
        }
        else if(x >= xmax)
        {
            Vector3 reflect1 = Vector3.Reflect(rb.rotation.eulerAngles, new Vector3(-1, 0, 0));

            rb.rotation *= Quaternion.Euler(Mathf.Deg2Rad * reflect1);
            if (x > xmax)
            {
                rb.position = new Vector3(xmax - bounce, rb.position.y, rb.position.z);
            }
        }
        else if(z <= zmin)
        {
            Vector3 reflect1 = Vector3.Reflect(rb.rotation.eulerAngles, new Vector3(0, 0, 1));
            
            rb.rotation *= Quaternion.Euler(Mathf.Deg2Rad * reflect1);
            if (z < zmin)
            {
                rb.position = new Vector3(rb.position.x, rb.position.y, zmax + bounce);
            }
        }
        else if (z >= zmax)
        {
            Vector3 reflect1 = Vector3.Reflect(rb.rotation.eulerAngles, new Vector3(0, 0, -1));
            
            rb.rotation *= Quaternion.Euler(Mathf.Deg2Rad * reflect1);
            if (z > xmax)
            {
                rb.position = new Vector3(rb.position.x, rb.position.y, zmax - bounce);
            }
        }
    }
}

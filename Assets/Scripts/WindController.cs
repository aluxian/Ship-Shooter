using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public float windPower;
    public float windMax;
    public float windMin;
    public int directionMin;
    public int directionMax;
    public float randomicityPower;
    public float randomicityDirection;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == 0)
        {
            direction = (int)Random.Range(0, 360);
        }
        if (windPower == 0)
        {
            windPower = Random.Range(windMin, windMax);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction += (int)Random.Range(-randomicityDirection, randomicityDirection);
        windPower += Random.Range(-randomicityPower * windMax, randomicityPower * windMax);
        windPower = Mathf.Clamp(windPower, windMin, windMax);
        while(direction > 360)
        {
            direction -= 360;
        }
        while(direction < 0)
        {
            direction += 360;
        }
        if (direction < directionMin)
        {
            direction = directionMin;
        }
        else if(direction > directionMax)
        {
            direction = directionMax;
        }
    }
}

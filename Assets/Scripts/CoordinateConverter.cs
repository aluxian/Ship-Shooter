using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class CoordinateConverter
{
    public static Vector2 CartesianToPolar(Vector3 point)
 {
        Vector2 polar = new Vector2();
 
     //calc longitude
     polar.y = Mathf.Atan2(point.x, point.z);
 
     //this is easier to write and read than sqrt(pow(x,2), pow(y,2))!
     var xzLen = new Vector2(point.x, point.z).magnitude;
    //atan2 does the magic
    polar.x = Mathf.Atan2(-point.y,xzLen);
 
     //convert to deg
     polar *= Mathf.Rad2Deg;
 
     return polar;
 }
 
}
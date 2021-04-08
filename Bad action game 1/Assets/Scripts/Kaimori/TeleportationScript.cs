using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    public float x, y;

    public static float xd, yd;

    private void Start()
    {
        xd = x; yd = y;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyShotPlayer : MonoBehaviour
{
    public string shotbutton;
    private bool gunbut = false;
    public bool Gunbut()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gunbut = true;
        }
        else
        {
            gunbut = false;
        }
        return gunbut;
    }


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}

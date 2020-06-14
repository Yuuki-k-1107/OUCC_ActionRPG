using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameractlr : MonoBehaviour
{
    GameObject you;
    // Start is called before the first frame update
    void Start()
    {
        this.you = GameObject.Find("arpg-player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 yourposition = this.you.transform.position;
        if (yourposition.x >= 0 && yourposition.y >= 0)
        {
            transform.position = new Vector3(yourposition.x, yourposition.y, transform.position.z);
        }
        else if (yourposition.x >= 0)
        {
            transform.position = new Vector3(yourposition.x, 0, transform.position.z);
        }
        else if (yourposition.y >= 0)
        {
            transform.position = new Vector3(0, yourposition.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Stage1");
            Plyrctlr.maxHP = 150;
            Plyrctlr.curEXP = 0;
            Plyrctlr.Level = 1;
            Plyrctlr.Attack = 5;
        }
    }
}

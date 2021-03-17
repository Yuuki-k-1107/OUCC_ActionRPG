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
            SceneManager.LoadScene("Stage1-1");
            PlayerController.maxHP = 150;
            PlayerController.curEXP = 0;
            PlayerController.Level = 1;
            PlayerController.Attack = 5;
            PlayerController.Defense = 3;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    #region
    [Header("遷移先")] public string destination;
    #endregion


    public static string next;

    private void Start()
    {
        next = destination;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided on player");
    }
}

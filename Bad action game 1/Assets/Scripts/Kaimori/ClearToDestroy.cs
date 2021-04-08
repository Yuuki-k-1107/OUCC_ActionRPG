using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearToDestroy : MonoBehaviour
{
    private int stageNumber = 3;
    public GameObject[] blockers = new GameObject[3];

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<stageNumber;i++)
        {
            blockers[i].gameObject.SetActive(!PlayerController.hasCleared[i]);
        }
    }
}

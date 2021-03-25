using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillToAppear : MonoBehaviour
{
    public GameObject target1, target2, target3;
    public GameObject killToShow1, killToShow2, killToShow3;
    void Start()
    {
        if (killToShow1 != null)
        {
            killToShow1.gameObject.SetActive(false);
        }

        if (killToShow2 != null)
        {
            killToShow2.gameObject.SetActive(false);
        }

        if (killToShow3 != null)
        {
            killToShow3.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (target1 == null || !target1.activeInHierarchy)
        {
            Debug.Log("OK:1");
            if (target2 == null || !target2.activeInHierarchy)
            {
                Debug.Log("OK:2");
                if (target3 == null || !target3.activeInHierarchy)
                {
                    Debug.Log("OK:3 then,create this object:)");

                    if (killToShow1 != null)
                    { 
                        killToShow1.gameObject.SetActive(true);
                    }

                    if (killToShow2 != null)
                    {
                        killToShow2.gameObject.SetActive(true);
                    }

                    if (killToShow3 != null)
                    {
                        killToShow3.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}

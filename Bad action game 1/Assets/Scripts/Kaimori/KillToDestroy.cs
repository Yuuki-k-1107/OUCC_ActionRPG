using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillToDestroy : MonoBehaviour
{
    public GameObject target1, target2, target3;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(target1 == null || !target1.gameObject.activeInHierarchy)
        {
            Debug.Log("OK:1");
            if(target2 == null || !target2.gameObject.activeInHierarchy)
            {
                Debug.Log("OK:2");
                if (target3 == null || !target3.gameObject.activeInHierarchy)
                {
                    Debug.Log("OK:3 then,destroy this object:)");
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}

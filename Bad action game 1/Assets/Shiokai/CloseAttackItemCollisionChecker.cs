using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackItemCollisionChecker : MonoBehaviour
{

    // このオブジェクトに触れたときに取得する近接武器のindex
    [SerializeField]
    [Header("近接武器のid")]private int CloseWeponIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            WeponIndexContainer.CloseWeponIndex = this.CloseWeponIndex;
        }

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

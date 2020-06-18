using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotGun : MonoBehaviour
{
    [Header("弾丸")] public GameObject bulObj;//bullet object
    [Header("発射ボタン")] public OnlyShotPlayer gunbut;//gun button
    [Header("発射アニメ")] public string stanim;//shoot anime
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool shot = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame+
    void Update()
    {
        shot = gunbut.Gunbut();
        if (shot)
        {
            Debug.Log("shot");
            GameObject bul = Instantiate(bulObj);
            bul.transform.SetParent(transform);
            bul.transform.position = bulObj.transform.position;
            bul.SetActive(true);
        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool(stanim, shot);
    }
}

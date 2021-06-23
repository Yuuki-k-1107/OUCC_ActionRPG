using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWormBody : MonoBehaviour
{
    [Header("何番目の体 1~6")] public int BodyNumber = 1;
    [Header("頭パーツ")] public GameObject WormHead;
    [Header("自分より一つ頭側のパーツ")] public GameObject WormParent;

    [Header("画像差分")] public Sprite normal;
    public Sprite tail;

    private ObjectCollision oc = null;
    private Rigidbody2D rb = null;
    private Vector3 positionHead;
    private float battleAreaYmin;
    private float speedHead;
    private Vector3 defaultPos;
    private Vector3 latestPos;
    private int Bodylength = 7;
    private Vector3 moveForward;
    private bool started = false;
    private float timer = 0.0f;


    BossWormHead WormHeadScript;
    BossWormHead colliderTriggerParent;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";
        oc = GetComponent<ObjectCollision>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        WormHeadScript = WormHead.GetComponent<BossWormHead>();
        defaultPos =WormHead.transform.position;
        latestPos = transform.position;
        positionHead = WormHead.transform.position;
        battleAreaYmin = defaultPos.y - (WormHeadScript.battleAreaY * 7.5f / 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!started && timer > Mathf.Sqrt(BodyNumber*2)-2f) 
        {
            started = true;
            timer = 0.0f;
        }
        Bodylength = WormHeadScript.bodyNumPub;
        if((Bodylength-1) == BodyNumber)
        {
            spriteRenderer.sprite = tail;
        }
        if ((Bodylength - 1) < BodyNumber)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WormHeadScript.RelayOnTriggerEnter(collision);
    }

    void FixedUpdate()
    {
        if (started)
        {
            speedHead = WormHeadScript.speedAbsPublic;
            
            /*if (positionHead.y <= battleAreaYmin + 5.0f)
            {
                timer = 0.0f;
                started = false;
                transform.position = WormHead.transform.position + Vector3.down * 5 * BodyNumber;
            }*/
            //rb.MovePosition(WormHead.transform.position + Vector3.down * BodyNumber * 10f);いらんかったけどいつか使うかもしれん(汚部屋住民)
           
            if ((transform.position-WormParent.transform.position).magnitude > 0.8f)
            {
                Vector3 currentParentPos = WormParent.transform.position;
                Vector3 moveForwardtemp = (currentParentPos - transform.position).normalized;
                Vector3 moveForward = Vector3.Lerp(moveForwardtemp, (currentParentPos - transform.position).normalized, 0.0f);

                rb.velocity = new Vector3(moveForward.x * speedHead, moveForward.y * speedHead, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector3(0,0,0);
            }

            Vector2 diff = transform.position - latestPos;
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.down, diff);
                latestPos = transform.position;
            }
        }
        
    }
}

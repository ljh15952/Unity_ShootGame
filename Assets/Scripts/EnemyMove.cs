using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public int Hp;
    public Bullet b;
    Rigidbody2D EnemyRb;
    public GameObject origin;
    public PlayerMove PM;
    public int Shotnum;

    public BulletManager Bulletmng;
    public Transform ShotPos;
    public float bulletdelayTime;

    public GameObject BombAni;

    public GameObject Item;

    CircleCollider2D Col;

    public RealBoss realboss;
    // Use this for initialization
    void Start()
    {
        Hp = 5;
        EnemyRb = GetComponent<Rigidbody2D>();
        Col = GetComponent<CircleCollider2D>();
        if (Shotnum == 2)
        {
            StartCoroutine("Shot");
        }
        if (Shotnum == 3)
        {
            StartCoroutine("Shot2");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PM.isGameStart && Shotnum != 3 && realboss.isSpawn)
            gameObject.transform.Translate(Vector2.down * 1.1f * Time.fixedDeltaTime);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            b = collision.GetComponent<Bullet>();
            b.die();
            Hp--;
            if (Hp <= 0)
            {
                PM.TotalKill++;
                MonDie();
            }
        }
        if (collision.gameObject.CompareTag("END"))
        {
            MonDie();
        }
    }

    public void MonDie()
    {
        if (Shotnum == 1)
        {
            gameObject.transform.position = new Vector2(Random.Range(-2.2f, 2.5f), 11);
        }
        if(Shotnum ==2 )
        {
            BombAni.SetActive(true);
            StartCoroutine("Spawn");
            
        }
        else if(Shotnum ==3 )
        {
            BombAni.SetActive(true);
            Item.SetActive(true);
            gameObject.transform.position = new Vector2(2000,2000);
        }
           Hp = 5;
    }


    IEnumerator Spawn()
    {
        Col.enabled = false;
        origin.SetActive(false);
        yield return new WaitForSeconds(1.8f);
        origin.SetActive(true);
        gameObject.transform.position = new Vector2(Random.Range(-2.2f, 2.5f), 11);
        //gameObject.transform.rotation = Quaternion.Euler(0, 0,45);
        Col.enabled = true;
        BombAni.SetActive(false);
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletdelayTime);
            Bulletmng.RequestFireBullet(ShotPos.position, -90.0f, 3.0f);
        }
    }

    IEnumerator Shot2()
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletdelayTime);
            Transform playerTrans = GameObject.Find("Player").GetComponent<Transform>();

            float xPos = playerTrans.position.x - ShotPos.position.x;
            float yPos = playerTrans.position.y - ShotPos.position.y;

            float angle = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;


            Bulletmng.RequestFireBullet(ShotPos.position, angle, 5.0f);
            Bulletmng.RequestFireBullet(ShotPos.position + (Vector3.left * 0.2f), angle - 10, 5.0f);
            Bulletmng.RequestFireBullet(ShotPos.position + (Vector3.right * 0.2f), angle + 10, 5.0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody2D HeroRb;
    public float speed;
    public Bullet b;
    public EnemyMove EM;
    int HpCount;
    public GameObject Hp1;
    public GameObject Hp2;
    public GameObject Hp3;
    public bool isGameStart;
    public int ShotNum;
    public GameObject ReBt;

    public int TotalKill;


    // Use this for initialization
    void Start () {
        Screen.SetResolution(450, 800, false);

        TotalKill = 0;
        HeroRb = GetComponent<Rigidbody2D>();
        HpCount = 3;
        ShotNum = 1;
        isGameStart = true;
	}
	
	// Update is called once per frame


    private void FixedUpdate()
    {
        if(isGameStart)
        HeroMove();


        Debug.Log(TotalKill);

    }

    void HeroMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Debug.Log("SDSD");
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
           //  HeroRb.velocity = Vector2.up * speed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
             transform.Translate(Vector2.down * speed * Time.fixedDeltaTime);
          //  HeroRb.velocity = Vector2.down * speed * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
         //   HeroRb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
             transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
         //   HeroRb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            b = collision.GetComponent<Bullet>();
           
            Hit();
            b.die();
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
            ShotNum++;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Hit();
            EM =  collision.GetComponent<EnemyMove>();
            EM.MonDie();
        }
    }

    void Hit()
    {
        switch (HpCount)
        {
            case 1:
                Hp1.SetActive(false);
                break;
            case 2:
                Hp2.SetActive(false);
                break;
            case 3:
                Hp3.SetActive(false);
                break;
        }

        HpCount--;

        if (HpCount <= 0)
        {
            ReBt.SetActive(true);
            isGameStart = false;
        }
    }
}

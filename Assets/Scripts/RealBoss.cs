using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RealBoss : MonoBehaviour {


    public bool isSpawn;
   public PlayerMove PM;
    public bool CaniMove;
    public Bullet b;
    bool RightorLeft;
    public BulletManager bulletMng;
    public Transform firePos;
    public Transform firePos2;
    public Transform firePos3;
    public float bulletInterval;
    public GameObject BombAni;
    public GameObject origin;
    public GameObject ClearText;


    public GameObject ReBt;

    public int JSM;


    public int Hp;
    bool islive;
    int ShotCount;
    int whatshot;
    bool isfired;
    float offsetAngle;
    // Use this for initialization
    void Start () {
        isSpawn = true;
        CaniMove = false;
        RightorLeft = false;
        islive = true;
        isfired = false;
        ShotCount = 0;
        whatshot = 0;
        offsetAngle = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (PM.TotalKill >=50   && gameObject.transform.position.y>3.8)
        {
            isSpawn = false;
            gameObject.transform.Translate(Vector2.down * 1.1f * Time.fixedDeltaTime);
        }
        if(gameObject.transform.position.y <= 3.8)
        {
            CaniMove = true;
        }
        if(CaniMove&& islive&& PM.isGameStart)
        {
            if (RightorLeft)
            {
                gameObject.transform.Translate(Vector2.right * 1.1f * Time.fixedDeltaTime);
                if(gameObject.transform.position.x>=1.74)
                RightorLeft = !RightorLeft;
            }
            else if (!RightorLeft)
            {
                gameObject.transform.Translate(Vector2.left * 1.1f * Time.fixedDeltaTime);
                if (gameObject.transform.position.x <= -2.3)
                    RightorLeft = !RightorLeft;
            }
        }

        if(!islive)
        {
            PM.transform.Translate(Vector2.up * 2.0f * Time.fixedDeltaTime);
            if(PM.transform.position.y>3.81&& JSM != 100)
            {
                SceneManager.LoadScene("b");
            }
            else if(JSM == 100)
            {
                ReBt.SetActive(true);
            }
        }

        if (CaniMove)
        {
            if(!isfired&&PM.isGameStart)
            BossShot();
        }

    }

    void BossShot()
    {
        switch (whatshot)
        {
            case 0:
                    StartCoroutine("One");
                    isfired = true;
                    break;
            case 1:
                StartCoroutine("cross");
                isfired = true;
                break;
            case 2:
                StartCoroutine("ANG");
                isfired = true;
                break;
            case 3:
                StartCoroutine("ANG2");
                isfired = true;
                break;
        }
        Changepattern();
    }

    IEnumerator One()
    {
            bulletMng.RequestFireBullet(firePos.position, -90.0f, 5.0f);
            yield return new WaitForSeconds(0.05f);
        isfired = false;
        ShotCount++;
    }

    IEnumerator cross()
    {
        yield return new WaitForSeconds(bulletInterval);
        Transform playerTrans = GameObject.Find("Player").GetComponent<Transform>();

        float xPos = playerTrans.position.x - firePos.position.x;
        float yPos = playerTrans.position.y - firePos.position.y;

        float angle = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;


        bulletMng.RequestFireBullet(firePos.position, angle, 5.0f);
        bulletMng.RequestFireBullet(firePos.position + (Vector3.left * 0.2f), angle - 10, 5.0f);
        bulletMng.RequestFireBullet(firePos.position + (Vector3.right * 0.2f), angle + 10, 5.0f);
        isfired = false;
        ShotCount++;
    }


    IEnumerator ANG()
    {
        int lineCount = 4;
      
            for (int i = 0; i < lineCount; i++)
            {
                bulletMng.RequestFireBullet(firePos.position, ((360 / lineCount) * i) + offsetAngle, 5);
            }   
            offsetAngle += 4;
            yield return new WaitForSeconds(0.04f);
        isfired = false;
        ShotCount++;
    }

    IEnumerator ANG2()
    {
        int lineCount = 4;

        for (int i = 0; i < lineCount; i++)
        {
            bulletMng.RequestFireBullet(firePos.position, ((360 / lineCount) * i) + offsetAngle, 5);
            bulletMng.RequestFireBullet(firePos2.position, ((360 / lineCount) * i) + offsetAngle, 5);
            bulletMng.RequestFireBullet(firePos3.position, ((360 / lineCount) * i) + offsetAngle, 5);
        }
        offsetAngle += 20;
        yield return new WaitForSeconds(0.5f);
        isfired = false;
        ShotCount++;
    }


    void Changepattern()
    {
        if(ShotCount>10)
        {
            ShotCount = 0;
            whatshot = Random.Range(0, 4);
        }
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
                BombAni.SetActive(true);
                islive = false;
                PM.isGameStart = false;
                origin.SetActive(false);

                if(JSM==100)
                {
                    ClearText.SetActive(true);
                }

            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public BulletManager bulletMng;
    public Transform firePos;

    public float bulletInterval;

    bool isFire;
    bool isFired;

    // Use this for initialization
    void Start()
    {
        isFire = false;
        isFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFired)
        {
            isFire = true;

            int rand = Random.Range(0, 4);

            switch (rand)
            {
                case 0: StartCoroutine("One"); break;
                case 1: StartCoroutine("Three"); break;
                case 2: StartCoroutine("Daia"); break;
                case 3: StartCoroutine("cross"); break;
            }



            //if(rand == 0)
            //{
            //    StartCoroutine("One");
            //}
            //else if(rand == 1)
            //{
            //    StartCoroutine("Three");
            //}
            //else if (rand == 2)
            //{
            //    StartCoroutine("Daia");
            //}
            //else if (rand == 3)
            //{
            //    StartCoroutine("cross");
            //}

            //StartCoroutine("One");
            //StartCoroutine("Three");
            //StartCoroutine("Daia");
            //StartCoroutine("cross");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFire = false;
        }
    }


    IEnumerator One()
    {
        while (isFire)
        {
            isFired = true;

            bulletMng.RequestFireBullet(firePos.position, -90.0f, 5.0f);
            yield return new WaitForSeconds(bulletInterval);

            isFired = false;
        }
    }


    IEnumerator cross()
    {

        int lineCount = 4;
        float offsetAngle = 0;
        while (isFire)
        {
            isFired = true;
            for (int i = 0; i < lineCount; i++)
            {
                bulletMng.RequestFireBullet(firePos.position, ((360 / lineCount) * i) + offsetAngle, 5);
            }
            offsetAngle += 5;
            yield return new WaitForSeconds(bulletInterval);
            isFired = false;
        }

    }



    IEnumerator Three()
    {
        Transform playerTrans = GameObject.Find("Player").GetComponent<Transform>();

        float xPos = playerTrans.position.x - firePos.position.x;
        float yPos = playerTrans.position.y - firePos.position.y;

        float angle = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;

        while (isFire)
        {
            isFired = true;

            bulletMng.RequestFireBullet(firePos.position, angle, 5.0f);
            bulletMng.RequestFireBullet(firePos.position + (Vector3.left * 0.2f), angle - 10, 5.0f);
            bulletMng.RequestFireBullet(firePos.position + (Vector3.right * 0.2f), angle + 10, 5.0f);
            yield return new WaitForSeconds(bulletInterval);

            isFired = false;
        }
    }

    IEnumerator Daia()
    {
        Transform playerTrans = GameObject.Find("Player").GetComponent<Transform>();

        float xPos = playerTrans.position.x - firePos.position.x;
        float yPos = playerTrans.position.y - firePos.position.y;

        float angle = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;

        int j = 0;
        while (isFire)
        {
            isFired = true;

            for (int i = 0; i < 5; i++)
            {
                if (firearray[j, i] == 1)
                {
                    Vector3 fifififi = firePos.position;
                    fifififi.x = fifififi.x + ((i - 2) * 0.5f);
                    bulletMng.RequestFireBullet(fifififi, -90.0f, 5.0f);

                    //float ang = ((i - 2) * 10f);
                    //bulletMng.RequestFireBullet(firePos.position, -90.0f + ang, 5.0f);
                }
            }

            j++;
            if (j == 5) { j = 0; }

            yield return new WaitForSeconds(bulletInterval);

            isFired = false;
        }
    }


    int[,] firearray =
    {
        {0,0,1,0,0 },
        {0,1,0,1,0 },
        {1,0,0,0,1 },
        {0,1,0,1,0 },
        {0,0,1,0,0 }
    };

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {

    public BulletManager Bulletmng;
    public Transform ShotPos;
    AudioSource SO;
    public float bulletdelayTime;

    bool isFire;
    bool isFired;
    bool isbomb;
    public PlayerMove p;
	// Use this for initialization
	void Start () {
        isFire = false;
        isFired = false;
        isbomb = false;
        SO = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)&&!isFired&&p.isGameStart)
        {
            isFire = true;
            StartCoroutine("Shot");
        }
        if (Input.GetKeyDown(KeyCode.Z) && !isbomb && p.isGameStart)
        {
            StartCoroutine("Bomb");
            isbomb = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFire = false;
        }
	}

    IEnumerator Shot()
    {
        while(isFire)
        {
            SO.Play();

            Transform playerTrans = GameObject.Find("Player").GetComponent<Transform>();

            float xPos = playerTrans.position.x - ShotPos.position.x;
            float yPos = playerTrans.position.y - ShotPos.position.y;

            isFired = true;
            if (p.ShotNum == 1)
            {
                Bulletmng.RequestFireBullet(ShotPos.position, 90.0f, 5.0f);
                yield return new WaitForSeconds(bulletdelayTime);
            }
            if(p.ShotNum == 2)
            {
                Bulletmng.RequestFireBullet(ShotPos.position, 90.0f, 5.0f);
                Bulletmng.RequestFireBullet(ShotPos.position + (Vector3.left * 0.2f), 90.0f, 5.0f);
                yield return new WaitForSeconds(bulletdelayTime);
            }
            if(p.ShotNum == 3)
            {
                Bulletmng.RequestFireBullet(ShotPos.position, 90.0f, 5.0f);
                Bulletmng.RequestFireBullet(ShotPos.position + (Vector3.left * 0.2f), 90.0f, 5.0f);
                Bulletmng.RequestFireBullet(ShotPos.position + (Vector3.right * 0.2f), 90.0f, 5.0f);
                yield return new WaitForSeconds(bulletdelayTime);
            }
            isFired = false;
        }
    }

    IEnumerator Bomb()
    {
        Debug.Log("BOMB");
        for(int i=0;i<20;i++)
        {
             Bulletmng.RequestFireBullet(ShotPos.position + new Vector3(i*0.2f,0,0), 90.0f, 5.0f);
        }
        for (int i = 0; i < 20; i++)
        {
            Bulletmng.RequestFireBullet(ShotPos.position + new Vector3(-i * 0.2f, 0, 0), 90.0f, 5.0f);
        }
        yield return new WaitForSeconds(10);
        isbomb = false;
    }
}

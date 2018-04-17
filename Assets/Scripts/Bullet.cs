using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Transform myTransform;

    float bulletSpeed;
    float bulletAngle;
    float lifeTime;

    bool isLive;
    public bool LIVE{ get { return isLive; } }

    //new Transform transform;

    void Init () {
        //transform = GetComponent<Transform>();
        //transform --> GetComponent<Transform>()
        if (myTransform == null) myTransform = transform;
        lifeTime = 3.0f;
    }
	
	void Update () {
		
	}

    // 시작 위치
    // 속도
    // 발사 각도
    // 회전속도 <-- 실시간으로 (나중에 ^0^)
    // 총알종류 <-- 나중에
    public void FireBullet(Vector2 _startPos, float _bulletAngle, float _bulletSpeed)
    {
        Init();

        myTransform.position = _startPos;
        myTransform.rotation = Quaternion.Euler(0,0, _bulletAngle);

        bulletAngle = _bulletAngle;
        bulletSpeed = _bulletSpeed;

        isLive = true;
        gameObject.SetActive(true);
        StartCoroutine("deadTimer");
    }

    private void FixedUpdate()
    {
        if(isLive)
        {
            myTransform.Translate(Vector2.right * bulletSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator deadTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        die();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("END2"))
        {
            die();
        }
    }

    public void die()
    {
        gameObject.SetActive(false);
        isLive = false;
        myTransform.position = new Vector2(1000, 1000);

    }

   
}

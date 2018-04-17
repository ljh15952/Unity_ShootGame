using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    public GameObject bulletOrigin;
    List<Bullet> bulletList;


	// Use this for initialization
	void Start () {
        bulletList = new List<Bullet>();
    }

    public void RequestFireBullet(Vector2 _startPos, float _bulletAngle, float _bulletSpeed)
    {
        // 발사 성공했니???
        bool fireSuccess = false;

        for(int i = 0 ; i<bulletList.Count ; i++)
        {
            if(!bulletList[i].LIVE)
            {
                bulletList[i].FireBullet(_startPos, _bulletAngle, _bulletSpeed);
                fireSuccess = true; // 발사 성공
                break;
            }
        }

        // 총알 새로 만들어줌
        if(!fireSuccess)
        {
            Bullet currentBullet = Instantiate(bulletOrigin).GetComponent<Bullet>();
            bulletList.Add(currentBullet); // 리스트에 추가시켜줌
            currentBullet.FireBullet(_startPos, _bulletAngle, _bulletSpeed);
        }
    }



	
	//void Update () {
	//	if(Input.GetKeyDown(KeyCode.Space))
 //       {
 //           StartCoroutine("fifi");
 //       }
 //       if (Input.GetKeyUp(KeyCode.Space))
 //       {
 //           StopCoroutine("fifi");
 //       }
 //   }

 //   public int asdf;
 //   IEnumerator fifi()
 //   {
 //       float offsetAngle = 0;
 //       while(true)
 //       {
 //           for(int i = 0; i < asdf; i++)
 //           {
 //               RequestFireBullet(Vector2.zero, ((360 / asdf) *i) + offsetAngle, 5);
 //           }
 //           offsetAngle += 5;
 //           yield return new WaitForSeconds(0.15f);
 //       }
 //   }

}

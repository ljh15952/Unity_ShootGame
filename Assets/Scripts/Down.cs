using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.Translate(Vector2.down * 1.1f * Time.fixedDeltaTime);
    }
}

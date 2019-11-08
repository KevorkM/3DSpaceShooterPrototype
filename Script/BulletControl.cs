using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	float speed = 20f;

	void Start () {
		
	}
	
	void Update () {
		bulletLogic();
	}

	void bulletLogic(){
		this.transform.position = new Vector3 (
			this.transform.position.x,
			this.transform.position.y + speed*Time.deltaTime,
			this.transform.position.z
		);
		// Destroys the bullets when it reachs a certain point
		if (this.transform.position.y > 10){
			GameObject.Destroy(this.gameObject);
		}
	}
}

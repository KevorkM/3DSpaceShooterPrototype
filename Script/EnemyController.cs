using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

public delegate void OnKillAction();
public OnKillAction OnKill;

float speed = -5f;

	void Start () {
		
	}
	
	void Update () {
        MoveEnemyDown();
	}

	void MoveEnemyDown(){
		this.transform.position = new Vector3(
			this.transform.position.x,
			this.transform.position.y + speed*Time.deltaTime,
			this.transform.position.z
			);
			if (this.transform.position.y<-10){
				GameObject.Destroy(this.gameObject);
			}
	
	}
	public void OnTriggerEnter(Collider collider) {
		if (collider.GetComponent<BulletControl>() !=null){
			if (OnKill != null){
				OnKill();
			}
			GameObject.Destroy(this.gameObject);
			GameObject.Destroy(collider.gameObject);
		}
	}
}

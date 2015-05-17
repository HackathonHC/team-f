using UnityEngine;
using System.Collections;

public class MonsterSensor : MonoBehaviour {

	GameObject BK;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c) {
		if(c.gameObject.name == "SensorTriger"){
			if(!BK){
				BK = GameObject.Find("Bk");
			}
			BK.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	void OnTriggerExit2D(Collider2D c) {
		if(c.gameObject.name == "SensorTriger"){
			if(!BK){
				BK = GameObject.Find("Bk");
			}
			BK.GetComponent<SpriteRenderer>().color = Color.white;
		}
	} 
}

using UnityEngine;
using System.Collections;

public class MonsterSensor : MonoBehaviour {

	GameObject BK;
	Packman parent;
	bool mine;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D c) {
		if(c.gameObject.name == "SensorTriger"){
			if(Battle.instance.data.mode == Battle.Mode.Play){
				if(getMine()){
					getBK().GetComponent<SpriteRenderer>().color = Color.red;
				}
			} else {
				if(getMine()){
					getBK().GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D c) {
		if(c.gameObject.name == "SensorTriger"){
			if(getMine()){
				getBK().GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	} 

	GameObject getBK(){
		if(!BK){
			BK = GameObject.Find("Bk");
		}
		return BK;
	}

	bool getMine(){
		if(!parent){
			parent = GetComponentInParent<Packman>();
			mine = parent.photonView.isMine;
		}
		return mine;
	}
}

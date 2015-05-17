using UnityEngine;
using System.Collections;

public class MonsterSensor : MonoBehaviour {

	GameObject BK;
	Packman parent;
	bool mine;
	int cnt = 0;

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
					if(cnt > 100){
                        cnt = 0;
					} 
					if(cnt == 0){
						Handheld.Vibrate();
					}
					cnt++;
				}
			} else {
				if(getMine()){
					getBK().GetComponent<SpriteRenderer>().color = Color.white;
					cnt = 0;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D c) {
		if(c.gameObject.name == "SensorTriger"){
			if(getMine()){
				getBK().GetComponent<SpriteRenderer>().color = Color.white;
				cnt = 0;
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

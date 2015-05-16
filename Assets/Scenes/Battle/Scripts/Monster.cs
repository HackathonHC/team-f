using UnityEngine;
using System.Collections;

public class Monster : Character {

	// Use this for initialization
	void Start () {
		if (photonView.isMine) {
			GameObject.Find("Swip").GetComponent<Swip>().setTarget(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			Move();
		}
	}
}

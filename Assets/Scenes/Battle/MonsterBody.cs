using UnityEngine;
using System.Collections;

public class MonsterBody : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c) {
		print ("OnTriggerEnter2D");
		if (c.tag == Packman.Tag)
		{
			Battle.instance.Hit(c.GetComponentInParent<Packman>());
		}
	}
}

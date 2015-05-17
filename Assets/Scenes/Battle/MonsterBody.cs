using UnityEngine;
using System.Collections;

public class MonsterBody : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == Packman.Tag)
		{
			Battle.instance.Hit(c.GetComponentInParent<Packman>());
		}
	}
}

using UnityEngine;
using System.Collections;

public class CourseSensor : MonoBehaviour {

	private GameObject parent;
	private string myPosition;
	private Vector2 Position;
	private GameObject hitObject;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		Position = transform.localPosition;
		if(Position.x != 0){
			if(Position.x > 0){
				myPosition = "right";
			} else {
				myPosition = "left";
			}
		} else if(Position.y != 0){
			if(Position.y > 0){
				myPosition = "up";
			} else {
				myPosition = "down";
			}
		} else {
			myPosition = "body";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter2D(Collider2D c) {
		if(myPosition == "body"){
			Debug.Log("body");
			Vector2 parentPosition = parent.transform.position;
			Vector2 Scale = GetComponent<CircleCollider2D>().bounds.size;
			Vector2 WallPosition = c.transform.position;
			Vector2 WallScale = c.bounds.size;
			if(parent.GetComponent<Character>().moveX != 0){
				if(parent.GetComponent<Character>().moveX > 0){
					parentPosition.x = WallPosition.x-(WallScale.x/2)-(Scale.x/2)-(Scale.x/10);
				} else {
					parentPosition.x = WallPosition.x+(WallScale.x/2)+(Scale.x/2)+(Scale.x/10);
				}
				transform.position = Position;
				parent.GetComponent<Character>().moveX = 0;
			}
			if(parent.GetComponent<Character>().moveY != 0){
				if(parent.GetComponent<Character>().moveY > 0){
					parentPosition.y = WallPosition.y-(WallScale.y/2)-(Scale.y/2)-(Scale.y/10);
				} else {
					parentPosition.y = WallPosition.y+(WallScale.y/2)+(Scale.y/2)+(Scale.y/10);
				}
				parent.transform.position = parentPosition;
				parent.GetComponent<Character>().moveY = 0;
			}
		} else if(c.gameObject.name != "Body"){
			parent.GetComponent<Character>().hitCourse(myPosition,false);
		}
  }
  void OnTriggerExit2D(Collider2D c) {
		parent.GetComponent<Character>().hitCourse(myPosition,true);
  } 
}

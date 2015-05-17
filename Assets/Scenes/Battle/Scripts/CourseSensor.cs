using UnityEngine;
using System.Collections;

public class CourseSensor : MonoBehaviour {

	private GameObject parent;
	private string myPosition;
	private Vector2 Position;
	private GameObject hitObject;
	private Dammy dammy;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		dammy = parent.GetComponent<Dammy>();
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
		switch (c.tag)
		{
		case "Course":
			if(myPosition == "body"){
				float moveX = dammy.moveX;
				float moveY = dammy.moveY;
				if(moveX != 0){
					if(moveX > 0){
						if(!dammy.CanRight){
							dammy.moveX = 0;
						}
					} else {
						if(!dammy.CanLeft){
							dammy.moveX = 0;
						}
					}
				} else if(moveY != 0){
					if(moveY > 0){
						if(!dammy.CanUp){
							dammy.moveY = 0;
						}
					} else {
						if(!dammy.CanDown){
							dammy.moveY = 0;
						}
					}
				}
				/*
				if(c.gameObject.name != "UpSensor" && c.gameObject.name != "DownSensor" && c.gameObject.name != "LeftSensor" && c.gameObject.name != "RightSensor" ){
					Debug.Log("body");
					Vector2 parentPosition = parent.transform.position;
					Vector2 Scale = GetComponent<CircleCollider2D>().bounds.size;
					Vector2 WallPosition = c.transform.position;
					Vector2 WallScale = c.bounds.size;
					if(parent.GetComponent<Character>().moveX != 0){
						if(parent.GetComponent<Character>().moveX > 0){
							parentPosition.x = WallPosition.x-(WallScale.x/2)-(Scale.x/2)-(Scale.x/100);
						} else {
							parentPosition.x = WallPosition.x+(WallScale.x/2)+(Scale.x/2)+(Scale.x/100);
						}
						parent.transform.position = parentPosition;
						parent.GetComponent<Character>().moveX = 0;
					}
					if(parent.GetComponent<Character>().moveY != 0){
						if(parent.GetComponent<Character>().moveY > 0){
							parentPosition.y = WallPosition.y-(WallScale.y/2)-(Scale.y/2)-(Scale.y/100);
						} else {
							parentPosition.y = WallPosition.y+(WallScale.y/2)+(Scale.y/2)+(Scale.y/100);
						}
						parent.transform.position = parentPosition;
						parent.GetComponent<Character>().moveY = 0;
					}
				}
				*/
			} else if(c.gameObject.name != "Body"){
				dammy.hitCourse(myPosition,false);
			}
			break;
		default:
			if (dammy.tag == Monster.Tag && c.tag == Packman.Tag)
			{
				Battle.instance.Hit(c.GetComponentInParent<Packman>());
			}
			break;
		}
  }
  void OnTriggerExit2D(Collider2D c) {
		switch (c.tag)
		{
		case "Course":
			dammy.hitCourse(myPosition,true);
			break;
		}
  } 
}

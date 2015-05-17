using UnityEngine;
using System.Collections;

public class Dammy : MonoBehaviour {

	protected GameObject target;

	protected bool canUp = true;
	protected bool canDown = true;
	protected bool canLeft = true;
	protected bool canRight = true;
	protected float move_x = 0f;
	protected float move_y = 0f;
	
	public float moveX
	{
		set { this.move_x = value; }
		get { return this.move_x; }
	}
	public float moveY
	{
		set { this.move_y = value; }
		get { return this.move_y; }
	}
	public bool CanUp
	{
		get { return this.canUp; }
	}
	public bool CanDown
	{
		get { return this.canDown; }
	}
	public bool CanLeft
	{
		get { return this.canLeft; }
	}
	public bool CanRight
	{
		get { return this.canRight; }
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		if(target){
			target.transform.position = transform.position;
		}
	}

	public void setTarget(GameObject go) {
		transform.position = go.transform.position;
		target = go;
	}
	
	void Move() {
		Vector2 Position = transform.position;
		Position.x += move_x*Time.deltaTime;
		Position.y += move_y*Time.deltaTime;
		transform.position = Position;
	}
	
	void OnUp() {
			if(canUp){
				move_x = 0;
				move_y = 1.0f;
			}
	}
	
	void OnDown() {
			if(canDown){
				move_x = 0;
			move_y = -1.0f;
			}
	}
	
	void OnLeft() {
			if(canLeft){
			move_x = -1.0f;
				move_y = 0;
			}
	}
	
	void OnRight() {
			if(canRight){
			move_x = 1.0f;
				move_y = 0;
			}
	}
	
	public void hitCourse(string pos, bool hit){
			switch(pos){
			case "up":
				canUp = hit;
				break;
			case "down":
				canDown = hit;
				break;
			case "left":
				canLeft = hit;
				break;
			case "right":
				canRight = hit;
				break;
			}
	}
}

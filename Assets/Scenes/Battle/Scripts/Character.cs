using UnityEngine;
using System.Collections;

public class Character : Photon.MonoBehaviour {

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

	}

	protected void Move() {
		Vector2 Position = transform.position;
		Position.x += move_x;
		Position.y += move_y;
		transform.position = Position;
	}
	
	void OnUp() {
		if (photonView.isMine) {
			if(canUp){
				move_x = 0;
				move_y = 0.1f;
			}
		}
	}
	
	void OnDown() {
		if (photonView.isMine) {
			if(canDown){
				move_x = 0;
				move_y = -0.1f;
			}
		}
	}
	
	void OnLeft() {
		if (photonView.isMine) {
			if(canLeft){
				move_x = -0.1f;
				move_y = 0;
			}
		}
	}
	
	void OnRight() {
		if (photonView.isMine) {
			if(canRight){
				move_x = 0.1f;
				move_y = 0;
			}
		}
	}

	public void hitCourse(string pos, bool hit){
		if (photonView.isMine) {
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
}

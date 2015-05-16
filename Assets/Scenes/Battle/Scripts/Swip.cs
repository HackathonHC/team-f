using UnityEngine;
using System.Collections;

public class Swip : MonoBehaviour {

	public float validityTouchTime = 0.5f;
	public float validityTouchDistance = 20.0f;
	
	public float validityFlickTime = 0.5f;
	public float validityFlickMinDistance = 30.0f;
	public float validityFlickMaxDistance = 300.0f;
	public int validityFlickDegRange = 20;
	
	//public bool enabledOnTouch = true;
	//public FlickCallBackRule[] rules;
	
	private float touchTime = 0;
	private bool isTouch = false;
	
	private Vector3 touchPosition;
	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(target){
			//if (Input.GetMouseButtonDown(0)) Down();
			//if (Input.GetMouseButtonUp(0)) Up();
			if(Input.GetKeyDown (KeyCode.Mouse0)) Down();
			if(Input.GetKeyUp (KeyCode.Mouse0)) Up();
		}
	}

	public void setTarget(GameObject go) {
		Debug.Log(go);
		target = go;
	}

	void Down() {
		touchPosition = Input.mousePosition;
		
		touchTime = Time.time;
		isTouch = true;
	}
	
	void Up() {
		if (!isTouch) return;
		
		Vector3 touchEndPosition = Input.mousePosition;
		
		float deltaTime = Time.time - touchTime;
		float distance = Vector3.Distance(touchPosition, touchEndPosition);
		int deg = getDeg(touchPosition, touchEndPosition);
		
		//if (enabledOnTouch && ValidateTouch(deltaTime, distance)) {
		//	SendMessage("OnTouch");
		//}
		
		if (ValidateFlick(deltaTime, distance)) {
			//foreach (FlickCallBackRule rule in rules) {
			//	if (ValidateFlickDeg(rule.deg, deg)) {
			//		SendMessage(rule.callbackName);
			//		break;
			//	}
			//}
			if (ValidateFlickDeg(0, deg)) {
				Debug.Log("OnUp");
				target.SendMessage("OnUp");
			}
			if (ValidateFlickDeg(90, deg)) {
				Debug.Log("OnRight");
				target.SendMessage("OnRight");
			}
			if (ValidateFlickDeg(180, deg)) {
				Debug.Log("OnDown");
				target.SendMessage("OnDown");
			}
			if (ValidateFlickDeg(270, deg)) {
				Debug.Log("OnLeft");
				target.SendMessage("OnLeft");
			}
		}
		
		isTouch = false;
	}
	
	//bool ValidateTouch(float deltaTime, float distance) {
	//	if (validityTouchTime < deltaTime) return false;
	//	if (validityTouchDistance < distance) return false;
	//	return true;
	//}
	
	bool ValidateFlick(float deltaTime, float distance) {
		if (validityFlickTime < deltaTime) return false;
		
		return (validityFlickMinDistance < distance && distance < validityFlickMaxDistance);
	}
	
	bool ValidateFlickDeg(int ruleDeg, int deg) {
		int min = ruleDeg - validityFlickDegRange;
		int max = ruleDeg + validityFlickDegRange;
		
		if (min < deg && deg < max) return true;
		
		// 0度付近を考慮し360度分足してから再チェック
		min += 360;
		max += 360;
		
		return min < deg && deg < max;
	}
	
	int getDeg(Vector3 a, Vector3 b) {
		return Mathf.RoundToInt(180 + (Mathf.Atan2(a.x - b.x, a.y - b.y) * Mathf.Rad2Deg));
	}
}

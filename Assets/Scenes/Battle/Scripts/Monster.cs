using UnityEngine;
using System.Collections;

public class Monster : Character {

	public static Monster instance { get; private set; }

	private SpriteRenderer _renderer;

	public const string Tag = "Monster";

	void Awake()
	{
		instance = this;
		_renderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		if (photonView.isMine) {
			GameObject.Find("Swip").GetComponent<Swip>().setTarget(this.gameObject);
		}
		else {
			Hide();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			Move();
		}
	}

	void Show()
	{
		_renderer.enabled = true;
	}

	void Hide()
	{
		_renderer.enabled = false;
    }
}

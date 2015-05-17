using UnityEngine;
using System.Collections;

public class Monster : Character {

	public static Monster instance { get; private set; }

	private CharacterAnimator _animator;


	public const string Tag = "Monster";

	void Awake()
	{
		instance = this;
		_animator = GetComponentInChildren<CharacterAnimator>();
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

	}

	public void Show()
	{
		print ("Show monster");
		_animator.isWalkAnimation = true;
		_animator.Play("Front");
	}

	public void Hide()
	{
		print ("Hide monster");
		_animator.isWalkAnimation = false;
		_animator.Play("Hide");
	}
}

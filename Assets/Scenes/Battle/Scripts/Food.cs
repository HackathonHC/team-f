using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	private float _hiddenTime = 0;
	private const float IntervalWhenAte = 10;
	private SpriteRenderer _renderer;
	private Collider2D _collider;
	public const string Tag = "Food";

	void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_collider = GetComponent<Collider2D>();
	}

	void Start () {

	}
	
	void Update () {
		if (_hiddenTime > 0 && Time.time - _hiddenTime > IntervalWhenAte)
		{
			Show();
		}
	}

	void Show()
	{
		_hiddenTime = 0;
		_renderer.enabled = true;
		_collider.enabled = true;
	}

	public void Hide()
	{
		_hiddenTime = Time.time;
		_renderer.enabled = false;
		_collider.enabled = false;
	}
}

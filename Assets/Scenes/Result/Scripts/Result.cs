using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
	[SerializeField] Sprite[] bks;
	[SerializeField] SpriteRenderer _renderer;
	[SerializeField] GameObject _exitButton; 

	public static Result instance;

	void Awake()
	{
		instance = this;
		_renderer.enabled = false;
	}

	public void Show()
	{
		_renderer.enabled = true;
	}

	public void OnClickExitButton()
	{
	}
}

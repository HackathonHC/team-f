using UnityEngine;
using System.Collections;

public class HeaderIcon : MonoBehaviour {

	[SerializeField] Sprite[] icons;
	[SerializeField] SpriteRenderer _rendere;

	public void Show(int id)
	{
		_rendere.sprite = icons[id];
	}
}

using UnityEngine;
using System.Collections;

public class Packman : Character {
	private SpriteRenderer _renderer;

	private int _id;

	void Awake()
	{
		_id = PhotonNetwork.playerList.Length - 1;
		_renderer = GetComponent<SpriteRenderer>();
		_renderer.sprite = Resources.Load<Sprite>(GetSpriteName());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	string GetSpriteName()
	{
		return string.Format("Packman{0}", _id);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		print ("Food");
		switch (c.tag)
		{
		case Food.Tag:
			c.GetComponent<Food>().Hide();
			if (photonView.isMine)
			{
				// TODO: ADD score etc...
			}
			break;
		case PowerFood.Tag:
			c.GetComponent<PowerFood>().Hide();
			if (photonView.isMine)
			{
				// TODO: ADD score etc...
			}
			break;
		}

	}
}

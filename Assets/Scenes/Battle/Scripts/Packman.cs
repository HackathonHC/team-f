using UnityEngine;
using System.Collections;

public class Packman : Character {
	private SpriteRenderer _renderer;

	private int _id;
	public float powerTime = 0;
	public float IntervalWhenAte = 10;

	void Awake()
	{
		photonView.observed = this;
		_id = PhotonNetwork.playerList.Length - 1;
		_renderer = GetComponent<SpriteRenderer>();
		_renderer.sprite = Resources.Load<Sprite>(GetSpriteName());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine)
		{
			if (powerTime > 0 && Time.time - powerTime > IntervalWhenAte)
			{
				Weaken();
			}
		}

		if (powerTime == 0)
		{
			transform.localScale = Vector3.one;
		}
		else
		{
			transform.localScale = 1.5f * Vector3.one;
		}
	}

	void Strengthen()
	{
		powerTime = Time.time;
	}

	void Weaken()
	{
		powerTime = 0;
	}

	string GetSpriteName()
	{
		return string.Format("Packman{0}", _id);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
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
				Strengthen();
			}
			break;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		print ("OnPhotonSerializeView");
		if (stream.isWriting) {
			//データの送信
			stream.SendNext(powerTime);
		} else {
			//データの受信
			powerTime = (float)stream.ReceiveNext();
		}
	}
}

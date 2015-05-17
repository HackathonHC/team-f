using UnityEngine;
using System.Collections;

public class Packman : Character {
	public int id;
	public float powerTime = 0;
	public float IntervalWhenAte = 10;
	public const string Tag = "Packman";

	private CharacterAnimator _animator;

	void Awake()
	{
		photonView.observed = this;
		id = PhotonNetwork.playerList.Length - 1;
		GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>(GetPrefabName()));
		obj.transform.SetParent(transform, false);
		_animator = GetComponent<CharacterAnimator>();
	}

	// Use this for initialization
	void Start () {
		if (photonView.isMine) {
			GameObject.Find("Swip").GetComponent<Swip>().setTarget(this.gameObject);
		}
	}

	public bool isPower { get { return powerTime > 0 && Time.time - powerTime < IntervalWhenAte; } } 
	
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
			// TODO:
			// transform.localScale = Vector3.one;
		}
		else
		{
			// transform.localScale = 1.5f * Vector3.one;
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
		return string.Format("Packman{0}", id);
	}

	string GetPrefabName()
	{
		return string.Format("Packman{0}", 1);
		//return string.Format("Packman{0}", id);
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
				Battle.instance.EatFood(id);
			}
			break;
		case PowerFood.Tag:
			c.GetComponent<PowerFood>().Hide();
			if (photonView.isMine)
			{
				Strengthen();
			}
			break;
		case LightItem.Tag:
			Battle.instance.TakeLight();
			break;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
//		print ("OnPhotonSerializeView");
		if (stream.isWriting) {
			//データの送信
			stream.SendNext(powerTime);
		} else {
			//データの受信
			powerTime = (float)stream.ReceiveNext();
		}
	}
}

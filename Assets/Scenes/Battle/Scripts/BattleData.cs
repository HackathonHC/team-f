using UnityEngine;
using System.Collections;

public class BattleData : Photon.MonoBehaviour {

	public enum State
	{
		Play,
	}

	private Battle battle;

	void Awake()
	{
		photonView.observed = this;
		battle = Battle.instance;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			//データの送信
//			stream.SendNext(battle.data.mode);
		} else {
			//データの受信
//			battle.data.mode = (Battle.Mode)stream.ReceiveNext();
		}
	}

	public void SendState(State state)
	{
		photonView.RPC("ReceiveState", PhotonTargets.Others, (int)state);
	}
	
	[RPC] 
	void ReceiveState(int value)
	{
		print ("ReceiveState");
		State state = (State)value;
		switch (state)
		{
		case State.Play:
			battle.Play();
            break;
        }
    }
}

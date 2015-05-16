using UnityEngine;
using System.Collections;

public class BattleData : Photon.MonoBehaviour {

	public enum State
	{
		Play,
		EatPowerFood,
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

	public void SendState(State state, int id=-1)
	{
		PhotonTargets targets;

		switch (state)
		{
		case State.EatPowerFood:
			targets = PhotonTargets.MasterClient;
			break;
		default:
			targets = PhotonTargets.Others;
			break;
		}

		photonView.RPC("ReceiveState", targets, (int)state, id);
	}
	
	[RPC] 
	void ReceiveState(int nState, int id)
	{
		print ("ReceiveState");
		State state = (State)nState;
		switch (state)
		{
		case State.Play:
			battle.Play();
            break;
		case State.EatPowerFood:
			battle.Weaken(id);
			break;
        }
    }
}

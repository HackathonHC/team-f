using UnityEngine;
using System.Collections;

public class BattleData : Photon.MonoBehaviour {

	public enum State
	{
		Play,
		EatPowerFood,
		Grabbed,
		EatFood,
		Show,
		Hide,
	}

	public Battle battle { get; set; }
	private Battle.Data _data = new Battle.Data();

	void Awake()
	{
		photonView.observed = this;
		battle = Battle.instance;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Battle.Data data;

		if (photonView.isMine)
		{
			data = battle.data;
		}
		else
		{
			data = _data;
		}

		if (stream.isWriting) {
			//データの送信
			stream.SendNext(_data.mode);
		} else {
			//データの受信
			_data.mode = (Battle.Mode)stream.ReceiveNext();
		}

	}

	public void SendState(State state, int id=-1)
	{
		PhotonTargets targets;

		switch (state)
		{
		case State.EatPowerFood:
		case State.EatFood:
			targets = PhotonTargets.MasterClient;
			break;
		case State.Show:
			targets = PhotonTargets.All;
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
		print ("ReceiveState:" + nState + ":" + id);
		State state = (State)nState;
		switch (state)
		{
		case State.Play:
			battle.Play();
            break;
		case State.EatPowerFood:
			battle.Weaken(id);
			break;	
		case State.EatFood:
			battle.AddFoodCount();
			break;
		case State.Grabbed:
			battle.Grab(id);
			break;
		case State.Show:
			battle.Show();
			break;
		case State.Hide:
			battle.Hide();
			break;
        }
    }
}

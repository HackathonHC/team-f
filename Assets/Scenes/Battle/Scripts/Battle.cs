using UnityEngine;
using System.Collections;


public class Battle : MonoBehaviour {

	public enum PlayerType
	{
		Monster,
		Packman,
	}

	public enum Mode
	{
		Standby,	// おまちください・・・
		Play,
		Dead,		// 食べられた
	}

	public class Data
	{
		public PlayerType playerType;
		public Mode mode;

		public string GetResourceName()
		{
			return playerType.ToString();
		}
	}

	public Data data { get; set; }

	void Awake() 
	{
		data = new Data();
		data.mode = Mode.Standby;
	}

	void Start () 
	{
		JoinLobby();
	}

	void JoinLobby()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
		PhotonNetwork.sendRate = 60;
		PhotonNetwork.sendRateOnSerialize = 60;
	}

	public void OnClickStartButton()
	{
	}

	void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 5 };
		PhotonNetwork.JoinOrCreateRoom("team-f", roomOptions, TypedLobby.Default);
	}
	
	void OnJoinedRoom()
	{
		if (PhotonNetwork.isMasterClient)
		{
			data.playerType = PlayerType.Monster;
		}
		else
		{
			data.playerType = PlayerType.Packman;
		}

		PhotonNetwork.Instantiate(data.GetResourceName(), new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Quaternion.identity, 0);
	}


}

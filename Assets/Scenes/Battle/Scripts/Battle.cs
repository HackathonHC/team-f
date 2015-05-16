using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
		Grabbed,		// 食べられた
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

	public static Battle instance { get; private set; }

	public BattleData battleData { get; private set; }

	public Character character { get; private set; }

	private GameObject _startButton;
	private GameObject _waitingLabel;

	private HashSet<int> _powerPackmans = new HashSet<int>();

	void Awake() 
	{
		instance = this;
		data = new Data();
		data.mode = Mode.Standby;
		Transform canvas = transform.FindChild("Canvas");
		_waitingLabel = canvas.FindChild("WaitingLabel").gameObject;
		_startButton = canvas.FindChild("StartButton").gameObject;
		_startButton.SetActive(false);
	}

	void Start () 
	{
		JoinLobby();
	}

	void JoinLobby()
	{
		PhotonNetwork.ConnectUsingSettings("3.0");
		PhotonNetwork.sendRate = 60;
		PhotonNetwork.sendRateOnSerialize = 60;
	}

	public void OnClickStartButton()
	{
		Play();
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
			_startButton.SetActive(true);
		}
		else
		{
			data.playerType = PlayerType.Packman;
		}

		battleData = PhotonNetwork.Instantiate("BattleData", Vector3.zero, Quaternion.identity, 0).GetComponent<BattleData>();
		GameObject g = PhotonNetwork.Instantiate(data.GetResourceName(), new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Quaternion.identity, 0);
		character = g.GetComponent<Character>();
	}

	const float Speed = 1.25f;
	void Update()
	{
		if (!canMove)
		{
			return;
		}

		if (Input.GetKey(KeyCode.RightArrow))
	    {
			character.transform.position += Speed * Vector3.right * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			character.transform.position -= Speed * Vector3.right * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			character.transform.position += Speed * Vector3.up * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			character.transform.position -= Speed * Vector3.up * Time.deltaTime;
		}
	}

	public void Play()
	{
		if (PhotonNetwork.isMasterClient)
		{
			battleData.SendState(BattleData.State.Play);
		}
		_startButton.SetActive(false);
		_waitingLabel.SetActive(false);
		data.mode = Mode.Play;
    }

	public void Weaken(int id)
	{
		_powerPackmans.Add(id);
	}

	public void Hit(Packman packman)
	{
		if (PhotonNetwork.isMasterClient)
		{
			// TODO:  ちゃんとしたやられた処理
			if (packman.isPower)
			{
				PhotonView.Destroy(character.gameObject);
	        }
			else
			{
				battleData.SendState(BattleData.State.Grabbed, packman.id);
			}
		}
	}

	public void Grab(int id)
	{
		Packman packman = character as Packman;
		if (packman.id == id)
		{
			packman.transform.localPosition = Vector3.zero;
			data.mode = Mode.Grabbed;
		}
	}

	public bool canMove { get { return data.mode == Mode.Play; } }
}

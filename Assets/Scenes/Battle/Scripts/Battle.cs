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

	public const int MaxFood = 2;
    
    public class Data
	{
		public PlayerType playerType;
		public Mode mode;
		public string GetResourceName()
		{
			return playerType.ToString();
		}
		public int id;
	}

	public Data data { get; set; }

	public static Battle instance { get; private set; }

	public BattleData battleData { get; private set; }

	public Character character { get; private set; }

	private LightItem _lightItem;

	private GameObject _startButton;
	private GameObject _waitingLabel;

	public const float ShowInterval = 10f;
	private float _showTime = 0;
	private int _foodCount = 0;

	private HashSet<int> _powerPackmans = new HashSet<int>();

	private HashSet<Packman> _grabbedPackmans = new HashSet<Packman>();

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
		PhotonNetwork.ConnectUsingSettings("5.0");
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

	readonly static Vector3[] MonsterPositions = new Vector3[] { new Vector3(3.4f, -3.9f, 0), new Vector3(-3.4f, -3.9f, 0), new Vector3(-3.4f, 3.6f, 0), new Vector3(3.4f, 3.6f, 0) };
	readonly static Vector3 PackmanPosition = new Vector3(0, 0.95f, 0);
	readonly static Vector3 LightPosition = new Vector3(0, -0.85f, 0);
	//readonly static Vector3 LightPosition = new Vector3(3.4f, -3.9f, 0);
	void OnJoinedRoom()
	{
		Vector3 startPosition;

		if (PhotonNetwork.isMasterClient)
		{
			data.playerType = PlayerType.Monster;
			data.id = -1;
			_startButton.SetActive(true);
			startPosition = MonsterPositions[Random.Range(0, MonsterPositions.Length)];
			battleData = PhotonNetwork.Instantiate("BattleData", Vector3.zero, Quaternion.identity, 0).GetComponent<BattleData>();
			GameObject g = PhotonNetwork.Instantiate(data.GetResourceName(), startPosition, Quaternion.identity, 0);
			character = g.GetComponent<Character>();
		}
		else
		{
			data.playerType = PlayerType.Packman;
			data.id = PhotonNetwork.playerList.Length - 1;
			startPosition = PackmanPosition;
			battleData = PhotonNetwork.Instantiate("BattleData", Vector3.zero, Quaternion.identity, 0).GetComponent<BattleData>();
			GameObject g = PhotonNetwork.Instantiate(data.GetResourceName(), startPosition, Quaternion.identity, 0);
			Packman packman = g.GetComponent<Packman>();
			packman.id = data.id;
			character = (Character)packman;
		}
	}

	const float Speed = 1.25f;
	void Update()
	{
		if (!canMove)
		{
			return;
		}

		if (_showTime > 0 && Time.time - _showTime > ShowInterval  && PhotonNetwork.isMasterClient)
		{
			Hide();
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
				if (!_grabbedPackmans.Contains(packman))
				{
					_grabbedPackmans.Add(packman);
					battleData.SendState(BattleData.State.Grabbed, packman.id);
				}
			}
		}
	}

	public void Grab(int id)
	{
		if (PhotonNetwork.isMasterClient)
		{
			return;
		}

		Packman packman = character as Packman;
		if (packman.id == id)
		{
			packman.transform.localPosition = Vector3.zero;
			data.mode = Mode.Grabbed;
		}
	}

	public void EatFood(int id)
	{
		battleData.SendState(BattleData.State.EatFood, id);
	}

	public void TakeLight()
	{
		battleData.SendState(BattleData.State.Show);
	}

	public void AddFoodCount()
	{
		if (PhotonNetwork.isMasterClient)
		{
			if (_showTime > 0)
			{
				//nop
			}
			else if (_foodCount > MaxFood && _lightItem == null)
			{
				_lightItem = PhotonNetwork.Instantiate("Light", LightPosition, Quaternion.identity, 0).GetComponent<LightItem>();
				_foodCount = 0;
			}
			else
			{
				_foodCount++;
			}
		}
	}

	public void Show()
	{
		if (PhotonNetwork.isMasterClient)
		{
			_showTime = Time.time;
			PhotonNetwork.Destroy(_lightItem.gameObject);
			_lightItem = null;
		}
		else
		{
			Monster.instance.Show();
		}
	}

	public void Hide()
	{
		if (PhotonNetwork.isMasterClient)
		{
			_showTime = 0;
			battleData.SendState(BattleData.State.Hide);
		}
		else
		{
			Monster.instance.Hide();
		}
	}
    
    public bool canMove { get { return data.mode == Mode.Play; } }
}

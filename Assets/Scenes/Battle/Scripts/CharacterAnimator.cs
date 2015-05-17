using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {
	private Animator _animator;

	public bool isWalkAnimation = true;
	
	protected Direction dir;
	
	private Vector3 _prevPosition;

	public enum Direction
	{
		Wait,
		Front,
		Back,
		Right,
		Left,
	}

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_prevPosition = transform.position;
	}

	public void Play(string animationName)
	{
		_animator.Play(animationName);
	}

	void Start()
	{
		_prevPosition = transform.position;
	}

	void Update() {
		if (_animator != null && isWalkAnimation)
		{
			Direction nextDir = dir;
			if (transform.position.x > _prevPosition.x)
			{
				nextDir = Direction.Right;
			}
			else if (transform.position.x < _prevPosition.x)
			{
				nextDir = Direction.Left;
			}
			else if (transform.position.y > _prevPosition.y)
			{
				nextDir = Direction.Back;
			}
			else if (transform.position.y < _prevPosition.y)
			{
				nextDir = Direction.Front;
			}
			

			if (nextDir != dir && nextDir != Direction.Wait)
			{
				_animator.Play(nextDir.ToString());
				nextDir = dir;
			}
		}
		_prevPosition = transform.position;
	}

}

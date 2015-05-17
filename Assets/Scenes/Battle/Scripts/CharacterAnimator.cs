using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {
	private Animator _animator;

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Play(string animationName)
	{
		_animator.Play(animationName);
	}
}

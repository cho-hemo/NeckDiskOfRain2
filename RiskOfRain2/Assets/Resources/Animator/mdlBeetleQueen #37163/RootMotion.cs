using UnityEngine;

public class RootMotion : MonoBehaviour
{
    [SerializeField]
    private GameObject _rootBone;

    private Animator _animator;
    private float _rootBoneScale;

	private void Awake()
	{
        _animator = GetComponent<Animator>();
        _rootBoneScale = _rootBone.transform.localScale.x;
	}

    private void OnAnimatorMove()
    {
        transform.position += _animator.deltaPosition * _rootBoneScale;
    }
}
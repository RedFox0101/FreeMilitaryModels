using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityInfo _entityInfo;
    [SerializeField] private Animator _animator;

    protected Vector3 _direction = Vector3.zero;
    protected Vector3 _rotateDirection = Vector3.forward;
    public Vector3 GlobalDirection => _entityInfo.PlayerTransform.forward * _direction.z + _entityInfo.PlayerTransform.right * _direction.x;

    public Animator Animator => _animator;

    public abstract void Move();
    public abstract void Rotate();

    public virtual void UseGravity()
    {

    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    public void SetRotateDirection(Vector3 direction)
    {
        _rotateDirection = direction;
    }
}

[Serializable]
public struct EntityInfo
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateTime;

    public CharacterController CharacterController => _characterController;
    public Transform PlayerTransform => _characterController.transform;
    public float Speed => _speed;
    public float RotateSpeed => _rotateTime;
}
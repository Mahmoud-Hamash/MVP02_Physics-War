using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _chargeSpeed;
    [SerializeField] private float _distanceToMove = 50f;
    private Animator _animator;
    private Vector3 _targetPosition;
    bool _isCharging = false;
    bool _isHovering = false;
 
    private void Start()
    {
        _animator = GetComponent<Animator>();

        transform.forward = transform.right;
        _targetPosition = transform.position + transform.forward * _distanceToMove;

        GameEventManager.StartListening(GameEventType.PalisadeWrecked, OnPalisadeWrecked);
    }

    private void Update()
    {
        if(_isHovering) return;

        float distance = Vector3.Distance(transform.position, _targetPosition);
        if (distance < 0.1f)
        {
            if(_isCharging)
            {
                _isHovering = true;
                _animator.SetBool("Hover", true);
                _animator.SetBool("FlyingFWD", false);
            }
            else
            {
                transform.forward = -transform.forward;
                _targetPosition = transform.position + transform.forward * _distanceToMove;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }

    private void OnPalisadeWrecked(System.Object obj)
    {
        _isCharging = true;
        _speed = _chargeSpeed;
        _targetPosition = new Vector3(CameraLoader.xrCamera.transform.position.x, transform.position.y, CameraLoader.xrCamera.transform.position.z);

        transform.LookAt(_targetPosition);
    }

    public void SetSpeeds(float speed, float chargeSpeed)
    {
        _speed = speed;
        _chargeSpeed = chargeSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    [SerializeField] private Transform _counterWeight;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private TTSSpeaker _ttsSpeaker;
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private string _introText;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _animator.SetInteger("status", 1);
    }

    private void Speak()
    {
        _ttsSpeaker.Speak(_introText);
    }

    private void Update()
    {
        // Debug.DrawRay(transform.position, (new Vector3(CameraLoader.xrCamera.transform.position.x, transform.position.y, CameraLoader.xrCamera.transform.position.z) - transform.position) * 10, Color.red);
        Quaternion targetRotation1 = Quaternion.LookRotation(new Vector3(CameraLoader.xrCamera.transform.position.x, transform.position.y, CameraLoader.xrCamera.transform.position.z) - transform.position);
        transform.rotation = targetRotation1;
            
        // draw ray
        if(_animator.GetInteger("status") != 1 ) return;

        // only rotate on y axis when looking at target
        if (Vector3.Distance(transform.position, _targetPosition.position) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(_targetPosition.position.x, transform.position.y, _targetPosition.position.z) - transform.position);
            transform.rotation = targetRotation;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition.position, _walkSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = _targetPosition.position;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(CameraLoader.xrCamera.transform.position.x, transform.position.y, CameraLoader.xrCamera.transform.position.z) - transform.position);
            transform.rotation = targetRotation;
            _animator.SetInteger("status", 0);
            Speak();
        }
    }
}
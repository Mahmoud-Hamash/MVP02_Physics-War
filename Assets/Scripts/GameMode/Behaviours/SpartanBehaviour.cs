using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpartanBehaviour : MonoBehaviour
{
    enum Status
    {
        Walk = 0,
        Charge = 1,
        Die = 2,
        Attack = 3
    }

    [SerializeField] private AudioClip _choppingSound;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _avatar;
    [SerializeField] private float _speed = 1f;
 
    private AudioSource _audioSource;
    private PalisadeBehaviour _palisade;
    private float _timeBetweenAttacks = 1f;
    private int _column = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {   
        Status status = (Status)_animator.GetInteger("status");
        switch(status)
        {
            case Status.Walk:
            case Status.Charge:
                UpdateCharge();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }
    }

    private void UpdateCharge()
    {
        _avatar.position += _avatar.forward * _speed * Time.deltaTime;
    }

    private void UpdateAttack()
    {
        if(_palisade)
        {
            if(!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_choppingSound);
            }

            _timeBetweenAttacks -= Time.deltaTime;
            if(_timeBetweenAttacks > 0) return;
            _palisade.Damage();
            _timeBetweenAttacks = 0.5f;
        }
    }

    private void UpdateDie()
    {
    }

    private void Die()
    {
        GameEventManager.TriggerEvent(GameEventType.SpartanDied, _column);
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            _animator.SetInteger("status", (int)Status.Die);
            Invoke(nameof(Die), 5f);
        }
        else if (collision.gameObject.CompareTag("Palisade"))
        {
            _animator.SetInteger("status", (int)Status.Attack);
            _palisade = collision.gameObject.GetComponent<PalisadeBehaviour>();
        }
    }

    public void SetColumn(int column)
    {
        _column = column;
    }
}

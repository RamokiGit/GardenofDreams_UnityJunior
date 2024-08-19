using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;



    public class EnemyControler : MonoBehaviour
    {
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int HandAttackTrigger = Animator.StringToHash("HandAtack");
        private static readonly int FootAttackTrigger = Animator.StringToHash("FootAtack");
        private static readonly int DeathTrigger = Animator.StringToHash("Death");
        [SerializeField] float _moveSpeed = 5f;
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] Animator _animator;
        [SerializeField] float _attackDistance = 1f; 
        [SerializeField] float _chasePlayerAfterAttack = 0.2f;
    
        private float _currentAttackTime;
        [SerializeField] private float _defaultAttackTime = 2f;
        private Transform _player;
        private bool _followPlayer;
        private bool _attackPlayer;
        private Health _health;
        private bool _isDead =false;
        public SpawnManager spawn;
        private void Awake()
        {
            _health = GetComponent<Health>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            _health.OnDeath += Death;
        }


        private void OnDisable()
        {
            _health.OnDeath -= Death;
        }
        private void Death()
        {
            _isDead = true;
            _health.OnDeath -= Death;
            _animator.SetTrigger(DeathTrigger);
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(1f);
            spawn.Return(gameObject);
        }
        
        void Start()
        {
            _followPlayer = true;
            _currentAttackTime = _defaultAttackTime;
        }
        
        void Update()
        {
            Attack();
            
        }

        private void FixedUpdate()
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            if (_isDead) return;
            if (!_followPlayer) return;
            if (Vector3.Distance(transform.position, _player.position) > _attackDistance)
            {
                transform.LookAt(_player);
                _rigidbody.velocity = transform.forward * _moveSpeed;
                if (_rigidbody.velocity.sqrMagnitude != 0)
                {
                    _animator.SetBool(Movement, true);
                }
            }
            else if (Vector3.Distance(transform.position, _player.position) <= _attackDistance)
            {
                _rigidbody.velocity = Vector3.zero;
                _animator.SetBool(Movement,false);
                _followPlayer = false;
                _attackPlayer = true;
            }
           
        }

        void Attack()
        {
            if (_isDead) return;
            if (!_attackPlayer) return;
            _currentAttackTime+=Time.deltaTime;
            if (_currentAttackTime>_defaultAttackTime)
            {
                Attack(Random.Range(0,2));
                _currentAttackTime = 0f;
            }

            if (Vector3.Distance(transform.position, _player.position) > _attackDistance + _chasePlayerAfterAttack)
            {
                _attackPlayer = false;
                _followPlayer = true;
            }
            
        }


        private void Attack(int index)
        {
            switch (index)
            {
                case 0:
                    _animator.SetTrigger(HandAttackTrigger);
                    break;
                case 1:
                    _animator.SetTrigger(FootAttackTrigger);
                    break;
            }
        }

        public void ResetState()
        {
            _isDead = false;
            _health.ResetHealth();
        }
    }


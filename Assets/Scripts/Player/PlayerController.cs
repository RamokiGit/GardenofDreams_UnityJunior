using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int AttackTrigger = Animator.StringToHash("HandAtack");
        private static readonly int FootAttackTrigger = Animator.StringToHash("FootAtack");
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private Animator _animator;
        private PlayerInput _playerInput;
        private Health _health;
        private CharacterController _characterController;
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _health = GetComponent<Health>();
            _animator = GetComponentInChildren<Animator>();
            _playerInput = new PlayerInput();
            _playerInput.Player.Enable();
        }

        private void OnEnable()
        {
            _health.OnDeath += Death;
            _playerInput.Player.HandAtack.performed += HandAtack;
            _playerInput.Player.FootAtack.performed += FootAtack;
        }

        

        private void OnDisable()
        {
            _health.OnDeath -= Death;
            _playerInput.Player.HandAtack.performed -= HandAtack;
            _playerInput.Player.FootAtack.performed -= FootAtack;
        }
        private void Death()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        private void Update()
        {
            Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
            if (inputVector == Vector2.left) transform.rotation = Quaternion.Euler(0, -90f, 0);
            if (inputVector == Vector2.right) transform.rotation = Quaternion.Euler(0, 90f, 0);
            _animator.SetBool(Walk, inputVector.sqrMagnitude > Vector2.zero.magnitude);
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y) * _movementSpeed;
            _characterController.Move(moveDirection * Time.deltaTime);
        }
        
        private void HandAtack(InputAction.CallbackContext context)
        {
            _animator.SetTrigger(AttackTrigger);
           
        }
        
        private void FootAtack(InputAction.CallbackContext obj)
        {
            _animator.SetTrigger(FootAttackTrigger);
            
        }
        
    }
}

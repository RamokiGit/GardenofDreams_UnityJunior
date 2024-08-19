using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public enum TutorialState
{
    Move,
    HandAttack,
    FootAttack
}

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField, TextArea] private string[] _tutorialTexts;
    private PlayerInput _playerInput;
    public Action EndTutorial;
    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        SetTutorialText(TutorialState.Move);
    }

    private void OnEnable()
    {
        _playerInput.Player.Move.performed += Move;
    }

    private void FootAtack(InputAction.CallbackContext obj)
    {
        EndTutorialGame();
        _playerInput.Player.FootAtack.performed -= FootAtack;
    }

    private void HandAtack(InputAction.CallbackContext obj)
    {
        _playerInput.Player.FootAtack.performed += FootAtack;
        SetTutorialText(TutorialState.FootAttack);
        _playerInput.Player.HandAtack.performed -= HandAtack;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _playerInput.Player.HandAtack.performed += HandAtack;
        SetTutorialText(TutorialState.HandAttack);
        _playerInput.Player.Move.performed -= Move;
    }


    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= Move;
        _playerInput.Player.HandAtack.performed -= HandAtack;
        _playerInput.Player.FootAtack.performed -= FootAtack;
    }

    private void SetTutorialText(TutorialState state)
    {
        _text.text = _tutorialTexts[(int)state];
    }

    private void EndTutorialGame()
    {
        EndTutorial?.Invoke();
        gameObject.SetActive(false);
    }
}

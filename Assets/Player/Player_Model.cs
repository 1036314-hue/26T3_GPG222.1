using System;
using Unity.Netcode;
using UnityEngine;

public class Player_Model : NetworkBehaviour
{
    private Player_View _view;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private int _walkSpeed;
    private Vector2 _movementValue;

    public void Movement(Vector2 _moveInput)
    {
        Debug.Log("Movement");
        _movementValue = _moveInput;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_movementValue.x, 0, _movementValue.y).normalized;
        _rb.MovePosition(_rb.position + direction * (_walkSpeed * Time.deltaTime));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    // Runtime
    PlayerControls pControls;

    [Header("Input Containers")]
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private UnityEvent onShoot;


    private void Awake() {
        // Assign input.
        pControls = new PlayerControls();

        pControls.Keyboard.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        pControls.Keyboard.Shoot.started += ctx => onShoot.Invoke();
    }

#region Input Getters
    public Vector2 GetMoveDirection(){
        return moveDirection;
    }

#endregion

    private void OnEnable() {
        pControls.Enable();
    }

    private void OnDisable() {
        pControls.Disable();
    }
}

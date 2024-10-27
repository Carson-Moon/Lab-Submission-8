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
    [SerializeField] private UnityEvent onSave;
    [SerializeField] private UnityEvent onLoad;


    private void Awake() {
        // Assign input.
        pControls = new PlayerControls();

        pControls.Keyboard.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        pControls.Keyboard.Shoot.started += ctx => onShoot.Invoke();
        pControls.Keyboard.Save.started += ctx => onSave.Invoke();
        pControls.Keyboard.Load.started += ctx => onLoad.Invoke();
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

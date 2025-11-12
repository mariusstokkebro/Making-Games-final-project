using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Items_and_Weapons;
using UnityEngine.Serialization;
using System.Reflection;

public class PlayerScript : BaseEntity, Controls.IPlayerActions
{
    private bool _usingController = false;
    private Vector3 _movementDirection;
    private Vector2 _lookInput;
    private Vector2 _mousePosition;
    
    [SerializeField] private IList<PassiveItemData> items = new List<PassiveItemData>();
    
    [SerializeField] private Weapon saltShaker;
    [SerializeField] private bool usingPrimaryWeapon = true;
    private WeaponBehaviour primaryWeapon;
    private WeaponBehaviour secondaryWeapon;
    private Animator animator;
    void Start()
    {
        primaryWeapon = saltShaker.EquipWeapon(this);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.position += _movementDirection * (movementSpeed * Time.deltaTime);

        if (!_usingController)
        {
            // makes sure player is always facing the mouse position
            _lookInput = LookDirectionFromMouse(_mousePosition);
        }

        if (_lookInput.magnitude > 1e-12)
        {
            Vector3 lookDirection = new Vector3(_lookInput.x, 0, _lookInput.y);
            lookDirection = _matrix.MultiplyPoint3x4(lookDirection);
            transform.right = -lookDirection;
        }
        else if (_movementDirection.magnitude > 1e-12)
        {
            transform.right = -_movementDirection;
        }
    }

    public override void TakeDamage(float amount)
    {
        animator.SetBool("isHit", true);
        base.TakeDamage(amount);
        HUD.Instance.UpdateHealthBar(health);
        health -= amount;
    }

    public void EndHitTaken()
    {
        animator.SetBool("isHit", false);
    }

    protected override void Die()
    {
        animator.SetBool("isDead", true);
    }

    public void DeathAnimationDone()
    {
        base.Die();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 pressed = context.ReadValue<Vector2>();
        Vector3 tmp = new Vector3(pressed.x, 0, pressed.y);
        _movementDirection = _matrix.MultiplyPoint3x4(tmp).normalized;

        if (_usingController != (context.control.device == Gamepad.current))
        {
            _lookInput = Vector2.zero;
            _usingController = (context.control.device == Gamepad.current);
        }
        if (context.performed)
        {
            animator.SetBool("isWalking", pressed.sqrMagnitude > 0.01f);
        }
        else if (context.canceled)
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Takes the mouse position in screen space and calculates the on-screen direction from player to mouse cursor
    private Vector2 LookDirectionFromMouse(Vector2 mousePosition)
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return (_mousePosition - playerScreenPosition).normalized;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _usingController = (context.control.device == Gamepad.current);

        if (_usingController)
            _lookInput = context.ReadValue<Vector2>();
        else
            _mousePosition = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        animator.SetBool("isAttacking", true);
        if (context.performed)
        {
            if (usingPrimaryWeapon)
            {
                primaryWeapon.StartAttack();
            }
            else if (secondaryWeapon)
            {
                secondaryWeapon.StartAttack();
            }
        }
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void AddPassiveItem(PassiveItemData item)
    {
        Debug.Log($"Added item {item.name} to player items");
        items.Add(item);
    }

    public void RemovePassiveItem(PassiveItemData item)
    {
        items.Remove(item);
    }

    public void SetSecondaryWeapon(Weapon item) => secondaryWeapon = item.EquipWeapon(this);

    

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}

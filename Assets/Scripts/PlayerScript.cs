using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Items_and_Weapons;

public class PlayerScript : BaseEntity, Controls.IPlayerActions
{
    private List<SkinnedMeshRenderer> renderers;
    private Color[][] originalColors;
    [SerializeField] private float invulnerabilityDuration = 100.0f;
    public float gravity = -9.81f;
    private float invulnerabilityTimer = 0.0f;
    private Rigidbody rb;
    private bool _usingController = false;
    private Vector3 _movementDirection;
    private Vector3 velocity;
    private Vector2 _lookInput;
    private Vector2 _mousePosition;
    private CharacterController controller;
    private IInteractable currentInteractable;
    [SerializeField] private IList<PassiveItemData> items = new List<PassiveItemData>();
    [SerializeField] private Weapon saltShaker;
    [SerializeField] private Weapon TestSecondary;
    [SerializeField] private bool usingPrimaryWeapon = true;
    private WeaponBehaviour primaryWeapon;
    private WeaponBehaviour secondaryWeapon;
    public Animator animator;
    public AudioClip attackSound;

    public bool HasKey = false;

    void Start()
    {
        renderers = new List<SkinnedMeshRenderer>(GetComponentsInChildren<SkinnedMeshRenderer>());
        originalColors = new Color[renderers.Count][];
        // Save the original color of each renderer's material
        for (int i = 0; i < renderers.Count; i++)
        {
            Material[] mats = renderers[i].materials;
            originalColors[i] = new Color[mats.Length];
            for (int j = 0; j < mats.Length; j++)
            {
                originalColors[i][j] = mats[j].color;
            }
        }

        controller = GetComponent<CharacterController>();
        primaryWeapon = saltShaker.EquipWeapon(this);
        animator = GetComponent<Animator>();
        HUD.Instance.SetPrimaryWeapon(saltShaker);
        HUD.Instance.InitializeHealthBar(health, health / 5);
        HUD.Instance.UpdateHealthBar(health);
        SetSecondaryWeapon(TestSecondary);
    }

    void Update()
    {
        if (invulnerabilityTimer > 0f)
        {
            invulnerabilityTimer -= Time.deltaTime;
            float flash = Mathf.PingPong(Time.time * 10, 1);
            for (int i = 0; i < renderers.Count; i++)
            {
                Material[] mats = renderers[i].materials; // get all materials for this renderer
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j].color = Color.Lerp(originalColors[i][j], Color.white, flash);
                }
            }
        }
        else
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                Material[] mats = renderers[i].materials; // get all materials for this renderer
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j].color = originalColors[i][j];
                }
            }

        }
        velocity.y += gravity * Time.deltaTime;
        Vector3 movevec = _movementDirection * (movementSpeed * Time.deltaTime);
        Vector3 move = movevec + velocity * Time.deltaTime;
        if (knockbackTimer > 0f)
        {
            move += knockbackVelocity * Time.deltaTime;
            knockbackTimer -= Time.deltaTime;
        }
        controller.Move(move);

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
        if (invulnerabilityTimer > 0f)
        {
            Debug.Log("Player is invulnerable, no damage taken");
            return;
        }

        base.TakeDamage(amount);
        HUD.Instance.UpdateHealthBar(health);
        health -= amount;
        if (health > 0)
        {
            animator.SetBool("isHit", true);
        }
        invulnerabilityTimer = invulnerabilityDuration;

    }

    public void EndHitTaken()
    {
        animator.SetBool("isHit", false);
    }

    protected override void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<PlayerInput>().enabled = false;
    }

    public void DeathAnimationDone()
    {
        base.Die();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 pressed = context.ReadValue<Vector2>();
        Vector3 tmp = new Vector3(pressed.x, 0, pressed.y);
        _movementDirection = _matrix.MultiplyPoint3x4(tmp);

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
        if (context.performed)
        {
            if (usingPrimaryWeapon)
            {
                if (!primaryWeapon)
                    Debug.Log("No primary weapon!");
                primaryWeapon.StartAttack();
            }
            else if (secondaryWeapon)
            {
                secondaryWeapon.StartAttack();
            }
            else
            {
                Debug.Log("No secondary weapon!");
            }
        }
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (usingPrimaryWeapon && !secondaryWeapon) return; // No secondary to equip, do nothing

        usingPrimaryWeapon = !usingPrimaryWeapon;
        HUD.Instance.SwitchWeapons();
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

    public void SetSecondaryWeapon(Weapon item)
    {
        secondaryWeapon = item.EquipWeapon(this);
        HUD.Instance.SetSecondaryWeapon(item);
    }

    /** Use for manually dropping secondary weapon */
    public void LoseSecondaryWeapon()
    {
        HUD.Instance.RemoveSecondaryWeapon();
        if (!usingPrimaryWeapon)
        {
            usingPrimaryWeapon = true;
            HUD.Instance.SwitchWeapons();
        }
    }


    public void OnInteract(InputAction.CallbackContext context)
    {

        if (context.performed && currentInteractable != null)
        {
            currentInteractable.Interact(this);
            if (currentInteractable is ItemScript)
            {
                TooltipManager.Instance.HideTooltip();
                currentInteractable = null;
            }
        }

    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
        if (collider.TryGetComponent(out ItemScript itemScript))
        {
            TooltipManager.Instance.ShowTooltip(itemScript.item?.GetDescription());
        }
        if (collider.TryGetComponent(out Key _))
        {
            TooltipManager.Instance.ShowTooltip("Press 'E' to pick up the key");
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<IInteractable>(out var interactable))
        {
            if (currentInteractable == interactable)
                currentInteractable = null;
        }
        if (collider.TryGetComponent(out ItemScript _))
        {
            TooltipManager.Instance.HideTooltip();
        }
        if (collider.TryGetComponent(out Key _))
        {
            TooltipManager.Instance.HideTooltip();
        }
    }

}

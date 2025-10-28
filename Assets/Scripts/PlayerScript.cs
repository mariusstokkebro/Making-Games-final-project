using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : EntityScript, Controls.IPlayerActions
{
    private Vector3 _direction;
    private Vector3 _lookDirection;
    
    [SerializeField]
    private int rotationSpeed = 200;

    private Matrix4x4 _matrix = Matrix4x4.Rotate(Quaternion.Euler(0,-45, 0));

    void Update()
    {
        transform.position += _direction * (movementSpeed * Time.deltaTime);
        
        transform.forward = _lookDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 pressed = context.ReadValue<Vector2>();
        Vector3 tmp = new Vector3(pressed.x, 0, pressed.y);
        _direction = _matrix.MultiplyPoint3x4(tmp);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        if (context.control.device == Mouse.current)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            lookInput -= screenPosition;
            lookInput.Normalize();
        }

        if (lookInput.magnitude > 1e-12)
        {
            Vector3 tmp = new Vector3(lookInput.y, 0, - lookInput.x).normalized;
            _lookDirection = _matrix.MultiplyPoint3x4(tmp);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

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

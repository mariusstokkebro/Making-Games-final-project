using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : EntityScript, Controls.IPlayerActions
{
    private bool _usingController = false;
    private Vector3 _movementDirection;
    private Vector3 _lookDirection;
    private Vector2 _lookInput;

    private Vector2 _mousePosition;
    private Vector2 _stickRotation;
    
    [SerializeField]
    private int rotationSpeed = 200;

    private Matrix4x4 _matrix = Matrix4x4.Rotate(Quaternion.Euler(0,-45, 0));

    void Update()
    {
        transform.position += _movementDirection * (movementSpeed * Time.deltaTime);

        if (!_usingController)
        {
            // makes sure player is always facing mouse position
            _lookInput = LookDirectionFromMouse(_mousePosition);
        }
        
        if(_lookInput.magnitude > 1e-12){
            Vector3 tmp = new Vector3(_lookInput.x, 0, _lookInput.y);
            _lookDirection = _matrix.MultiplyPoint3x4(tmp);
            transform.right = - _lookDirection;
        }else if (_movementDirection.magnitude > 1e-12)
        {
            transform.right = - _movementDirection;
        }
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
    }

    private Vector2 LookDirectionFromMouse(Vector2 mousePosition)
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return (_mousePosition - playerScreenPosition).normalized;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _usingController = (context.control.device == Gamepad.current);

        if (_usingController)
        {
            _lookInput = context.ReadValue<Vector2>();
        }
        else
        {
            _mousePosition = context.ReadValue<Vector2>();
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

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int MoveX = Animator.StringToHash("moveX");
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    public bool FacingLeft { get; set; }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerDirection();
        Move();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat(MoveX, _movement.x);
        _animator.SetFloat(MoveY, _movement.y);
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerDirection()
    {
        FacingLeft = Input.mousePosition.x < _camera.WorldToScreenPoint(transform.position).x;
        _spriteRenderer.flipX = FacingLeft;
    }
}

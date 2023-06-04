using Unity.Mathematics;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    private Animator _animator;
    private ActiveWeapon _activeWeapon;
    private PlayerControls _playerControls;
    private PlayerController _playerController;
    private Camera _camera;

    private GameObject _slashAnim;


    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _animator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _camera = Camera.main;
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void SwingUpFlipAnim()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        _slashAnim.GetComponent<SpriteRenderer>().flipX = _playerController.FacingLeft;
    }

    public void SwingDownFlipAnim()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        _slashAnim.GetComponent<SpriteRenderer>().flipX = _playerController.FacingLeft;
    }

    private void Attack()
    {
        _animator.SetTrigger(AttackTrigger);

        _slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, quaternion.identity);
        _slashAnim.transform.position = transform.position;
    }

    private void MouseFollowWithOffset()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = _camera.WorldToScreenPoint(_playerController.transform.position);

        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        _activeWeapon.transform.rotation = mousePos.x < playerScreenPoint.x
            ? Quaternion.Euler(0, -180, angle)
            : Quaternion.Euler(0, 0, angle);
    }
}

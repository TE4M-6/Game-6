using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// @Daniel K.
/// Initial commit: 29-Nov-2022
/// Last modified: 05 Dec 2022 by @Toni N.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /* EXPOSED FIELDS */
    [Header("Moving")]
    [SerializeField] private float moveSpeed = 5.0f;
    [Header("Dashing")]
    [SerializeField] private float dashingTime = 0.5f;
    [Tooltip("Value is used to multiple the player's velocity!")]
    [SerializeField] [Range(1, 10)] private float dashPower = 1;
    [SerializeField] [Range(1,10)] private float dashCooldown = 1;
    [SerializeField] GameObject DashLight;
    [SerializeField] Slider dashSlider;

    [SerializeField] AudioClip footstep;
    /* HIDDEN FIELDS */
    private Vector2 _rawInputKeys;
    private Vector2 _rawInputMouse;
    private Rigidbody2D _rigidbody;
    private TrailRenderer _trailRenderer;
    private Animator _animator;
    private bool _canDash = true;
    private bool _isDashing; // false by default
    
    // Animator Hash
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Start()
    {
        // Binding Components:
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _animator = GetComponent<Animator>();
        dashSlider.value = dashCooldown;

        // Calling Methods:
    }

    private void Update()
    {
        Move();
        FaceCursor();
        dashSlider.value = Time.time;
    }

    public void FillDash()
    {
        dashSlider.minValue = Time.time;
        dashSlider.maxValue = Time.time + dashCooldown;
    }

    /* FUNCTIONS */
    private void FaceCursor()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        // Debug.Log("worldPosition.x => " + worldPosition.x);
        // Debug.Log("worldPosition.y => " + worldPosition.y);

        if (worldPosition.x > _rigidbody.position.x)
        {
            // Debug.Log("worldPosition.x > _rigidbody.position.x");
            transform.localScale = new Vector2(Mathf.Sign(+1), 1f);
        }
        if (worldPosition.x < _rigidbody.position.x)
        {
            // Debug.Log("worldPosition.x < _rigidbody.position.x");
            transform.localScale = new Vector2(Mathf.Sign(-1), 1f);
        }
        
        // Getting position data:
        // Debug.Log("Playa.X: " + _rigidbody.position.x);
        // Debug.Log("Mouse.X: " + worldPosition.x);
    }
    private void Move()
    {
        Vector2 playerVelocity = new Vector2(_rawInputKeys.x * moveSpeed, _rawInputKeys.y * moveSpeed);

        if (playerVelocity == new Vector2(0, 0))
            _animator.SetBool(IsMoving, false);
        
        if (!_isDashing)
        {
            _rigidbody.velocity = playerVelocity;
            _trailRenderer.emitting = false;
        }
        if (_isDashing)
        {
            _rigidbody.velocity = dashPower * playerVelocity;
            _trailRenderer.emitting = true;
        }
    }

    /* EVENT FUNCTIONS */
    public void OnStep(AnimationEvent animationEvent)
    {
        SoundManager.instance.PlaySingle(footstep);
        
    }
    private void OnMove(InputValue inputValue)
    {
        _rawInputKeys = inputValue.Get<Vector2>();
        _animator.SetBool(IsMoving, true);
    }
    
    private void OnDash(InputValue inputValue)
    {
        // if (!_canDash) return;
        StartCoroutine(DashCoroutine());
    }
    
    /* COROUTINE */
    private IEnumerator DashCoroutine()
    {
        // Condition:
        if (!_canDash) yield break;
        
        _canDash = false;
        _isDashing = true;
        DashLight.SetActive(false);
        FillDash();
        yield return new WaitForSeconds(dashingTime);
        _isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
        DashLight.SetActive(true);
    }
}

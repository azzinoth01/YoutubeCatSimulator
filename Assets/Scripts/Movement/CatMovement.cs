using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour, PlayerInput.ICatMovementActions {

    private PlayerInput _inputSystem;
    private Vector3 _moveDirection;
    private Rigidbody _body;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _howlAnnoyance;
    [SerializeField] private float _howlAnnoyanceDelay;
    private bool _canAnnoy;
    private bool _isGround;
    private bool _canDoubleJump;
    private float _currentHowlDelay;

    [SerializeField] private List<AudioClip> _meowSounds;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIInteractions _uiIntercations;

    [SerializeField] private Animator _animator;

    [SerializeField] private AudioClip _jumpAudio;
    [SerializeField] private AudioSource _walkSound;


    public bool IsGround {
        get {
            return _isGround;
        }

        set {

            _isGround = value;
            if (_isGround == true) {
                _animator.SetBool("Jump", false);
            }
        }
    }

    public void OnHowl(UnityEngine.InputSystem.InputAction.CallbackContext context) {

        if (context.started) {
            Debug.Log("Howl");
            int index = Random.Range(0, _meowSounds.Count);

            AudioSource.PlayClipAtPoint(_meowSounds[index], transform.position);

            if (_canAnnoy) {
                _canAnnoy = false;
                _currentHowlDelay = _howlAnnoyanceDelay;
                _gameManager.IncreaseAnnoyance(_howlAnnoyance);


            }
            _animator.SetBool("Meow", true);

        }
        //     throw new System.NotImplementedException();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context) {

        if (context.started) {
            Debug.Log("Jump");
            if (_isGround == true) {
                _isGround = false;
                _body.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
                _canDoubleJump = true;

                _animator.SetBool("Jump", true);

            }
            else if (_canDoubleJump == true) {
                _canDoubleJump = false;
                _body.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
                _animator.SetBool("Jump", true);
            }

            AudioSource.PlayClipAtPoint(_jumpAudio, transform.position);
        }

        //  throw new System.NotImplementedException();
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        //  Debug.Log(context);

        Vector2 move = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(move.x, 0, move.y);
        Debug.Log("Move: " + move);

        if (move != Vector2.zero) {
            _animator.SetBool("Walk", true);
            _animator.SetBool("Idle", false);
        }
        else {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
        }

        //throw new System.NotImplementedException();
    }

    public void OnQuit(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (context.started) {
            _uiIntercations.OpenMenu();
        }
    }

    // Start is called before the first frame update
    void Start() {



        _body = GetComponent<Rigidbody>();
        if (_inputSystem == null) {
            _inputSystem = new PlayerInput();
            _inputSystem.CatMovement.SetCallbacks(this);
            _inputSystem.CatMovement.Enable();
        }
    }

    // Update is called once per frame
    void Update() {

        Quaternion baseRot = Quaternion.Euler(new Vector3(0, 45 + 90, 0));


        Vector3 move = baseRot * _moveDirection.normalized * _movementSpeed;

        if (move != Vector3.zero) {
            Quaternion rot = Quaternion.LookRotation(move, transform.up);

            transform.rotation = rot;
            if (_walkSound.isPlaying == false) {
                _walkSound.Play();
            }

        }
        else {
            _walkSound.Stop();
        }


        move.y = _body.velocity.y;

        _body.velocity = move;

        _currentHowlDelay = _currentHowlDelay - Time.deltaTime;

        if (_canAnnoy == false && _currentHowlDelay <= 0) {
            _canAnnoy = true;
        }


    }
}

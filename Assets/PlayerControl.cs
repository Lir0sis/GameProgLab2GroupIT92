using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public TextMeshProUGUI ScoreUIText;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Transform _camera;

    public float Sensetivity = 2.5f;
    public LayerMask ItemLayer;

    public float SensorRadius = 0.1f;
    public LayerMask GroundLayer;

    public float MovementVelocity = 10f;
    public float JumpForce = 10f;

    public int CurrentScore;
    public int ScoreRequired = 5;

    private float _horAxis;
    private float _vertAxis;
    private Vector2 _moveVector;
    private bool _jumpButton;
    private Vector2 _mouseAxis;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseAxis.x += Input.GetAxis("Mouse X") * Sensetivity;
        _mouseAxis.y += Input.GetAxis("Mouse Y") * Sensetivity;

        _horAxis = Input.GetAxis("Horizontal");
        _vertAxis = Input.GetAxis("Vertical");
        _moveVector = new Vector2(_horAxis, _vertAxis).normalized;

        _jumpButton = Input.GetButton("Jump");

        transform.localEulerAngles = new Vector3(0, _mouseAxis.x, 0);
        _camera.localEulerAngles = new Vector3(-_mouseAxis.y, 0, 0);
    }

    private void FixedUpdate()
    {
        var velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        var bottom = new Vector3(_collider.bounds.center.x, _collider.bounds.center.y - _collider.bounds.size.y / 2, _collider.bounds.center.z);

        ScoreUIText.text = "Score: " + CurrentScore;
        if (CurrentScore >= ScoreRequired)
        {
            SceneManager.LoadScene("Menu");
        }

        velocity += transform.right * _moveVector.x * MovementVelocity * Time.fixedDeltaTime;
        velocity += transform.forward * _moveVector.y * MovementVelocity * Time.fixedDeltaTime;

        if (_jumpButton && Mathf.Abs(_rigidbody.velocity.y) < .1 && Physics.CheckSphere(bottom, SensorRadius, GroundLayer))
        {
            Debug.LogWarning("Jump Block...");
            velocity.Set(0, JumpForce * Time.fixedDeltaTime, 0);
        }

        _rigidbody.velocity = velocity;
    }
}

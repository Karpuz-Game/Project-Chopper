using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    [Header("Floats")]
    public float maxSpeed;
    private float _horizontal;
    public float horizontalSpeed;
    public float clampXmin, clampXmax;
    public float clampYmin, clampYmax;
    public float bulletSpeed = 50f;
    public float forwardRotation, backwardRotation,rotationSpeed;


    [FormerlySerializedAs("gravitiy")] [Header("Vectors")]
    public Vector3 gravity;
    public Vector3 thrust;
    public Vector3 horizontalThrust;
    private Vector3 _clampPosition;
    private Vector3 _mouseMove;
    private Vector3 _chopperRotation;
    private Vector3 _velocity = Vector3.zero;



    [Header("GameObjects")]
    private GameObject _turret;
    private GameObject _crossHair;


    [Header("Cams")]
    private Camera _mainCam;

    [Header("Others")]
    public chopperBulletPool chopperBulletPool;


    private void Start() {
        _turret = gameObject.transform.GetChild(0).gameObject;
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _crossHair = gameObject.transform.GetChild(1).gameObject;
    }

    private void Update() {
        MouseTracking();
        //TODO: fix fire back
        if (Input.GetMouseButtonDown(0)) {
            Fire(chopperBulletPool.GetNextAvailableObject());
        }
    }
    private void FixedUpdate() {
        ChopperPhysics();
    }



    private void ChopperPhysics() {
        _horizontal = Input.GetAxisRaw("Horizontal");

        _velocity += gravity * Time.deltaTime;

        if (_horizontal != 0){
            _velocity += horizontalThrust * (_horizontal * Time.fixedDeltaTime * horizontalSpeed);
        }

        if (!(_horizontal > 0) || !(_chopperRotation.z > forwardRotation)) {
            if (_horizontal < 0 && _chopperRotation.z < backwardRotation) {
                _chopperRotation.z += Time.deltaTime * rotationSpeed;
            }
            else {
                if (_chopperRotation.z < 0 && _chopperRotation.z != 0) {
                    _chopperRotation.z += Time.deltaTime * rotationSpeed;
                }

                if (_chopperRotation.z > 0 && _chopperRotation.z != 0) {
                    _chopperRotation.z -= Time.deltaTime * rotationSpeed;
                }
            }
        }
        else
        {
            _chopperRotation.z -= Time.deltaTime * rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(_chopperRotation);
        
        if (Input.GetKey(KeyCode.W)) {
            _velocity += thrust;
        }

        var position = transform.position;
        _clampPosition = position;

        _clampPosition.x = Mathf.Clamp(_clampPosition.x, clampXmin, clampXmax);
        _clampPosition.y = Mathf.Clamp(_clampPosition.y, clampYmin, clampYmax);
        
        position = _clampPosition;

        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);
        position += _velocity * Time.deltaTime;
        transform.position = position;

        if (_velocity.x == 0 || _horizontal != 0) return;
        if (_velocity.x > 0)
            _velocity -= (horizontalThrust / 5) * (Time.fixedDeltaTime * horizontalSpeed);
        if (_velocity.x < 0)
            _velocity += (horizontalThrust / 5) * (Time.fixedDeltaTime * horizontalSpeed);
    }

    private void MouseTracking() {
        _mouseMove = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _mouseMove.Set(_mouseMove.x, _mouseMove.y, transform.position.z);

        var position = _turret.transform.position;
        var angleRad = Mathf.Atan2(
           _mouseMove.y - position.y,
           _mouseMove.x - position.x
           );

        var angleDeg = (180 / Mathf.PI) * angleRad;

        _turret.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(angleDeg, -80.0F, 41.15F));

        _crossHair.transform.position = _mouseMove;

    }

    private void Fire(GameObject bullet) {
        var position = _turret.transform.position;
        bullet.transform.position = position;
        bullet.transform.rotation = _turret.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce((_crossHair.transform.position - position).normalized * (bulletSpeed * 1000));
    }
}

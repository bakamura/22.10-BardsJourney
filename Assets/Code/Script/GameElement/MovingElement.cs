using UnityEngine;
using UnityEngine.Events;

public class MovingElement : MonoBehaviour {

    [Header("Movement")]

    private float _movementState = 0f;
    [SerializeField] private float _movementDuration = 8f;
    [SerializeField] private float _heightStart = 0f;
    [SerializeField] private float _heightMax = 2f;
    [SerializeField] private float _heightMin = -0.5f;
    private float _movementChangePoint;
    private float _heightTotal;
    [SerializeField] private float _sizeInitial = 1f;
    [SerializeField] private float _sizeFinal = 4f;

    [HideInInspector] public UnityEvent onChangeHeightDirection;
    [HideInInspector] public UnityEvent onCompletePath;

    private void Start() {

        _heightTotal = (_heightMax - _heightStart) + (_heightMax - _heightMin);
        _movementChangePoint = (_heightMax - _heightStart) / _heightTotal;
    }

    private void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {
        if (_movementState < 1f) {
            float lastMovementState = _movementState;
            _movementState += Time.deltaTime / _movementDuration;


            float _heightAmountPostChange = (_movementState * _heightTotal) - (_heightMax - _heightStart);
            transform.position = new Vector3(transform.position.x, _movementState < _movementChangePoint ? _heightStart + (_movementState * _heightTotal) : _heightMax - _heightAmountPostChange, transform.position.z);

            float size = _movementState < _movementChangePoint ? _sizeInitial : _sizeInitial + ((_sizeFinal - _sizeInitial) * _heightAmountPostChange / (_heightMax - _heightMin));
            transform.localScale = new Vector3(size, size, 1);

            if (lastMovementState < _movementChangePoint && _movementState >= _movementChangePoint) {
                GetComponent<SpriteRenderer>().sortingOrder += 5;
                onChangeHeightDirection.Invoke();
            }
        }
        else onCompletePath.Invoke();
    }

    // To prevent having to change variable protection level
    public void ResetMovement() {
        _movementState = 0;
        transform.position = new Vector3(transform.position.x, _heightStart, transform.position.z);
        transform.localScale = new Vector3(_sizeInitial, _sizeInitial, 1);
    }

}

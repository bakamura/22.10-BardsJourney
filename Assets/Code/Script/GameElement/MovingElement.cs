using UnityEngine;
using UnityEngine.Events;

public class MovingElement : MonoBehaviour {

    [Header("Movement")]

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public float movementState = 0f;
    [SerializeField] private float _movementDuration = 8f;
    [SerializeField] private float _heightStart = 0f;
    [SerializeField] private float _heightMax = 2f;
    [SerializeField] private float _heightMin = -0.5f;
    private float _movementChangePoint;
    private float _heightTotal;
    [SerializeField] private float _sizeInitial = 1f;
    [SerializeField] private float _sizeFinal = 4f;

    private SpriteRenderer _sr;

    [HideInInspector] public UnityEvent onChangeHeightDirection;
    [HideInInspector] public UnityEvent onCompletePath;

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();

        _heightTotal = (_heightMax - _heightStart) + (_heightMax - _heightMin);
        _movementChangePoint = (_heightMax - _heightStart) / _heightTotal;
    }

    private void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {
        if (canMove) {
            if (movementState < 1f) {
                float lastMovementState = movementState;
                movementState += Time.deltaTime / _movementDuration;


                float _heightAmountPostChange = (movementState * _heightTotal) - (_heightMax - _heightStart);
                transform.position = new Vector3(transform.position.x, movementState < _movementChangePoint ? _heightStart + (movementState * _heightTotal) : _heightMax - _heightAmountPostChange, transform.position.z);

                float size = movementState < _movementChangePoint ? _sizeInitial : _sizeInitial + ((_sizeFinal - _sizeInitial) * _heightAmountPostChange / (_heightMax - _heightMin));
                transform.localScale = new Vector3(size, size, 1);

                _sr.sortingOrder = (int) Mathf.Floor(movementState * 100f);
                if (lastMovementState < _movementChangePoint && movementState >= _movementChangePoint) {
                    GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
                    onChangeHeightDirection.Invoke();
                }
            }
            else onCompletePath.Invoke();
        }
    }

    // To prevent having to change variable protection level
    public void ResetMovement() {
        movementState = 0;
        transform.position = new Vector3(transform.position.x, _heightStart, transform.position.z);
        transform.localScale = new Vector3(_sizeInitial, _sizeInitial, 1);
    }

}

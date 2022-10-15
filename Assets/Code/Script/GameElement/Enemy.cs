using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public static List<Enemy> enemyList = new List<Enemy>();

    [Header("Movement")]

    private float _movementState = 0f;
    [SerializeField] private float _movementDuration = 8f;
    [SerializeField] private float _heightStart = 0f;
    [SerializeField] private float _heightMax = 2f;
    [SerializeField] private float _heightMin = -0.5f;
    private float _heightTotal;
    [SerializeField] private float _sizeInitial = 1f;
    [SerializeField] private float _sizeFinal = 4f;

    [Header("Rythm")]

    [SerializeField] private Rythm _rythm;
    private int _rythmState = 0;

    private void Start() {
        enemyList.Add(this);

        _heightTotal = (_heightMax - _heightStart) + (_heightMax - _heightMin);
    }

    private void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {
        if (_movementState < 1f) {
            _movementState += Time.deltaTime / _movementDuration;

            transform.position = new Vector3(transform.position.x,
                _movementState < (_heightMax - _heightStart) / _heightTotal ? _movementState * _heightTotal : _heightMax - ((_movementState * _heightTotal) - (_heightMax - _heightStart)), transform.position.z);

            float size = _movementState < (_heightMax - _heightStart) / _heightTotal ?
                _sizeInitial : _sizeInitial + ((_sizeFinal - _sizeInitial) * ((_movementState * _heightTotal) - (_heightMax - _heightStart)) / (_heightMax - _heightMin));

            transform.localScale = new Vector3(size, size, 1);
        }
        else {
            // Fail level
        }
    }

    public bool CheckRythm(Rythm.DrumNote note) {
        if (_rythm.drumNote[_rythmState] == note) {
            _rythmState++;
            if (_rythmState == _rythm.Size) Destroy(gameObject); // Provisory
            return true;
        }
        _rythmState = 0;
        return false;
    }

}

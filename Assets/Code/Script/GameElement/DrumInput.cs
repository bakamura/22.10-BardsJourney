using System;
using UnityEngine;

public class DrumInput : MonoBehaviour {

    [Header("Metronome")]

    private float _metronomeState = 0;
    public float metronomeSpeed = 1f;
    public float metronomeSuccessInterval = 0.25f;

    [SerializeField] private RectTransform _needleRectTransform;
    [SerializeField] private float _needleAmplitude = 100f;

    [Header("Drums")]

    // [PC Testing]
    [SerializeField] private KeyCode aDrumkey;
    [SerializeField] private KeyCode bDrumkey;
    [SerializeField] private KeyCode cDrumkey;
    [SerializeField] private KeyCode dDrumkey;

    private void Update() {
        UpdateMetronome();

        // [PC Testing]
        if (Input.GetKeyDown(aDrumkey)) DrumBtnPress(1);
        if (Input.GetKeyDown(bDrumkey)) DrumBtnPress(2);
        if (Input.GetKeyDown(cDrumkey)) DrumBtnPress(3);
        if (Input.GetKeyDown(dDrumkey)) DrumBtnPress(4);
    }

    private void UpdateMetronome() {
        _metronomeState += metronomeSpeed * Time.deltaTime;
        if (_metronomeState > 1f) {
            _metronomeState = 1f - (_metronomeState - 1f);
            metronomeSpeed *= -1f;
        }
        else if (_metronomeState < 0f) {
            _metronomeState = Mathf.Abs(_metronomeState);
            metronomeSpeed *= -1f;
        }

        _needleRectTransform.rotation = Quaternion.Euler(0, 0, (_metronomeState * _needleAmplitude) - (_needleAmplitude / 2));
        // USE TO GET NEEDLE POINT SUCCESS START _needleRectTransform.rotation = Quaternion.Euler(0, 0, ((0.5f - (metronomeSuccessInterval/2)) * _needleAmplitude) - (_needleAmplitude / 2));
    }

    private bool GetMetronomeSuccess() {
        float halfState = _metronomeState > 0.5f ? 1 - _metronomeState : _metronomeState;
        if (halfState > (1 - metronomeSuccessInterval) / 2) return true;
        return false;
    }

    public void DrumBtnPress(int drumN) {
        if (GetMetronomeSuccess()) {
            // visual success indicator
            if (Enemy.enemyList[0].CheckRythm((Rythm.DrumNote) drumN)) Debug.Log("Hit Correct");
            else Debug.Log("Hit Wrong");
            // get closest enemy with matching result
            // break any sequences that where interrupted
        }
        else {
            // visual failure indicator
            Debug.Log("MetroFailure");
            // break current sequence
        }
    }

}

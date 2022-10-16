using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrumInput : MonoBehaviour {

    [Header("Metronome")]

    [SerializeField] private float _metronomeState = 0.5f;
    public float metronomeSpeed = 1f;
    public float metronomeSuccessInterval = 0.25f;

    [SerializeField] private RectTransform _needleRectTransform;
    [SerializeField] private float _needleAmplitude = 100f;

    private bool _lastMetronomeSuccess;
    private bool _hasInputed = false;

    [Header("Drums")]

    [SerializeField] private AudioSource _drumSource;
    [SerializeField] private float _drumFeedbackDuration = 0.6f;
    [SerializeField] private Image[] _drumImageFeedback = new Image[4]; // positions
    [SerializeField] private AudioClip[] _drumSfx = new AudioClip[5]; // 0 is fail
    [SerializeField] private Sprite[] _drumFeedbackSprite = new Sprite[5]; //0 is fail

    [Header("Proxy")]

    private EnemyWaveManager _waveManger;

    // [PC Testing]
    [SerializeField] private KeyCode aDrumkey;
    [SerializeField] private KeyCode bDrumkey;
    [SerializeField] private KeyCode cDrumkey;
    [SerializeField] private KeyCode dDrumkey;

    private void Start() {
        _waveManger = GetComponent<EnemyWaveManager>();
    }

    private void Update() {
        UpdateMetronome();

        // [PC Testing]
        PcTesting();
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
        //_needleRectTransform.rotation = Quaternion.Euler(0, 0, ((0.5f - (metronomeSuccessInterval / 2)) * _needleAmplitude) - (_needleAmplitude / 2)); // Get Success Interval

        if (_lastMetronomeSuccess != GetMetronomeSuccess() && _lastMetronomeSuccess) {
            if (_hasInputed) _hasInputed = false;
            else _waveManger.enemiesSpawned[0].ResetRythmBar();
        }
        _lastMetronomeSuccess = GetMetronomeSuccess();
    }

    private bool GetMetronomeSuccess() {
        float halfState = _metronomeState > 0.5f ? 1 - _metronomeState : _metronomeState;
        if (halfState > (1 - metronomeSuccessInterval) / 2) return true;
        return false;
    }

    public void DrumBtnPress(int drumN) {
        if (GetMetronomeSuccess() && !_hasInputed) {
            if (_waveManger.enemiesSpawned[0].CheckRythm((Rythm.DrumNote)drumN + 1)) {
                _hasInputed = true;
                StartCoroutine(ShowFeedback(drumN, 2));
            }
            else StartCoroutine(ShowFeedback(drumN, 1));
        }
        else {
            StartCoroutine(ShowFeedback(drumN, 0));
            _waveManger.enemiesSpawned[0].ResetRythmBar();
        }
    }

    private IEnumerator ShowFeedback(int drumN, int success) {
        if (success == 0) Debug.Log("WrongTiming");
        if (success == 1) Debug.Log("WrongDrum");
        else Debug.Log("CorrectDrum");

        _drumSource.clip = success == 2? _drumSfx[drumN + 1] : _drumSfx[0];
        _drumSource.Play();

        Image feedbackImage = _drumImageFeedback[drumN];
        feedbackImage.sprite = success == 2? _drumFeedbackSprite[drumN + 1] : _drumFeedbackSprite[0];
        float alpha = 1;
        while (alpha > 0) {
            alpha -= Time.deltaTime / _drumFeedbackDuration;
            feedbackImage.color = new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, alpha);
            // Add scale factor

            yield return null;
        }
    }

    // [PC Testing]
    private void PcTesting() {
        if (Input.GetKeyDown(aDrumkey)) DrumBtnPress(0);
        if (Input.GetKeyDown(bDrumkey)) DrumBtnPress(1);
        if (Input.GetKeyDown(cDrumkey)) DrumBtnPress(2);
        if (Input.GetKeyDown(dDrumkey)) DrumBtnPress(3);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Passerby : MonoBehaviour {

    [Header("Movement")]

    private bool _canMove = true;
    [SerializeField] private Vector3 _initialPos;
    [SerializeField] private Vector3 _finalPos;
    private float _currentPos = 0;
    [SerializeField] private float _movementDuration = 24f;

    [Header("Rythm")]

    [SerializeField] private SpriteRenderer[] _rythmNotes = new SpriteRenderer[5];
    [SerializeField] private Rythm _rythm;
    private int _rythmState = 0;

    [Header("PowerUp")]

    private Image _powerupImage;

    private void Start() {
        for (int i = 0; i < _rythm.Size; i++) {
            _rythmNotes[i].sprite = _rythm.noteSprites[(int)_rythm.drumNote[i]];
            _rythmNotes[i].enabled = _rythm.drumNote[i] != Rythm.DrumNote.Null;
        }

        _powerupImage = GameObject.FindGameObjectWithTag("PowerupImage").GetComponent<Image>();
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        if (_canMove) {
            _currentPos += Time.deltaTime / _movementDuration;
            if (_currentPos > 1) Destroy(gameObject);
            transform.position = Vector3.Lerp(_initialPos, _finalPos, _currentPos);
        }
    }

    public bool CheckRythm(Rythm.DrumNote note) {
        // Handle rythm sequences before return
        if (_rythm.drumNote[_rythmState] == note) {
            StartCoroutine(ShowFeedback());
            _rythmState++;
            if (_rythmState == _rythm.Size) {
                StartCoroutine(FadeOut());
            }
            return true;
        }
        ResetRythmBar();
        return false;
    }

    private IEnumerator ShowFeedback() {
        SpriteRenderer noteSr = _rythmNotes[_rythmState];

        float alpha = 1;
        while (alpha > 0) {
            alpha -= Time.deltaTime;
            noteSr.color = new Color(noteSr.color.r, noteSr.color.g, noteSr.color.b, alpha);
            noteSr.transform.localScale = new Vector3(2 - alpha, 2 - alpha, 1);

            yield return null;
        }
    }

    public void ResetRythmBar() {
        StopAllCoroutines();
        foreach (SpriteRenderer sr in _rythmNotes) {
            sr.color = Color.white;
            sr.transform.localScale = Vector3.one;
        }
        _rythmState = 0;
    }

    private IEnumerator FadeOut() {
        _canMove = false;
        GetComponent<Animator>().SetTrigger("Charmed");
        _rythmNotes[0].transform.parent.gameObject.SetActive(false);
        // Give Powerup
        _powerupImage.color = Color.white;

        yield return new WaitForSeconds(1.5f);

        float alpha = 1;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (alpha > 0) {
            alpha -= Time.deltaTime;
            sr.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
        Destroy(gameObject);
    }
}

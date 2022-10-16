using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    [Header("Rythm")]

    [SerializeField] private SpriteRenderer[] _rythmNotes = new SpriteRenderer[5];
    [SerializeField] private Rythm _rythm;
    private int _rythmState = 0;

    [HideInInspector] public EnemyWaveManager waveManager;
    private MovingElement _movingElement;

    private void Start() {
        GetComponent<MovingElement>().onChangeHeightDirection.AddListener(SetBarAboveGround);
        GetComponent<MovingElement>().onCompletePath.AddListener(AttackPlayer);
        _movingElement = GetComponent<MovingElement>();

        for (int i = 0; i < _rythm.Size; i++) {
            _rythmNotes[i].sprite = _rythm.noteSprites[(int)_rythm.drumNote[i]];
            _rythmNotes[i].enabled = _rythm.drumNote[i] != Rythm.DrumNote.Null;
        }
    }

    private void Update() {
        _rythmNotes[0].transform.parent.GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Floor(_movingElement.movementState * 100f) - 1;
        foreach (SpriteRenderer sr in _rythmNotes) sr.sortingOrder = (int)Mathf.Floor(_movingElement.movementState * 100f);
    }

    private void SetBarAboveGround() {

        _rythmNotes[0].transform.parent.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
        for (int i = 0; i < _rythmNotes.Length; i++) _rythmNotes[i].sortingLayerName = "InFront";
    }

    private void AttackPlayer() {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack() {
        // Start anim

        waveManager.loseScreen.SetActive(true);

        yield return new WaitForSeconds(2.5f); // Set duration to be adjustable

        // Fail Level

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        StopCoroutine(Attack());
        GetComponent<MovingElement>().canMove = false;
        GetComponent<Animator>().SetTrigger("Charmed");
        _rythmNotes[0].transform.parent.gameObject.SetActive(false);
        waveManager.enemiesSpawned.RemoveAt(0);

        yield return new WaitForSeconds(1.5f);

        float alpha = 1;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (alpha > 0) {
            alpha -= Time.deltaTime;
            sr.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
        waveManager.charmCount++;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public static List<Enemy> enemyList = new List<Enemy>();

    [Header("Rythm")]

    [SerializeField] private SpriteRenderer[] _rythmNotes = new SpriteRenderer[5];
    [SerializeField] private Rythm _rythm;
    private int _rythmState = 0;

    private void Start() {
        enemyList.Add(this);

        GetComponent<MovingElement>().onChangeHeightDirection.AddListener(SetBarAboveGround);
        GetComponent<MovingElement>().onCompletePath.AddListener(AttackPlayer);
    }

    private void SetBarAboveGround() {
        _rythmNotes[0].transform.parent.GetComponent<SpriteRenderer>().sortingOrder += 5;
        for (int i = 0; i < _rythmNotes.Length; i++) _rythmNotes[i].sortingOrder += 5;
    }

    private void AttackPlayer() {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack() {
        // Start anim

        yield return new WaitForSeconds(1f); // Set duration to be adjustable

        // Fail Level
    }

    public bool CheckRythm(Rythm.DrumNote note) {
        // Handle rythm sequences before return
        if (_rythm.drumNote[_rythmState] == note) {
            _rythmState++;
            if (_rythmState == _rythm.Size) {
                StopAllCoroutines();
                Destroy(gameObject); // Provisory, should play charmed animation instead
            }
            return true;
        }
        _rythmState = 0;
        return false;
    }

}

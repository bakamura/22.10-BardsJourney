using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {

    [SerializeField] private GameObject[] _enemyList = new GameObject[5];

    [SerializeField] private EnemySpawn[] _enemiesToSpawn = new EnemySpawn[15];
    [HideInInspector] public List<Enemy> enemiesSpawned;

    [HideInInspector] public int charmCount = 0;
    public GameObject loseScreen;
    public GameObject winScreen;

    private void Start() {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() {
        for (int i = 0; i < _enemiesToSpawn.Length; i++) {
            Enemy enemySpawned = Instantiate(_enemyList[_enemiesToSpawn[i].typeOf], new Vector3(Random.Range(-1.8f, 1.8f), -2.5f, 0), Quaternion.identity).GetComponent<Enemy>();
            if (enemySpawned != null) {
                enemySpawned.waveManager = this;
                enemiesSpawned.Add(enemySpawned);
            }
            else charmCount++;

            yield return new WaitForSeconds(_enemiesToSpawn[i].timeOf);
        }
        while (true) {
            if (charmCount == _enemiesToSpawn.Length) break;

            yield return null;
        }
        winScreen.SetActive(true);
    }
}

[System.Serializable]
struct EnemySpawn {
    public float timeOf;
    public int typeOf;
}
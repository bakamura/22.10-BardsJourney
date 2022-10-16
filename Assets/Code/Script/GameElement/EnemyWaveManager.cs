using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {

    [SerializeField] private GameObject[] _enemyList = new GameObject[5];

    [SerializeField] private EnemySpawn[] _enemiesToSpawn = new EnemySpawn[15];
    [HideInInspector] public List<Enemy> enemiesSpawned;

    public GameObject loseScreen;

    private void Start() {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() {
        for (int i = 0; i < _enemiesToSpawn.Length; i++) {
            Enemy enemySpawned = Instantiate(_enemyList[_enemiesToSpawn[i].typeOf], new Vector3(Random.Range(-1.8f, 1.8f), 0, 0), Quaternion.identity).GetComponent<Enemy>();
            enemySpawned.waveManager = this;
            enemiesSpawned.Add(enemySpawned);

            yield return new WaitForSeconds(_enemiesToSpawn[i].timeOf);
        }
    }
}

[System.Serializable]
struct EnemySpawn {
    public float timeOf;
    public int typeOf;
}
using System.Collections;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {

    [SerializeField] private GameObject[] _enemyList = new GameObject[5];

    private EnemySpawn[] _enemiesToSpawn = new EnemySpawn[15];

    private void Start() {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() {
        for (int i = 0; i < _enemiesToSpawn.Length; i++) {
            Instantiate(_enemyList[_enemiesToSpawn[i].typeOf], new Vector3(Random.Range(-1.8f, 1.8f), 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(_enemiesToSpawn[i].timeOf);
        }
    }

}

struct EnemySpawn {
    public float timeOf;
    public int typeOf;
}
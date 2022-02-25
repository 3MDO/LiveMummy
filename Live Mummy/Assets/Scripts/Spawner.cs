using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _spawnDelay = 12f;
    [SerializeField] Mummy[] _mummyPrefabs;

    float _nextSpawnTime;
    int _spawnCount;

    void Update()
    {
        if (ReadyToSpawn())
            StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float delay = _spawnDelay - _spawnCount;
        delay = Mathf.Max(1, delay);
       

        _nextSpawnTime = Time.time + delay;
        _spawnCount++;

        int randomIndex = UnityEngine.Random.Range(0, _mummyPrefabs.Length);
        var mummyPrefab = _mummyPrefabs[randomIndex];

        var mummy = Instantiate(mummyPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        mummy.StartWalking();
    }

    bool ReadyToSpawn() => Time.time >= _nextSpawnTime;
}

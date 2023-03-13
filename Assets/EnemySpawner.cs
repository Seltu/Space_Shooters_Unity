using UnityEngine;
using UnityEngine.U2D;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime;
    public EnemyShip enemyPrefab;
    public SpriteShapeController curve;
    private float _spawnTimer;
    private void Update()
    {
        if(_spawnTimer > 0) _spawnTimer -= Time.deltaTime;
        else
        {
            var enemy = Instantiate(enemyPrefab, transform);
            enemy.curve = curve;
            _spawnTimer = spawnTime;
        }
    }
}

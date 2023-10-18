using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnRate = 0.8f;
    public float maxSpawnRate = 1.8f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));

        //float gameSpeedFactor = GameManager.Instance.gameSpeed / GameManager.Instance.initialGameSpeed;

        //// Adjust the spawn rate according to the game speed.
        //float adjustedMaxSpawnRate = maxSpawnRate / gameSpeedFactor;
        //float adjustedMinSpawnRate = minSpawnRate / gameSpeedFactor;

        //Invoke(nameof(Spawn), Random.Range(adjustedMinSpawnRate, adjustedMaxSpawnRate));
    }
}

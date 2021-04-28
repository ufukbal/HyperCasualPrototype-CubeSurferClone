using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject[] cubePrefabs;

    public GameObject environment;

    private List<GameObject> environmentPool = new List<GameObject>();
    private List<GameObject> CubePool;


    private float levelStartZ = -2.3f;
    private float levelEndZ = 43f;
    private float xBoundary = 0.155f;

    [Range(0.05f, 3f)]
    public float spawnZ = 0.1f;
    private int prefabIndex;
    private Vector3 cubeSpawnPoint;

    private void Awake()
    {
        #region Singleton

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        #endregion //Singleton
    }
    private void Start()
    {
        CreateLevel(Vector3.zero);
    }
    public void CreateLevel(Vector3 envPos)
    {
        if (envPos.z / 45 > 1)
        {
            SendEnvironmentToPool();
        }

        if (GetEnvironmentFromPool())
        {
            InstantiatePoolItem(GetEnvironmentFromPool(), envPos, Quaternion.identity);

        }
        else
        {
            GameObject.Instantiate(environment, envPos, Quaternion.identity);
        }
        for (float z = levelStartZ + envPos.z; z < levelEndZ + envPos.z; z += spawnZ)
        {
            //TO-DO: Implement Object Pool for Cubes
            prefabIndex = Random.Range(0, cubePrefabs.Length);
            cubeSpawnPoint = new Vector3(Random.Range(-xBoundary, xBoundary), cubePrefabs[prefabIndex].transform.position.y, z);

            GameObject.Instantiate(cubePrefabs[prefabIndex], cubeSpawnPoint, Quaternion.identity);

        }

    }

    private void SendEnvironmentToPool()
    {
        GameObject unusedEnvironment = GameObject.FindGameObjectWithTag("Environment");
        environmentPool.Add(unusedEnvironment);
        unusedEnvironment.SetActive(false);

    }

    private GameObject GetEnvironmentFromPool()
    {
        GameObject poolItem;
        if (environmentPool.Count == 0 || environmentPool == null) return null;
        poolItem = environmentPool[0];
        return poolItem;
    }

    private void InstantiatePoolItem(GameObject gameObj, Vector3 pos, Quaternion rot)
    {
        gameObj.transform.position = pos;
        gameObj.transform.rotation = rot;
        gameObj.SetActive(true);
        environmentPool.Remove(gameObj);
    }

}

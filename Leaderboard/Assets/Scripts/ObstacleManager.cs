using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager instance;
    public static ObstacleManager Instance { get { return instance; } }

    [SerializeField]
    private GameObject Obstacle;
    [SerializeField]
    private Transform[] PosArray;

    private List<GameObject> pooledObjects;
    private int poolCount;

    private bool isGameOn;

    private void Awake()
    {
        if(instance != null && instance !=this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        poolCount = 15;
        GameObject tmp;
        for(int i =0; i <poolCount; i++)
        {
            tmp = Instantiate(Obstacle, PosArray[0].position, Quaternion.identity);
            tmp.transform.parent = this.gameObject.transform;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        isGameOn = false;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOn)
        { 
            if(GameManager.Instance.timerInSec %5 == 0)
            {
                StartCoroutine(SpawnObject());
            }
        }
    }

    private IEnumerator SpawnObject()
    {
        GameObject spawnedObstacle = GetObstacleFromPool();
        if(spawnedObstacle != null)
        {
            spawnedObstacle.SetActive(true);
            int num = Random.Range(0, 3);
            spawnedObstacle.transform.position = PosArray[num].transform.position;
        }
        yield return new WaitForSecondsRealtime(5);

    }
    private GameObject GetObstacleFromPool()
    {
        for(int i = 0; i<poolCount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
            
        }
        return null;
    }

    public void DestroyAllObjects()
    {
        foreach(GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }
}

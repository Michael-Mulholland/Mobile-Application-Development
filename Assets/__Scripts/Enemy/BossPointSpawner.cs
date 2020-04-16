using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class BossPointSpawner : MonoBehaviour
{
    // == private fields ==

    [SerializeField] private Boss bossPrefab;
    [SerializeField] private float spawnDelay = 2.0f;
    [SerializeField] private float spawnInterval = 600.0f;

    private const string SPAWN_BOSS_METHOD = "SpawnOneEnemy";
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject bossParent;

    // events for telling the system enemy spawned
    public delegate void BossSpawned();
    public static event BossSpawned BossSpawnedEvent;
    private const string bossParentName = "EnemyParent";

    // == private methods ==
    private void Start()
    {
        bossParent = GameObject.Find(bossParentName);

        if(!bossParent)
        {
            bossParent = new GameObject(bossParentName);
        }
        // get the spawn points here
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        //SpawnEnemyWaves();
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        //EnableBossSpawning();
    }

    private void SpawnEnemyWaves()
    {
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        //InvokeRepeating("SpawnOneEnemy", 0f, 0.25f);
        InvokeRepeating(SPAWN_BOSS_METHOD, spawnDelay, spawnInterval);
    }

    // stack version
    private void SpawnOneEnemy()
    {
        if(spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var boss = Instantiate(bossPrefab, bossParent.transform);
        var sp = spawnStack.Pop();
        boss.transform.position = sp.transform.position;
        PublishOnBossSpawnedEvent();   // tell the system
    }

    // create my event to publish the fact that enemt spawned
    public void PublishOnBossSpawnedEvent()
    {
        BossSpawnedEvent?.Invoke();
    }

    // == public methods ==

    public void EnableBossSpawning()
    {
        InvokeRepeating(SPAWN_BOSS_METHOD, spawnDelay, spawnInterval);
    }

    public void DisableBossSpawning()
    {
        CancelInvoke(SPAWN_BOSS_METHOD);
    }
}
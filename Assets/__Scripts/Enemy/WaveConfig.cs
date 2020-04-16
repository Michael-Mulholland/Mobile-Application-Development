using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changed to ScriptableObject
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    // Private Data Fields
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private float enemySpeed = 5.0f;
    [SerializeField] private float spawnIntervar = 0.35f;
    [SerializeField] public int scoreValue = 10;
    [SerializeField] private int damageValue = 10;

    // Public Methods
    public Sprite GetEnemySprite() { return enemySprite; }
    public int GetEnemiesPerWave(){ return enemiesPerWave; }
    public float GetEnemySpeed(){ return enemySpeed; }
    public float GetSpawnInterval(){ return spawnIntervar; }
    public int GetScoreValue(){ return scoreValue; }
    public int GetDamageValue(){ return damageValue; }
}
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnAngleMax = 15f, offset;
    private float _halfWidth, _halfHeight;
    private float _randomAngle, _nextSpawnTime, _secondsBetweenSpawns;
    private Vector2[] _offsets;
    private Vector2 _randomSpawnPosition;
    public Vector2 spawnSizeMinMax, secondsBetweenSpawnsMinMax;
    



    // Start is called before the first frame update
    void Start()
    {
        _halfHeight = Camera.main.orthographicSize;
        _halfWidth = Camera.main.aspect * _halfHeight;
        spawnSizeMinMax = new Vector2(.1f,2f);
        secondsBetweenSpawnsMinMax = new Vector2(.3f, .7f);
        _offsets = new[]{new Vector2(-_halfWidth * 2, 0), new Vector2(0,0), new Vector2(_halfWidth * 2,0)};
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawnTime)
        {
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            _secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x,
                Difficulty.GetDifficultyPercent());

            _nextSpawnTime = Time.time + _secondsBetweenSpawns;
            _randomSpawnPosition = new Vector2(Random.Range(-_halfWidth, _halfWidth), (_halfHeight + spawnSize));
            _randomAngle = Random.Range(-spawnAngleMax, spawnAngleMax);


            for (int i = 0; i < 3; i++)
            {
                GameObject newBlock = Instantiate(enemyPrefab, _randomSpawnPosition + _offsets[i],
                    Quaternion.Euler(Vector3.forward * _randomAngle));
                newBlock.transform.localScale = Vector2.one * spawnSize;
                
            }
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFallingBlocks : MonoBehaviour {
    public float secondsBetweenSpawns = 1;
    public float spawnAngleMax;
    public GameObject fallingBlock;
    public Vector2 spawnSizeMinMax;

   
    float halfScreenHeightInWordUnits;
    float halfScreenWidthInWorldUnits;
    float nextSpawnTime;
    float spawnHeight;
    float spawnWidth;
    float speed = 10f;
    List<GameObject> fallingBlockList = new List<GameObject>();
    Vector2 screenHalfSizeWorldUnits;

    // Use this for initialization
    void Start () {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
            //Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), spawnHeight, 0);
            GameObject cube = (GameObject)Instantiate(fallingBlock, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            cube.transform.localScale = Vector2.one * spawnSize;
            //cube.transform.parent = transform;
            //fallingBlockList.Add(cube);

            //DestroyFallingBlocks(fallingBlockList);
        }
    }

    void DestroyFallingBlocks(List<GameObject> blockList)
    {
        for (int i = 0; i < blockList.Count; i++)
        {
            if (blockList[i].transform.position.y < -halfScreenHeightInWordUnits)
            {
                Destroy(blockList[i]);
                blockList.RemoveAt(i);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFallingBlocks : MonoBehaviour {
    public float secondsBetweenSpawns = 1;
    public GameObject fallingBlock;

   
    float halfScreenHeightInWordUnits;
    float halfScreenWidthInWorldUnits;
    float nextSpawnTime;
    float spawnHeight;
    float spawnWidth;
    float speed = 10f;
    List<GameObject> fallingBlockList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        halfScreenHeightInWordUnits = Camera.main.orthographicSize;
        halfScreenWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        spawnHeight = halfScreenHeightInWordUnits + transform.localScale.y;
        spawnWidth = halfScreenWidthInWorldUnits - transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawnTime)
        {
            print("Time is " + Time.time + " Next spawn time is " + nextSpawnTime);
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), spawnHeight, 0);
            GameObject cube = (GameObject)Instantiate(fallingBlock, spawnPosition, Quaternion.identity);
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

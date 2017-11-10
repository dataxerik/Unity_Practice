using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    float halfScreenHeightInWordUnits;
    public Vector2 speedMinMax;
    float speed;

    void Start()
    {
        halfScreenHeightInWordUnits = -Camera.main.orthographicSize - transform.localScale.y;
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);

        if (transform.position.y < halfScreenHeightInWordUnits)
        {
            Destroy(gameObject);
        }
    }
}

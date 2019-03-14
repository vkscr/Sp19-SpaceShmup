﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static public Main S;                                // A singleton for Main
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;              // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f; // # Enemies/second
    public float enemyDefaultPadding = 1.5f; // Padding for position
    private BoundsCheck bndCheck;
    void Awake()
    {
        S = this;
        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        // Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);                      // a
    }
    public void SpawnEnemy()
    {
        // Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);                     // b
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);     // c

        float enemyPadding = enemyDefaultPadding;                            // d
        if (go.GetComponent<BoundsCheck>() != null)
        {                        // e
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        // Set the initial position for the spawned Enemy                    // f
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;
        // Invoke SpawnEnemy() again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);                      // g
    }
    public void DelayedRestart(float delay)
    {
        // Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        // Reload _Scene_0 to restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Scene_0");
    }


    // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

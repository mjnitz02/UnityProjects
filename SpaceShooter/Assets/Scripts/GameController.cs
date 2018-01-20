using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int hazardCount;
    public float startWait;
    public float waveDelay;
    public float waveWait;
    public float spawnWait;
    public int waveCount;

    public bool gameOver;
    private bool restart;

    public Text stateText;
    public Text scoreText;
    public Text restartText;
    private int score;

    public GameObject hazard;
    public Vector3 spawnValues;

	// Use this for initialization
	void Start ()
    {
        restartText.text = "";
        stateText.text = "";

        gameOver = false;
        restart = false;

        waveCount = 1;
        score = 0;
        UpdateScore();

        StartCoroutine (SpawnWaves());
    }
	
	// Update is called once per frame
	void Update () {

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
		
	}

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private IEnumerator SpawnWaves()
    {
        stateText.text = "Ready Player One";
        yield return new WaitForSeconds(waveDelay);
        while (true)
        {
            stateText.text = "Wave " + waveCount;
            yield return new WaitForSeconds(waveWait);
            stateText.text = "";

            for (int i = 0; i < hazardCount + (waveCount * 2); i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            waveCount++;

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void GameOver()
    {
        stateText.text = "Game Over";
        gameOver = true;
    }
}

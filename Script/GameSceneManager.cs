using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

float enemyIntervalMax = 2f;
float enemyIntervalMin = 0.2f;
float timeToMinimumInterval = 30f;

public Camera mainCamera;
public Text scoreText;
public Text gameOverText;

public ShipControl ship;
public GameObject enemyPrefab;

Vector3 leftBound;
Vector3 rightBound;

int score = 0;

float gameTimer;
float enemyTimer;
bool gameOver;

	void Start () {
		//decides how fast the game will go
		Time.timeScale = 1;

		gameOverText.enabled = false;

		enemyTimer = enemyIntervalMax;

        leftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, -mainCamera.transform.localPosition.z));
        rightBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.localPosition.z));

        ship.OnHitEnemy += OnGameOver;
	}
	
	void Update () {
        gameOverLogic();
        timerLogic();
	}

	void gameOverLogic(){
		if(gameOver){
			if (Input.GetKeyDown("r")){
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			scoreText.enabled = false;
			gameOverText.enabled = true;

			gameOverText.text = "Game Over! Total Score: "+score+ "\n Press R to restart";
			return;
		}
	}
	void timerLogic(){
		gameTimer+= Time.deltaTime;
		enemyTimer-=Time.deltaTime;

		if(enemyTimer <= 0){
			float intervalPercentage = Mathf.Min(gameTimer/timeToMinimumInterval);

			enemyTimer = enemyIntervalMax - (enemyIntervalMax - enemyIntervalMin)*intervalPercentage;
			GameObject enemy = GameObject.Instantiate<GameObject>(enemyPrefab);

			enemy.transform.position = new Vector3(Random.Range(leftBound.x, leftBound.y), leftBound.y+2,0);

			enemy.GetComponent<EnemyController>().OnKill += OnKillEnemy;
	}
	
}
public void OnKillEnemy(){
		score = score+100;
		scoreText.text = "Score: "+ score;
		
	}

public void OnGameOver(){
	gameOver = true;
	Time.timeScale = 0;
}
}
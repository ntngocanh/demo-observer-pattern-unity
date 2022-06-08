using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    // needed for spawning
	[SerializeField]
	GameObject prefabStar;
    // spawn control
	Timer spawnTimer;
    // spawn location support
	Vector3 location = new Vector3();
	int minSpawnX;
	int maxSpawnX;
	int minSpawnY;
	int maxSpawnY;

    // collision-free spawn support
	const int MaxSpawnTries = 20;
	float teddyBearColliderHalfWidth;
	float teddyBearColliderHalfHeight;
	Vector2 min = new Vector2();
	Vector2 max = new Vector2();
    // resolution support
	const int BaseWidth = 800;
	const int BaseHeight = 600;
	const int BaseBorderSize = 100;
    // Start is called before the first frame update
    void Start()
    {
        float widthRatio = (float)Screen.width / BaseWidth;
		float heightRatio = (float)Screen.height / BaseHeight;
		float resolutionRatio = (widthRatio + heightRatio) / 2;
		int spawnBorderSize = (int)(BaseBorderSize * resolutionRatio);

		// save spawn boundaries for efficiency
		minSpawnX = spawnBorderSize;
		maxSpawnX = Screen.width - spawnBorderSize;
		minSpawnY = spawnBorderSize;
		maxSpawnY = Screen.height - spawnBorderSize;

		// spawn and destroy a bear to cache collider values
		GameObject tempBear = Instantiate(prefabStar) as GameObject;
		CircleCollider2D collider = tempBear.GetComponent<CircleCollider2D>();
		teddyBearColliderHalfWidth = collider.radius;
		teddyBearColliderHalfHeight = collider.radius;
		Destroy(tempBear);

        // create and start timer
		spawnTimer = gameObject.AddComponent<Timer>();
		spawnTimer.AddTimerFinishedEventListener(HandleSpawnTimerFinishedEvent);
		StartRandomTimer();
    }
    /// <summary>
	/// Handles the spawn timer finished event
	/// </summary>
	private void HandleSpawnTimerFinishedEvent()
    {
		// only spawn a bear if below max number
		if (GameObject.FindGameObjectsWithTag("Star").Length < 5)
        {
			SpawnBear();
		}

		// change spawn timer duration and restart
		StartRandomTimer();
	}
    void StartRandomTimer()
    {
		spawnTimer.Duration = Random.Range(1, 3);
		spawnTimer.Run();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnBear()
    {		
		// generate random location and calculate teddy bear collision rectangle
		location.x = Random.Range(minSpawnX, maxSpawnX);
		location.y = Random.Range(minSpawnY, maxSpawnY);
		location.z = -Camera.main.transform.position.z;
		Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
		SetMinAndMax(worldLocation);

		// make sure we don't spawn into a collision
		int spawnTries = 1;
		while (Physics2D.OverlapArea(min, max) != null &&
			spawnTries < MaxSpawnTries)
        {
			// change location and calculate new rectangle points
			location.x = Random.Range(minSpawnX, maxSpawnX);
			location.y = Random.Range(minSpawnY, maxSpawnY);
			worldLocation = Camera.main.ScreenToWorldPoint(location);
			SetMinAndMax(worldLocation);

			spawnTries++;
		}

		// create new bear if found collision-free location
		if (Physics2D.OverlapArea(min, max) == null)
        {
			GameObject teddyBear = Instantiate(prefabStar) as GameObject;
			teddyBear.transform.position = worldLocation;
		}
	}
    void SetMinAndMax(Vector3 location)
    {
		min.x = location.x - teddyBearColliderHalfWidth;
		min.y = location.y - teddyBearColliderHalfHeight;
		max.x = location.x + teddyBearColliderHalfWidth;
		max.y = location.y + teddyBearColliderHalfHeight;
	}
}

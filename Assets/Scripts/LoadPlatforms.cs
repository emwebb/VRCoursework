using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlatforms : MonoBehaviour {

	public float startX = -5;
	public float startY = -10;
	public float startZ = -5;
	public float endX = 5;
	public float endY = 10;
	public float endZ = 10;

	public float jumpArcHeightMaximum = 10;
	public float jumpArcWidthMaximum = 10;

	public int platformsToGenerate = 10;

	public float minimumXDistanceShift = 1;
	
	public float maximumXDistanceShift = 6;

	public int GenerationMargin = 1;

	public float angleBounds = 360;

	public Transform platform;

	public Transform winnerPlatform;

	public Transform player;

	// Use this for initialization
	void Start () {
		List<Vector3> platforms = new List<Vector3>();

		platforms.Add(new Vector3(0, -5, 0));
		for (int i = 0; i < platformsToGenerate; i++) {
			int selector = platforms.Count - Random.Range(0, GenerationMargin); 
			selector = selector >= platforms.Count ?  platforms.Count - 1 : selector < 0 ? 0 : selector;
			
			Vector3 offset = generateCubeRelativeLocation();
			Vector3 target = generateOffsetVector(platforms[selector], offset, Random.Range(0, angleBounds));
			platforms.Add(target);
		}

		Vector3 winner = platforms[platforms.Count - 1];
		platforms.RemoveAt(platforms.Count - 1);

		Instantiate(winnerPlatform, winner, Quaternion.identity);

		foreach (var platformVector in platforms) {
			Instantiate(platform, platformVector, Quaternion.identity);
		}

		Vector3 heading = new Vector3(winner.x, player.position.y, winner.z);
		player.transform.LookAt(heading);
	}

	Vector3 generateCubeRelativeLocation() {
		float w = Random.Range(jumpArcWidthMaximum / 2, jumpArcWidthMaximum);
		float h = jumpArcHeightMaximum;
		float a = - (4*h) / (w * w);
		float b = (4*h) / w;

		float minx = (jumpArcWidthMaximum / 3) + minimumXDistanceShift;
		float maxx = minx + maximumXDistanceShift;

		float x = Random.Range(minx, maxx);

		float y = a*x*x + b*x;

		return new Vector3(x, y, 0);
	}

	Vector3 generateOffsetVector(Vector3 start, Vector3 offset, float angle) {
		return start + Quaternion.Euler(0, angle, 0) * offset;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

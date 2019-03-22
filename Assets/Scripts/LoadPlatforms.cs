using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Generates the environment in which the game is played. Placing all platforms
/// into locations that can be reached by jumping.
public class LoadPlatforms : MonoBehaviour {

	/// The maximum height a player can jump to.
	public float jumpArcHeightMaximum = 4;

	/// The maximum distance a player can travel before being beneath their 
	/// starting elevation.
	public float jumpArcWidthMaximum = 50;

	/// The number of platforms to generate
	public int platformsToGenerate = 30;

	/// The minimum distance to add to the jump parabola mid point when
	/// generating.
	public float minimumXDistanceShift = 0;

	/// The maximum distance to add to the jump parabola mid point when 
	/// generating.
	public float maximumXDistanceShift = 50;

	/// The number of generated platforms to use to determine the next platform
	/// locations.
	public int GenerationMargin = 3;

	/// The angle boundry within which platforms can be generated.
	public float angleBounds = 180;

	/// The platform prefab to clone.
	public Transform platform;

	/// The winner platform prefab to clone.
	public Transform winnerPlatform;

	/// The player object.
	public Transform player;

	/// The location of the winning platform (stored for resetting. See 
	/// <c>Reload</c>)
	private Vector3 winner;

	// Initialize the platform loader.
	void Start () {
		/// Add the initial platform beneath the player
		List<Vector3> platforms = new List<Vector3>();
		platforms.Add(new Vector3(0, -5, 0));
		/// Until the target number of platforms is reached, look at a number of
		/// the previous platforms, and generate a new platform reachable from
		/// it.
		for (int i = 0; i < platformsToGenerate; i++) {
			int selector = platforms.Count - Random.Range(0, GenerationMargin); 
			/// Clamp the selector to within bounds.
			selector = (selector >= platforms.Count) 
				? platforms.Count - 1 
				: (selector < 0)
				? 0
				: selector;
			Vector3 offset = generateCubeRelativeLocation();
			Vector3 target = generateOffsetVector(platforms[selector], offset, Random.Range(0, angleBounds));
			platforms.Add(target);
		}
		// Remove and process the winning platform (the final one generated)
		winner = platforms[platforms.Count - 1];
		platforms.RemoveAt(platforms.Count - 1);
		Instantiate(winnerPlatform, winner, Quaternion.identity);

		// Generate each platform.
		foreach (var platformVector in platforms) {
			Instantiate(platform, platformVector, Quaternion.identity);
		}

		// Adjust the players vision to look towards their end goal.
		Vector3 heading = new Vector3(winner.x, player.position.y, winner.z);
		player.transform.LookAt(heading);
	}

	/// On reload, the player has their vision re-adjusted to look back at the
	/// victory condition.
	public void Reload () {
		Vector3 heading = new Vector3(winner.x, player.position.y, winner.z);
		player.transform.LookAt(heading);	
	}

	/// Based on the formula for a parabola from it's fixed point, and the
	/// distance between two y=0 values with one at x=0. Determine an offset
	/// that is plausibly reachable by the jumper.
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

	/// Convert the generated offset vector into an actual location based on
	/// the selected platform to jump from.
	Vector3 generateOffsetVector(Vector3 start, Vector3 offset, float angle) {
		return start + Quaternion.Euler(0, angle, 0) * offset;
	}
	
}

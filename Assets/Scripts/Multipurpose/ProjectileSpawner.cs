using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

	public float fireRate, distanceFactor;
	public GameObject[] projectiles;

	private float nextFire;
	private int numSides;
	private Transform tf;

	void Start () {
		nextFire = Time.time + fireRate;
		numSides = projectiles.Length;
		tf = GetComponent<Transform> ();
	}

	void Update () {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Attack ();
		}
	}

	void Attack() {

		//Script attached to an empty child
		//start vector is tf.up, staying the same as tf is rotated around

		tf.localRotation = Quaternion.identity;
		Vector3 spawnPoint;
		Vector3 deltaRotation = new Vector3 (0, 0, 360 / numSides);

		for (int i = 0; i < numSides; i++) {
			spawnPoint = tf.position + (distanceFactor * tf.up.normalized);
			//firing sound
			Instantiate (projectiles[i], spawnPoint, tf.rotation);
			tf.Rotate (deltaRotation);
		}

	}


}

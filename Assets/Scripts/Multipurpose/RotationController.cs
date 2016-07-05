using UnityEngine;
using System.Collections;

// Controls the rotation for shapes and eggs
public class RotationController : MonoBehaviour {

	//dirchngrt is 5, rotrate is 150
	//public float rotationRate, dirChangeRate;

	private float nextDirChange, direction, rotationRate, dirChangeRate;
	private Transform tf;

	// Use this for initialization
	void Start () {
		setRotation (1f);
		tf = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		//change rotation direction and rate (or not?)
		if (Time.time > nextDirChange) {
			setRotation (-direction);
		}

		//rotate
		tf.Rotate (Vector3.forward * Time.deltaTime * rotationRate * direction);
	}

	void setRotation(float newDir) {
		rotationRate = Random.Range (100f, 165f);
		dirChangeRate = Random.Range (3f, 6.5f);
		nextDirChange = Time.time + dirChangeRate;
		direction = newDir;
	}

}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;

	}
	
	// use lateUpdate to be certain that player movement has already happened
	void LateUpdate () {
		if (player != null) {
			transform.position = player.transform.position + offset;
		}
	}
}

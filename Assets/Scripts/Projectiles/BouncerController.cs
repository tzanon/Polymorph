using UnityEngine;
using System.Collections;

public class BouncerController : MonoBehaviour {

	public int damage, bounceCount;

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.CompareTag ("Wall")) {
			if (bounceCount <= 0)
				Destroy (gameObject);
			else
				bounceCount--;
		} else
			Destroy (gameObject);
	}
}

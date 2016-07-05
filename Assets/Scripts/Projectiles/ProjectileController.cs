using UnityEngine;
using System.Collections;

// Controller for normal bullet-type projectiles
public class ProjectileController : MonoBehaviour {

	public int damage;

	void OnTriggerEnter2D(Collider2D coll) {
		Destroy (gameObject);
	}

}

using UnityEngine;
using System.Collections;

public class ShapeController : MonoBehaviour {

	public int damage, health;
	public string type;
	public AudioClip dmgSound, deathSound;

	private GameObject controller;
	//private AudioSource source;

	void Start() {
		controller = GameObject.FindGameObjectWithTag("GameController");
		controller.GetComponent<GameController> ().AddShape ();
        Debug.Log("Shape added");
		//source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag ("Wall") && !other.CompareTag ("Player")) {
			if (other.gameObject.CompareTag ("Projectile"))
				TakeDamage (other.gameObject.GetComponent<ProjectileController> ().damage);
			else if (other.gameObject.CompareTag ("Bouncy"))
				TakeDamage (other.gameObject.GetComponent<BouncerController> ().damage);
			else if (other.gameObject.CompareTag ("Egg"))
				TakeDamage (other.gameObject.GetComponent<EggController> ().damage);
			else if (other.gameObject.CompareTag ("Shape"))
				TakeDamage (other.gameObject.GetComponent<ShapeController> ().damage);
		}
	}

	void TakeDamage(int dmg) {
		if (health - dmg <= 0) {
			health = 0;
			//destroyed sound
			controller.GetComponent<GameController> ().RemoveShape();
			Destroy (gameObject);
		} else {
			health -= dmg;
			//dmg sound
		}
	}

}

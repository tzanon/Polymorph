using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float invulnerableTime;
	public float movementFactor;
	public float fireRate;
	public Text healthText;
	public GameObject Projectile;

	private int health;
	private float nextFire = 0.0f;
	private bool invulnerable;
	private Rigidbody2D rb;
	private Transform tf;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.freezeRotation = true;
		tf = GetComponent<Transform> ();
		health = 100;
		invulnerable = false;
		SetHealthText ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - tf.position);

		if (Input.GetKey (KeyCode.Mouse0) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Attack ();
		}
	}

	// use FixedUpdate() when dealing with forces
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb.velocity = movementFactor * movement;
		//Debug.Log ("velocity is " + rb.velocity);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (!invulnerable) {
			if (other.gameObject.CompareTag("Projectile"))
				TakeDamage(other.gameObject.GetComponent<ProjectileController> ().damage);
			else if (other.gameObject.CompareTag("Bouncy"))
				TakeDamage(other.gameObject.GetComponent<BouncerController> ().damage);
			else if (other.gameObject.CompareTag("Egg"))
				TakeDamage(other.gameObject.GetComponent<EggController> ().damage);
			else if (other.gameObject.CompareTag("Shape"))
				TakeDamage(other.gameObject.GetComponent<ShapeController> ().damage);
		}
	}

	void TakeDamage(int dmg) {
		if (health - dmg <= 0) {
			health = 0;
			Destroy (gameObject);
		} else {
			StartCoroutine (Invulnerable ());
			health -= dmg;
		}
		SetHealthText ();
	}

	// Creates a bullet GameObject
	void Attack() {
		Quaternion rotation = tf.rotation;
		Vector3 start = tf.position;
		start += 1.05f * tf.up.normalized;
		Instantiate (Projectile, start, rotation);
	}

	void SetHealthText() {
		healthText.text = "Health: " + health.ToString ();
	}

	IEnumerator Invulnerable () {
		invulnerable = true;
		yield return new WaitForSeconds (invulnerableTime);
		invulnerable = false;
	}

}

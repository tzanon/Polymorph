using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float invulnerableTime;
	public float movementFactor;
	public float fireRate;
	public Text healthText;
	public GameObject Projectile;
	public AudioClip fireSound, dmgSound, deathSound;

	private int health, ammo, magSize;
	private float nextFire = 0.0f;
	private bool invulnerable;
	private Rigidbody2D rb;
	private Transform tf;
	//private AudioSource source;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.freezeRotation = true;
		tf = GetComponent<Transform> ();
		//source = GetComponent<AudioSource> ();
		health = 100;
		ammo = 20;
		magSize = 20;
		invulnerable = false;
		SetHealthText ();
	}
	
	// Update is called once per frame
	void Update () {
		//float newAngle = Mathf.Atan ((Input.mousePosition.x - tf.position.x) / (Input.mousePosition.y - tf.position.y));
		//tf.rotation = (0f, 0f, Mathf.Rad2Deg * newAngle);

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - tf.position);

		//Debug.Log ("Mouse position is: " + (Input.mousePosition - tf.position));

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
			//play killed sound
		} else {
			StartCoroutine (Invulnerable ());
			health -= dmg;
			//play dmg sound
		}
		SetHealthText ();
	}

	// Creates a bullet GameObject
	void Attack() {
		Quaternion rotation = tf.rotation;
		Vector3 start = tf.position;
		start += 1.05f * tf.up.normalized;
		Instantiate (Projectile, start, rotation);
		//play fire sound
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

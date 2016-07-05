using UnityEngine;
using System.Collections;

public class EggController : MonoBehaviour {

	public int damage;
	public float eggTime, phaseDelay;
	public Sprite shapeSprite;
	public GameObject shape;

	private bool isTransitioning;
	private float transitionTime;
	private Transform tf;
	private SpriteRenderer sr;
	private Sprite egg;

	void Start () {
		isTransitioning = false;
		transitionTime = Time.time + eggTime;
		tf = GetComponent<Transform> ();
		sr = GetComponent<SpriteRenderer> ();
		egg = sr.sprite;
	}

	void Update() {
		if (Time.time > transitionTime && !isTransitioning)
			StartCoroutine(Transition ());
	}

	IEnumerator Transition () {
		isTransitioning = true;
		//Debug.Log ("Current sprite is " + sr.sprite.ToString());

		for (int i = 0; i < 5; i++) {
			sr.sprite = shapeSprite;
			yield return new WaitForSeconds (phaseDelay);
			sr.sprite = egg;
			yield return new WaitForSeconds (phaseDelay);
		}
		Instantiate (shape, tf.position, tf.rotation);
		//play 'hatch' sound?
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag ("Wall")) {
			//play a destroyed sound
			Destroy (gameObject);
		}
	}
}

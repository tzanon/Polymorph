using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text shapesText, winText;

	private int numberShapes;

	// Use this for initialization
	void Awake () {
        numberShapes = 0;
		SetShapesText ();
		winText.text = "";
        Debug.Log("Controller set");
    }
	
	public void AddShape() {
		numberShapes++;
		SetShapesText ();
	}

	public void RemoveShape() {
		numberShapes--;
		SetShapesText ();
		if (numberShapes <= 0) {
			winText.text = "YOU WON";
		}
	}

	void SetShapesText() {
		shapesText.text = "Shapes: " + numberShapes.ToString();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTrigger : MonoBehaviour
{
	public BoxCollider2D houseCollider;
	public PolygonCollider2D confinerCollider;
	public LevelSelectionManager manager;


	void Start() {
		houseCollider = GetComponent<BoxCollider2D>();
        confinerCollider = GameObject.Find("Confiner").GetComponent<PolygonCollider2D>();
		manager = FindObjectOfType<LevelSelectionManager>();
    }

	private void OnTriggerEnter2D(Collider2D collider) {
		manager.SetChosenLevel(gameObject.name);
	}

	private void OnTriggerExit2D(Collider2D collider) {
		manager.SetChosenLevel("None");
	}

}

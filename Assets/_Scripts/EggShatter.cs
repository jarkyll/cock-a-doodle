using UnityEngine;
using System.Collections;

public class EggShatter : MonoBehaviour {

	[SerializeField] private GameObject yolk;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag != "Player") {
			Vector3 yolkPosition = other.collider.bounds.ClosestPoint (other.contacts[0].point);
			yolkPosition.y = other.collider.bounds.max.y;
			Quaternion yolkRotation = new Quaternion ();
			yolkRotation.SetLookRotation (new Vector3(1, 0, 0), other.contacts[0].normal);
			Instantiate (yolk, yolkPosition, yolkRotation);
			Destroy (gameObject);
		}
	}
}

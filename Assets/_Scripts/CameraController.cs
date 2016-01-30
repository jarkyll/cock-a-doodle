using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	[SerializeField] private float distance;
	[SerializeField] private float elevation;
	[SerializeField] private float maxPitch;
	[SerializeField] private float minPitch;
	[SerializeField] private float defaultPitch;
	[SerializeField] private float pitchRate;
	[SerializeField] private float delay;

	private Queue<Quaternion> previousPlayerRotations;
	private Queue<Vector3> previousPlayerPositions;
	private Transform player;
	private float pitch;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		previousPlayerRotations = new Queue<Quaternion> ();
		previousPlayerPositions = new Queue<Vector3> ();
		pitch = defaultPitch;
	}

	void FixedUpdate () {
		previousPlayerRotations.Enqueue (player.rotation);
		previousPlayerPositions.Enqueue (player.position);

		// Change camera pitch based on right stick input
		pitch += Input.GetAxis ("Pitch") * pitchRate * Time.fixedDeltaTime;
		pitch = Mathf.Clamp (pitch, minPitch, maxPitch);

		// Delay the camera rotation
		if (delay > 0) {
			delay -= Time.fixedDeltaTime;
		} else {
			transform.rotation = Quaternion.Euler (previousPlayerRotations.Dequeue().eulerAngles + new Vector3 (pitch, 0, 0));
			transform.position = previousPlayerPositions.Dequeue() - transform.forward * distance + new Vector3(0, elevation, 0);
		}
	}
}

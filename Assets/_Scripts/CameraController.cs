using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	[SerializeField] private float distance;
	[SerializeField] private float elevation;
	[SerializeField] private float maxPitch;
	[SerializeField] private float minPitch;
	[SerializeField] private float pitchRate;
	[SerializeField] private float delay;

	private Queue<Quaternion> previousPlayerRotations;
	private Transform player;
	private float pitch;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		previousPlayerRotations = new Queue<Quaternion> ();
	}



	void FixedUpdate () {
		previousPlayerRotations.Enqueue (player.rotation);

		pitch += Input.GetAxis ("Pitch") * pitchRate * Time.fixedDeltaTime;
		pitch = Mathf.Clamp (pitch, minPitch, maxPitch);

		if (delay > 0) {
			delay -= Time.deltaTime;
		} else {
			transform.rotation = Quaternion.Euler (previousPlayerRotations.Dequeue().eulerAngles + new Vector3 (pitch, 0, 0));
			transform.position = player.position - transform.forward * distance + new Vector3(0, elevation, 0);
		}
	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] private float distance;
	[SerializeField] private float maxPitch;
	[SerializeField] private float minPitch;
	[SerializeField] private float pitchRate;

	private Transform player;
	private float pitch;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void Update () {
		pitch += Input.GetAxis ("Pitch") * pitchRate * Time.deltaTime;
		pitch = Mathf.Clamp (pitch, minPitch, maxPitch);

		transform.rotation = Quaternion.Euler(player.rotation.eulerAngles + new Vector3(pitch, 0, 0));
		transform.position = player.position - transform.forward * distance;
	}
}

using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {
    public Light sun;
    public float secondsPerDay = 120f;
    [Range(0, 1)]
    public float currentTime = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;

    private float sunInitialIntensity;

	// Use this for initialization
	void Start () {
        sunInitialIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update () {
        sun.transform.rotation = Quaternion.Euler((currentTime * 360f) - 90, 180, 0);    // Rotates the sun

        updateIntensity();

        currentTime += (Time.deltaTime / secondsPerDay) * timeMultiplier;                // Updates the currentTime
        currentTime %= 1;
	}

    void updateIntensity () {
        float intensityMultiplier = 1f;

        // Night time
        if (currentTime <= 0.23f || currentTime >= 0.75f) {
            intensityMultiplier = 0;
        // Sunrise
        } else if (currentTime <= 0.25f) {
            intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
        // Sunset
        } else if (currentTime >= 0.73f) {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}

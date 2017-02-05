using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : MonoBehaviour {

    bool splashTriggerActive = true;

    Color CurrentColor;

    public Light plight;
    public ParticleSystem tailParticles;
    public GameObject splash;

    ParticleSystem[] splashParticles;

    Material currentMaterial;

    IEnumerator startGravity() {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator resetSplash() {
        splashTriggerActive = false;
        yield return new WaitForSeconds(8f);
        splash.SetActive(false);
        splashTriggerActive = true;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(startGravity());

        CurrentColor = Color.HSVToRGB(Random.Range(0f,1f), 1, 1);
        print(CurrentColor);
        currentMaterial = transform.GetComponent<Renderer>().material;
        currentMaterial.SetColor("_ColorTint", CurrentColor); 
        currentMaterial.SetColor("_RimColor", CurrentColor);

        plight.color = CurrentColor;

        ParticleSystem.MainModule tailMain = tailParticles.main;
        tailMain.startColor = CurrentColor;

        splashParticles = splash.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem psystem in splashParticles) {
            ParticleSystem.MainModule pmain = psystem.main;
            pmain.startColor = CurrentColor;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (splashTriggerActive) {
            foreach (ContactPoint contact in collision.contacts) {
                if (contact.otherCollider.gameObject.tag == "ground") {
                    splash.SetActive(true);
                    StartCoroutine(resetSplash());
                }
            }
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}

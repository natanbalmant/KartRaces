using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class Kart : MonoBehaviour
{
    public string racerName;
    float timer;
    public int totalCheckpoints;
    public float lastTime;
    GameObject lastCheckpoint;

    public GameObject powerUpFx;

    public float powerupSpeed = 15;
    public float powerupTime = 5;
    public Renderer kartRenderer;
    Color kartDefaultColor;
    bool isPowerupOn;
    ArcadeKart arcadeKart;
    float defaultSpeed;
    // Start is called before the first frame update
    void Start()
    {
        kartDefaultColor = kartRenderer.material.color;
        arcadeKart = GetComponent<ArcadeKart>();
        defaultSpeed = arcadeKart.baseStats.TopSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Powerup")){
            Destroy(other.gameObject);
            StartCoroutine(ActivatePowerUp());
        }
        else if(other.CompareTag("Checkpoint")){
            if(other.gameObject == lastCheckpoint)
                return;

            lastCheckpoint = other.gameObject;
            totalCheckpoints++;
            lastTime = timer;
        }
        ArcadeKart otherKart = other.GetComponent<ArcadeKart>();
        if(otherKart != null && isPowerupOn){
            otherKart.ChangeSpeed(5);
        }
    }

    IEnumerator ActivatePowerUp(){
        Instantiate(powerUpFx, transform.position, Quaternion.identity);
        arcadeKart.baseStats.TopSpeed = powerupSpeed;
        isPowerupOn = true;
        float powerupTimer = 0;
        while(powerupTimer <= powerupTime){
            powerupTimer += 0.1f;
            kartRenderer.material.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));

            yield return new WaitForSeconds(0.1f);
        }
        arcadeKart.baseStats.TopSpeed = defaultSpeed;
        kartRenderer.material.color = kartDefaultColor;
        isPowerupOn = false;
    }
}

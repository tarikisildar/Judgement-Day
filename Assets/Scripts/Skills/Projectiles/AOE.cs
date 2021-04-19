using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public class AOE : MonoBehaviour
{
    public float lifeDuration = 1f;
    private float lifeTimer;

    public float damage = 3f;
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0f) {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("smt");
        if(other.gameObject.GetComponent<CharacterStats>() != null && other.gameObject.GetComponent<PlayerController>() == null) {
            other.gameObject.GetComponent<CharacterStats>().health -= damage;
        }
    }
}

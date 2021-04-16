using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 40f;
    public float lifeDuration = 3f;
    public float damage = 3f;
    private float lifeTimer;
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if(lifeTimer<=0f) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<CharacterStats>() != null) {
            other.gameObject.GetComponent<CharacterStats>().health -= 10;
        }
    }
}

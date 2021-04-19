using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 40f;
    public float lifeDuration = 3f;
    public float damage = 3f;
    private float lifeTimer;
    public float power = 10f;
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

    void KnockBack(Collision other)
    {
        Debug.Log("KNOCKBACKED");
        other.transform.GetComponent<Rigidbody>().AddForce(power * transform.forward );
        //other.gameObject.transform.DOMove(other.transform.position + transform.forward , 1f);
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<CharacterStats>() != null) {
            other.gameObject.GetComponent<CharacterStats>().health -= damage;
            KnockBack(other);
            Destroy(this.gameObject);
        }
    }

    
}

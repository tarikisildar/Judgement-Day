using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider slider;
    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarUI.GetComponent<RectTransform>().LookAt(healthBarUI.transform.position + Camera.main.transform.rotation * Vector3.back,
                                                        Camera.main.transform.rotation * Vector3.up);
        
        slider.value = CalculateHealth();

        if(health < maxHealth) {
            healthBarUI.SetActive(true);
        }

        if(health <= 0) {
            Destroy(gameObject);
        }

        if(health > maxHealth) {
            health = maxHealth;
        }
    }
    float CalculateHealth() 
    {
        return health / maxHealth;
    }
}

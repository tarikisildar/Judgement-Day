using System.Collections;
using System.Collections.Generic;
using Controllers.Character;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Controllers.CharacterController;

public class CharacterStats : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxHealth;
    public float critChance = 0.1f;

    public GameObject grave;
    public Animator animator;
    public GameObject healthBarUI;
    public Slider slider;
    
    private RectTransform rectTransform;
    private Camera camera;
    private bool isDead = false;
    
    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
        rectTransform = healthBarUI.GetComponent<RectTransform>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        Vector3 lookPosition = healthBarUI.transform.position + camera.transform.rotation * Vector3.back;
        Vector3 worldUp = camera.transform.rotation * Vector3.up;
        rectTransform.LookAt(lookPosition,worldUp);
        
        slider.value = CalculateHealth();

        if(health < maxHealth) {
            healthBarUI.SetActive(true);
        }

        if(health <= 0) {
            Die();
        }

        if(health > maxHealth) {
            health = maxHealth;
        }
    }
    float CalculateHealth() 
    {
        return health / maxHealth;
    }

    public void TakeDamage(int damageAmount,bool isCrit = false)
    {
        health -= isCrit ? damageAmount*2 : damageAmount;
        DamagePopup.Create(transform.position+Vector3.up/2, damageAmount,isCrit);
    }

    private void Die()
    {
        Destroy(GetComponent<CharacterController>());
        
        animator.SetTrigger(Constants.CharacterDieTrigger);
        isDead = true;
        StartCoroutine(DestroyThis(2f));

    }

    IEnumerator DestroyThis(float wTime)
    {
        yield return new WaitForSeconds(wTime);
        
        GameObject graveObj = Instantiate(grave,transform.position,transform.rotation);
        graveObj.transform.position = new Vector3(transform.position.x,0.1f,transform.position.z);

        Destroy(gameObject);
        
    }
}

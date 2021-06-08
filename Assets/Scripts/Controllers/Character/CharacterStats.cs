using System.Collections;
using System.Collections.Generic;
using Controllers.Character;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Controllers.CharacterController;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class CharacterStats : MonoBehaviourPun
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
        if (isDead) return;
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

    [PunRPC]
    public void TakeDamageFromClient(int damageAmount,int shooterId, bool isCrit = false)
    {
        if(shooterId == 0) return;
        var shooter = PhotonView.Find(shooterId).Owner; 
        TakeDamage(damageAmount, shooter, isCrit);
        //photonView.RPC("TakeDamage", RpcTarget.AllViaServer, damageAmount, isCrit);
    }

    void TakeDamage(int damageAmount,Player shooter,bool isCrit = false)
    {

        health -= isCrit ? damageAmount * 2 : damageAmount;
        if (shooter.UserId != photonView.Owner.UserId)
        {
            shooter.AddScore(isCrit ? damageAmount * 2 : damageAmount);
            if(health < 0){shooter.AddScore(100);}
        }
        else
        {
            if(health < 0){photonView.Owner.AddScore(-50);}

        }
            
        DamagePopup.Create(transform.position + Vector3.up / 2, damageAmount, isCrit);
    }

    protected virtual void Die()
    {
        if(photonView.IsMine)
        {
            Destroy(GetComponent<CharacterController>());
            StartCoroutine(DestroyThis(2f));
            animator.SetTrigger(Constants.CharacterDieTrigger);
            isDead = true;
            GameManager.Instance.PlayerDied();
        }
    }

    protected virtual IEnumerator DestroyThis(float wTime)
    {
        yield return new WaitForSeconds(wTime);

        photonView.RPC("createGrave", RpcTarget.All);
        //GameObject graveObj = Instantiate(grave,transform.position,transform.rotation);
        //graveObj.transform.position = new Vector3(transform.position.x,0.1f,transform.position.z);

        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }

    }

    [PunRPC]
    void createGrave()
    {
        if(photonView.IsMine)
        {
            GameObject graveObj = PhotonNetwork.Instantiate(Constants.UniversePath + grave.name, new Vector3(transform.position.x, 0.1f, transform.position.z), transform.rotation);
            //graveObj.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            //photonView.RPC("setParentSkill", RpcTarget.All, graveObj.GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    void setParentSkill(int id)
    {
        PhotonView pv = PhotonView.Find(id);
        pv.transform.SetParent(transform);
    }

    [PunRPC]
    void KnockBack(int id, Vector3 rotation, float power)
    {
        Debug.Log("KNOCKBACKED");
        var other = PhotonView.Find(id).gameObject;
        other.transform.GetComponent<Rigidbody>().AddForce(power * rotation);
        //other.gameObject.transform.DOMove(other.transform.position + transform.forward , 1f);
    }
}

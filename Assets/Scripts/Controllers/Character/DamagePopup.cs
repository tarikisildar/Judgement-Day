using System;
using TMPro;
using UnityEngine;

namespace Controllers.Character
{
    public class DamagePopup : MonoBehaviour
    {
        private TextMeshPro text;
        private float moveSpeedY = 1f;
        private float dissapearTimer;
        private Color textColor;

        private void Awake()
        {
            text = GetComponent<TextMeshPro>();
        }

        public static DamagePopup Create(Vector3 position, int damageAmount,bool isCrit = false)
        {
            GameObject damagePopupPrefab = Resources.Load<GameObject>(Constants.DamagePopupPath);
            GameObject damagePopupGameObject = Instantiate(damagePopupPrefab, position, Quaternion.identity);
            DamagePopup popup = damagePopupGameObject.GetComponent<DamagePopup>();
            popup.Setup(damageAmount,isCrit);
            if (isCrit)
            {
                GameObject critObject = Instantiate(damagePopupPrefab, position+Vector3.up/4f, Quaternion.identity);
                critObject.GetComponent<DamagePopup>().Setup("Critical!");
            }
            return popup;

        } 

        public void Setup(int damageAmount,bool isCrit)
        {
            if (isCrit) damageAmount *= 2;
            text.text = damageAmount.ToString();
            textColor = isCrit ? Color.red : Constants.Gold;
            text.color = textColor;
            dissapearTimer = 0.5f;
        }

        public void Setup(string st)
        {
            text.text = st;
            dissapearTimer = 0.5f;
            textColor = Color.red;
            text.color = textColor;
        }

        private void Update()
        {
            transform.position += Vector3.up * (moveSpeedY * Time.deltaTime);
            dissapearTimer -= Time.deltaTime;
            if (dissapearTimer < 0)
            {
                textColor.a -= 3f * Time.deltaTime;
                text.color = textColor;
                if (textColor.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
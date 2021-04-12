using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5;
        private Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 input)
        {
            transform.position += new Vector3(input.x,0,input.y) * (speed * Time.deltaTime);
            
        }
    }
}
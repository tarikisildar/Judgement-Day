using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

public class FixedJoystick : Joystick
{
    protected PlayerController controller;

    protected override void Start()
    {
        base.Start();
        Initialize();
        
    }

    private void OnEnable()
    {
        Enable();
    }

    private void OnDisable()
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    private void Enable()
    {
        controller = SurroundingsManager.Instance.mainPlayer.GetComponent<PlayerController>();

    }

    protected virtual void Initialize()
    {

        //StartCoroutine(WaitForInitialize());
    }

    IEnumerator WaitForInitialize()
    {
        yield return new WaitForSeconds(Constants.FirstBatch);
        controller = SurroundingsManager.Instance.mainPlayer.GetComponent<PlayerController>();

    }
     protected virtual void Update()
    {
        if (InputManager.Instance.takeInput)
        {
            if( new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")) != Vector2.zero) return;
            controller.Move(input);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{

    public bool isLeftHand = false;
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
    public AnimateHandOnServerTrigger animateHand;
    public float detectionCooldown = 3f;
    public float currentCooldownTime = 0f;

    public float pinch;
    public float grip;
    public GameObject detector;

    // Start is called before the first frame update
    void Start()
    {
        currentCooldownTime = detectionCooldown;
        pinch = 0;
        grip = 0;
        detector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (triggerValue > 0.01f)
        {
            animateHand.TriggerHandAnimation(isLeftHand, triggerValue, "Trigger");
            if (currentCooldownTime >= detectionCooldown && triggerValue >= 0.80f)
            {
                //detector.enabled = true;
                detector.SetActive(true);
                currentCooldownTime = 0;
            } else
            {
                currentCooldownTime += Time.deltaTime;
            }
            //pinch = triggerValue;
            
        }

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        if (gripValue > 0.01f)
        {
            animateHand.TriggerHandAnimation(isLeftHand, gripValue, "Grip");
            grip = gripValue;
        }
        //handAnimator.SetFloat("Trigger", triggerValue);

        //float gripValue = gripAnimationAction.action.ReadValue<float>();
        //handAnimator.SetFloat("Grip", gripValue);

        //if(Input.GetKeyDown(KeyCode.X))
        //{
        //    Debug.Log("Sending Hello");
        //    animateHand.Hello();
        //}
    }

}

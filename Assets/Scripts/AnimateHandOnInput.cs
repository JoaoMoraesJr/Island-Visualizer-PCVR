using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
    public AnimateHandOnServerTrigger animateHand;

    [SerializeField]
    public bool isLeftHand = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (triggerValue > 0.01f)
        {
            animateHand.TriggerHandAnimation(isLeftHand, triggerValue, "Trigger");
        }

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        if (gripValue > 0.01f)
        {
            animateHand.TriggerHandAnimation(isLeftHand, gripValue, "Grip");
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

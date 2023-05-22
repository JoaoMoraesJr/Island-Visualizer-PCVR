using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{

    public bool isLeftHand = false;
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
    public AnimateHandOnServerTrigger animateHand;
    public float detectionCooldown = 3f;
    public float currentCooldownTime = 0f;
    public Transform followTarget;

    private XRBaseInteractor interactor;

    public GameObject detector;
    [SerializeField]
    private GameObject grabbedElement;
    // Start is called before the first frame update
    void Start()
    {
        currentCooldownTime = detectionCooldown;
        detector.SetActive(false);
        //interactor = controller.GetComponent<XRBaseInteractor>();
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
        }
        if (gripValue < 0.25f && grabbedElement != null)
        {
            Debug.Log("Stop Grabbing");
            grabbedElement.GetComponent<CapsuleCollider>().isTrigger = false;
            grabbedElement.GetComponent<FollowDelay>().target = grabbedElement.transform;
            grabbedElement = null;
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

    public void Select(SelectEnterEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.gameObject);
        if (grabbedElement == null)
        {
            Debug.Log("Grabbing");
            grabbedElement = args.interactableObject.transform.gameObject;
            grabbedElement.GetComponent<CapsuleCollider>().isTrigger = true; //Trigger to stop clipping on contact
            grabbedElement.GetComponent<FollowDelay>().target = followTarget.transform;
        }
    }

}

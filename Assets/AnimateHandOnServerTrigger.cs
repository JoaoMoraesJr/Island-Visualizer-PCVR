using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateHandOnServerTrigger : NetworkBehaviour
{

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void TriggerHandAnimation(bool isLeftHand, float value, string name)
    {
        TriggerHandClient(isLeftHand, value, name);
    }

    [ClientRpc]
    public void TriggerHandClient(bool isLeftHand, float value, string name)
    {
        Debug.Log(name + " " + value);
        if (isLeftHand)
        {
            leftHandAnimator.SetFloat(name, value);
        } else
        {
            rightHandAnimator.SetFloat(name, value);
        }
    }
}

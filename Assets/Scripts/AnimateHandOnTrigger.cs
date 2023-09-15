using UnityEngine;

public class AnimateHandOnTrigger : MonoBehaviour
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

    public void TriggerHandAnimation(bool isLeftHand, float value, string name)
    {
        TriggerHandClient(isLeftHand, value, name);
    }

    public void TriggerHandClient(bool isLeftHand, float value, string name)
    {
        if (isLeftHand)
        {
            leftHandAnimator.SetFloat(name, value);
        } else
        {
            rightHandAnimator.SetFloat(name, value);
        }
    }
}

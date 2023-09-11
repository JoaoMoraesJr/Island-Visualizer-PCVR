using UnityEngine;

public class HouseController : MonoBehaviour
{
    public GameObject[] windowGlassObjects;

    [ColorUsage(true, true)]
    public Color lightsOnEmissionColor;

    [ColorUsage(true, true)]
    public Color lightsOffEmissionColor;

    public bool lightStatus = false;
    private bool currentLightStatus = false;
    public AudioSource switchSound;

    private void Start()
    {
        changeLights();
    }

    private void Update()
    {
        if (currentLightStatus != lightStatus) 
        {
            currentLightStatus = lightStatus;
            changeLights();
            switchSound.Play();
        }
    }

    public void changeLights()
    {
        Color colorChange = lightsOffEmissionColor;
        if (currentLightStatus == true) colorChange = lightsOnEmissionColor;
        foreach (GameObject targetObject in windowGlassObjects)
        {
            Material targetMaterial = targetObject.GetComponent<Renderer>().material;
            // Set the new HDR color to the material's emission color property
            targetMaterial.SetColor("_EmissionColor", colorChange);

            // Enable emission on the material (if it's not already enabled)
            targetMaterial.EnableKeyword("_EMISSION");
        }
    }
}

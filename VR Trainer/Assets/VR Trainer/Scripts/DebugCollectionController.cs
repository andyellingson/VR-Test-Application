using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRStandardAssets.Utils;

public class DebugCollectionController : MonoBehaviour
{
    //this gameobject holds all other debugtext textmeshes
    public GameObject DebugCollection;
    public TextMeshPro DebugButtonText;
    public TextMeshPro VRInteractiveItemDebugText;
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private bool IsOn;

    private void OnEnable()
    {
        IsOn = false;
        DebugButtonText.text = "Debug Text: Off";
        m_InteractiveItem.OnDown += HandlePickUp;
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnDown -= HandlePickUp;
    }

    private void HandlePickUp()
    {
        VRInteractiveItemDebugText.text = "VRInteractiveItem: DebugCollectionController IsOn =" + IsOn;
        if(IsOn)
        {
            DebugCollection.SetActive(false);
            DebugButtonText.text = "Debug Text: Off";
            IsOn = false;
        }
        else
        {
            DebugCollection.SetActive(true);
            DebugButtonText.text = "Debug Text: On";
            IsOn = true;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

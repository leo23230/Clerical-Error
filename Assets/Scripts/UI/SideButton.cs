using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideButton : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject MainButton;
    private GameObject BackButton;
    private GameObject BackpackButton;
    private GameObject SpellcastingButton;
    private GameObject CraftingButton;
    private Button MainButtonUIComponent;
    private TextMeshProUGUI MainButtonText;
    private MainButton MainButtonComponent;

    private void OnEnable()
    {
        StaticEventHandler.BagRummageEvent += DeactivateActionButtons;
    }
    private void OnDisable()
    {
        StaticEventHandler.BagRummageEvent -= DeactivateActionButtons;
    }

    private void Awake()
    {
        MainButton = GameObject.Find("MainButton");
        BackButton = GameObject.Find("BackButton");
        BackpackButton = GameObject.Find("BackpackButton");
        SpellcastingButton = GameObject.Find("SpellcastingButton");
        CraftingButton = GameObject.Find("CraftingButton");
        if (BackButton.activeSelf) BackButton.SetActive(false);
        MainButtonUIComponent = MainButton.GetComponent<Button>();
        MainButtonComponent = MainButton.GetComponent<MainButton>();
        MainButtonText = MainButton.transform.Find("text").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMainButton()
    {
        MainButtonUIComponent.interactable = true;
    }

    public void MakeMainBackButton()
    {
        //EnableMainButton();
        BackButton.SetActive(true);
        //DeactivateActionButtons();
    }

    public void DisableMainButton()
    {
        MainButtonUIComponent.interactable = false;
    }

    public void DeactivateActionButtons(BagRummageEventArgs eventArgs)
    {
        BackpackButton.SetActive(false);
        SpellcastingButton.SetActive(false);
        CraftingButton.SetActive(false);
    }
}

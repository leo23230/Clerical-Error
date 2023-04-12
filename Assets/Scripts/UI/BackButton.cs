using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private GameObject MainButton;
    private MainButton MainButtonComponent;

    private void OnEnable()
    {
        StaticEventHandler.BagRummageEvent += ActivateSelf;
        StaticEventHandler.ExitMiniGameEvent += DeactivateSelf;
    }

    private void OnDisable()
    {
        StaticEventHandler.BagRummageEvent += ActivateSelf;
        StaticEventHandler.ExitMiniGameEvent += DeactivateSelf;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainButton = GameObject.Find("MainButton");
        MainButtonComponent = MainButton.GetComponent<MainButton>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisableButtonObject()
    {
        MainButton.SetActive(true);
        
    }

    public void DeactivateSelf(ExitMiniGameEventArgs eventArgs)
    {
        gameObject.SetActive(false);
    }

    public void ActivateSelf(BagRummageEventArgs eventArgs)
    {
        gameObject.SetActive(true);
    }
}

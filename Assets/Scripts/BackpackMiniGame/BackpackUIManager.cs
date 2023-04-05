using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackUIManager : MonoBehaviour
{

    private TextMeshProUGUI toolTipName;
    private TextMeshProUGUI toolTipDescription;
    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = transform.Find("TooltipPanel").gameObject;
        toolTipName = panel.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        toolTipDescription = panel.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();

        toolTipName.text = "";
        toolTipDescription.text = "";
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        StaticEventHandler.ItemSelectedEvent += UpdateToolTipToSelected;
        StaticEventHandler.ConsumableUsedEvent += ResetTooltipAfterUse;
    }

    private void OnDisable()
    {
        StaticEventHandler.ItemSelectedEvent -= UpdateToolTipToSelected;
        StaticEventHandler.ConsumableUsedEvent -= ResetTooltipAfterUse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateToolTipToSelected(ItemSelectedEventArgs eventArgs)
    {
        panel.SetActive(true);
        toolTipName.text = eventArgs.itemDetails.itemName;
        toolTipDescription.text = eventArgs.itemDetails.itemDescription;
    }
    void ResetTooltipAfterUse(ConsumableUsedEventArgs eventArgs)
    {
        ResetTooltip();
    }

    public void ResetTooltip()
    {
        panel.SetActive(false);
        toolTipName.text = "";
        toolTipDescription.text = "";
    }
}

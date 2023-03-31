using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackUIManager : MonoBehaviour
{

    private TextMeshProUGUI toolTip;

    // Start is called before the first frame update
    void Start()
    {
        toolTip = GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>();
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
        toolTip.text = eventArgs.itemDetails.itemName;
    }
    void ResetTooltipAfterUse(ConsumableUsedEventArgs eventArgs)
    {
        toolTip.text = "";
    }
}

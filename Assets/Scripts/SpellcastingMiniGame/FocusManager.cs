using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.UI;

public class FocusManager : MonoBehaviour
{
    private GameObject spellCircle;
    private List<Transform> focusTargets = new List<Transform>();
    public int targetNumber = 0;
    private ShapePopulator shapePopulator;
    private SimpleScrollSnap scrollSnap;
    private GameObject selectedPanel;
    private InkCounter inkCounter;
    private Inventory playerInventoryComponent;
    [HideInInspector]public List<string> selectedSpell = new List<string>();
    private Transform selectedRunesParentGroup;

    public GameObject spellReadyEffect;

    private void Awake()
    {
        spellCircle = GameObject.Find("SpellCircle");
        for (int i = 0; i < spellCircle.transform.childCount; i++)
            focusTargets.Add(spellCircle.transform.GetChild(i));
        shapePopulator = GameObject.Find("ShapePopulator").GetComponent<ShapePopulator>();
        scrollSnap = GetComponent<SimpleScrollSnap>();
        inkCounter = GameObject.Find("InkCounter").GetComponent<InkCounter>();
        playerInventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();
        selectedRunesParentGroup = GameObject.Find("SelectedRunes").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = focusTargets[0].transform.position;
        SetSelectedPanel(scrollSnap.CenteredPanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && targetNumber < 5)
        {
            Debug.Log(selectedPanel.name);
            if(selectedPanel.name == "Rune_ TargetEnemies(Clone)" || selectedPanel.name == "Rune_ TargetCharacters(Clone)")
            {
                inkCounter.UpdateTotalInkAmt(3);
            }
            else if (selectedPanel.name == "Rune_ Res(Clone)")
            {
                inkCounter.UpdateTotalInkAmt(6);
            }
            else
            {
                inkCounter.UpdateTotalInkAmt(1);
            }

            //First Instantiate the selected rune prefab
            //Add the rune's id to a list of rune id's
            //This List will be parsed to determine what spell it will turn into

            GameObject newPanel = Instantiate(selectedPanel);
            newPanel.transform.localPosition = transform.position;
            newPanel.transform.localScale = new Vector3(0.0156f, 0.0156f, 0);
            newPanel.GetComponent<Image>().enabled = false;
            
            newPanel.transform.SetParent(selectedRunesParentGroup);

            if(targetNumber < 5) targetNumber += 1;
            /*else
            {
                targetNumber = 0;
                for (int i = 0; i < selectedRunesParentGroup.childCount; i++)
                {
                    Destroy(selectedRunesParentGroup.GetChild(i).gameObject);
                }
            }*/

            if (targetNumber == 0) shapePopulator.changeShapeGroup(1);
            if (targetNumber == 1) shapePopulator.changeShapeGroup(2);

            //reset selected panel since the old panels have been deleted
            selectedPanel = scrollSnap.Panels[scrollSnap.CenteredPanel].gameObject;

            //if the target number is equal to 5, there is no other target so we need to disable the focus.
            //(OR JUST REMOVE THE CURRENT SHAPES)
            if(targetNumber == 5)
            {
                //disable
                shapePopulator.RemoveCurrentShapesFromScrollSnap(5);
            }
            else
            {
                transform.position = focusTargets[targetNumber].transform.position;
            }

            SetSelectedPanel(scrollSnap.CenteredPanel);

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Collect Children of SelectedRunes
            List<string> selectedRunes = new List<string>();
            Transform runesParent = GameObject.Find("SelectedRunes").transform;
            for(int i = 0; i < runesParent.childCount; i++)
            {
                string runeName = runesParent.GetChild(i).gameObject.name;
                string parsedRuneName = "";
                foreach(char c in runeName)
                {
                    if (c == '(') break;
                    parsedRuneName += c;
                }
                selectedRunes.Add(parsedRuneName);
                Destroy(runesParent.GetChild(i).gameObject);
            }

            selectedSpell = selectedRunes;
            playerInventoryComponent.SetPreparedSpell(selectedSpell);
            ResetFocus();
            spellReadyEffect.SetActive(true);
            //Call Reset Spell Circle Function
        }
    }

    public void SetSelectedPanel(int index)
    {
        selectedPanel = scrollSnap.Panels[scrollSnap.CenteredPanel].gameObject;
    }

    public void ResetShapeGroup()
    {
        targetNumber = 0;
        for (int i = 0; i < selectedRunesParentGroup.childCount; i++)
        {
            Destroy(selectedRunesParentGroup.GetChild(i).gameObject);
        }
        shapePopulator.changeShapeGroup(1);

    }

    public void ResetFocus()
    {
        ResetShapeGroup();
        selectedPanel = scrollSnap.Panels[scrollSnap.CenteredPanel].gameObject;

        transform.position = focusTargets[targetNumber].transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkCounter : MonoBehaviour
{
    public float maxInkAmtPerBottle = 3;

    [HideInInspector] public float totalInkAmt;

    [HideInInspector] public float totalInkMax;

    private List<GameObject> inkBottleObjects = new List<GameObject>();

    private void Awake()
    {
        //initalize list of ink bottle children (for now anyway)
        for(int i = 0; i < transform.childCount; i++)
        {
            inkBottleObjects.Add(transform.GetChild(i).gameObject);
        }

        totalInkMax = maxInkAmtPerBottle * inkBottleObjects.Count;
        totalInkAmt = totalInkMax;
    }

    public void UpdateTotalInkAmt(int amtToSubtract)
    {
        totalInkAmt -= amtToSubtract;
        UpdateInkBottleSprites();
    }

    public void UpdateInkBottleSprites()
    {
        int fullBottleCount = Mathf.FloorToInt(totalInkAmt / maxInkAmtPerBottle);
        float leftOverInk = totalInkAmt % maxInkAmtPerBottle;

        Debug.Log("fullBottleCount" + fullBottleCount.ToString());
        Debug.Log("leftOverInk" + leftOverInk.ToString());

        //the for loop is to make sure ALL bottles that aren't full get updated
        for (int i = fullBottleCount; i < inkBottleObjects.Count; i++)
        {
            //update first bottle to whatever level it is at 
            if (leftOverInk > 0)
            {
                InkBottle inkBottleToUpdate = inkBottleObjects[i].GetComponent<InkBottle>();
                inkBottleToUpdate.inkAmt = leftOverInk;
                leftOverInk = 0;
                inkBottleToUpdate.UpdateInkSprite();
            }
            //update the rest of the bott
            else
            {
                InkBottle inkBottleToUpdate = inkBottleObjects[i].GetComponent<InkBottle>();
                inkBottleToUpdate.inkAmt = 0;
                inkBottleToUpdate.UpdateInkSprite();
            }
        }
    }
    
}

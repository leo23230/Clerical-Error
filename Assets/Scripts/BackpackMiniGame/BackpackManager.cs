using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public List<GameObject> itemPrefabs = new List<GameObject>();
    private List<GameObject> backpackObjects = new List<GameObject>();
    
    private int maxItems = 24;
    private int maxLayerItems = 8;
    private const string bottomLayer = "BPBottom";
    private const string middleLayer = "BPMiddle";
    private const string topLayer = "BPTop";
    private const string bottomSortingLayer = "BPBottom";
    private const string middleSortingLayer = "BPMiddle";
    private const string topSortingLayer = "BPTop";

    void Start()
    {
        //temporary generate a list
        for (int i = 0; i < maxItems - 4; i++)
        {
            int rand = Mathf.RoundToInt(Random.Range(0f, (float)itemPrefabs.Count-1));

            //instantiate the prefab
            GameObject item = Instantiate(itemPrefabs[rand]);

            //get a random x and y for starting Position
            float randX = Random.Range(-8f, 8f);
            float randY = Random.Range(-4f, 4f);
            Vector3 startingPos = new Vector3(randX, randY, 0f);
            //set starting position
            item.transform.position = startingPos;

            if (i < maxLayerItems)
            {
                //set the item's layers to bottom
                item.layer = LayerMask.NameToLayer(bottomLayer);
                item.GetComponent<SpriteRenderer>().sortingLayerName = bottomSortingLayer;
            }
            else if (i < maxLayerItems * 2)
            {
                //set the item's layers to middle
                item.layer = LayerMask.NameToLayer(middleLayer);
                item.GetComponent<SpriteRenderer>().sortingLayerName = middleSortingLayer;
            }
            else
            {
                //set the item's layers to top
                item.layer = LayerMask.NameToLayer(topLayer);
                item.GetComponent<SpriteRenderer>().sortingLayerName = topSortingLayer;
            }

            //add reference to the list
            backpackObjects.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReorganizeItemsIntoLayers(GameObject selectedItem)
    {
        backpackObjects.Remove(selectedItem);
        backpackObjects.Add(selectedItem);
        for(int i = 0; i < backpackObjects.Count; i++)
        {
            if(i < maxLayerItems)
            {
                //set the item's layers to bottom
                backpackObjects[i].layer = LayerMask.NameToLayer(bottomLayer);
                backpackObjects[i].GetComponent<SpriteRenderer>().sortingLayerName = bottomSortingLayer;
            }
            else if (i < maxLayerItems * 2)
            {
                //set the item's layers to middle
                backpackObjects[i].layer = LayerMask.NameToLayer(middleLayer);
                backpackObjects[i].GetComponent<SpriteRenderer>().sortingLayerName = middleSortingLayer;
            }
            else
            {
                //set the item's layers to top
                backpackObjects[i].layer = LayerMask.NameToLayer(topLayer);
                backpackObjects[i].GetComponent<SpriteRenderer>().sortingLayerName = topSortingLayer;
            }
        }
    }
}

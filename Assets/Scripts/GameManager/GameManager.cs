using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CharacterDetailsSO> characters;
    public int numberOfCharacters;

    void Start()
    {
        generateCharacters();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            regenerateCharacters();
        }
    }

    void generateCharacters()
    {
        //get a list of 3 random Characters
        List<CharacterDetailsSO> chosenCharacters = chooseCharacters(numberOfCharacters);

        //spawn the characters
        for (var i = 0; i < chosenCharacters.Count; i++)
        {
            CharacterDetailsSO chosenCharacterDetails = chosenCharacters[i];
            var instance = Instantiate(chosenCharacterDetails.characterPrefab);
            var spawn = GameObject.Find("Spawn" + i.ToString());
            if (spawn != null) {
                //spawn in the player prefab
                instance.transform.position = spawn.transform.position;
                instance.GetComponent<Character>().Initialize(chosenCharacterDetails);
            } 
            else Debug.Log("No Spawn");
        }
    }

    List<CharacterDetailsSO> chooseCharacters(int amt)
    {
        List<CharacterDetailsSO> chosenCharacters = new List<CharacterDetailsSO>();
        for (var i = 0; i < amt; i++)
        {
            var chosenCharacter = characters[i];
            chosenCharacters.Add(chosenCharacter);
        }
        return chosenCharacters;
    }

    List<CharacterDetailsSO> chooseRandomCharacters(int amt)
    {
        //choose characters at random from list of characters
        List<CharacterDetailsSO> chosenCharacters = new List<CharacterDetailsSO>();
        for (var i = 0; i < amt; i++)
        {
            int rand = Mathf.RoundToInt(Random.Range(0, characters.Count));
            var chosenCharacter = characters[rand];
            chosenCharacters.Add(chosenCharacter);
        }
        return chosenCharacters;
    }

    //for testign purposes
    void regenerateCharacters()
    {
        var existingCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject character in existingCharacters)
        {
            Destroy(character);
        }
        generateCharacters();
    }
}

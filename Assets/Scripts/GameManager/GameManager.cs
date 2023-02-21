using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CharacterDetailsSO> characters;

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
        List<CharacterDetailsSO> chosenCharacters = chooseRandomCharacters(3);

        //spawn the characters
        for (var i = 0; i < chosenCharacters.Count; i++)
        {
            var instance = Instantiate(chosenCharacters[i].characterPrefab);
            var spawn = GameObject.Find("Spawn" + i.ToString());
            if (spawn != null) instance.transform.position = spawn.transform.position;
            else Debug.Log("No Spawn");
        }
    }

    List<CharacterDetailsSO> chooseRandomCharacters(int amt)
    {
        //choose characters at random from list of characters
        List<CharacterDetailsSO> chosenCharacters = new List<CharacterDetailsSO>();
        for (var i = 0; i < amt; i++)
        {
            var rand = Random.Range(0, characters.Count);
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

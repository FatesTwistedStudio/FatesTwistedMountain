using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_AISpawner : MonoBehaviour
{
    public S_CharacterDatabase S_CharacterDatabase;
    public GameObject AiCharacter;
    void Start()
    {
        selectCharacter();
        GameObject spawnCharacter = Instantiate(AiCharacter, transform.position, transform.rotation) as GameObject;
        spawnCharacter.GetComponent<PlayerInput>().enabled = false;
        spawnCharacter.tag = "Character";
        spawnCharacter.GetComponent<S_AiMovement>().enabled = true;
        //set AI mode

        //Debug.Log(spawnCharacter.tag);
    }
    public void selectCharacter()
    {
        if (AiCharacter == null)
        {
            int l = S_CharacterDatabase.characterInformation.Length;
            int n = Random.Range(0, l - 1);
            AiCharacter = S_CharacterDatabase.characterInformation[n].characterPrefab;
        }
    }
}

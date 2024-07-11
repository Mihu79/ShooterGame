using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollectCube : Interactable
{
   

    protected override void Interact()
    {
        Destroy(gameObject);
        GameOver();
    }
    private void GameOver()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}


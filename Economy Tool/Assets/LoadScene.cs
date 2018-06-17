using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TransitionScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if ( scene.name == "Advanced_resource")
        {
            SceneManager.LoadScene("Basic_resource", LoadSceneMode.Additive);
        }
       else if ( scene.name == "Basic_resource")
        { 
            SceneManager.LoadScene("Advanced_resource", LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Error loading the scene!");
        }

    }
}

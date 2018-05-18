using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public string level1Name;
    public string level2Name;
    public string level3Name;
    public string level4Name;
    public string levelHubName;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnLevel1()
    {
        SceneManager.LoadScene(level1Name);
    }

    public void OnLevel2()
    {
        SceneManager.LoadScene(level2Name);
    }

    public void OnLevel3()
    {
        SceneManager.LoadScene(level3Name);
    }

    public void OnLevel4()
    {
        SceneManager.LoadScene(level4Name);
    }

    public void OnLevelHub()
    {
        SceneManager.LoadScene(levelHubName);
    }
}

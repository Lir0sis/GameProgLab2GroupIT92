using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Update()
    {
        
    }
    public void ButtonBedroomScene()
    {
        SceneManager.LoadScene("BedroomScene");
    }
    public void ButtonExit()
    {
        Application.Quit();
    }
}

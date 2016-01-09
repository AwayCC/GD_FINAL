using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    public Canvas Mainmenu;
    public Canvas Pausemenu;
    public Canvas Quitmenu;
    public Button StartText;
    public Button ConfigText;
    public Button ExitText;
    // Use this for initialization
	void Start () {
        Mainmenu = Mainmenu.GetComponent<Canvas>();
        Pausemenu = Pausemenu.GetComponent<Canvas>();
        Quitmenu = Quitmenu.GetComponent<Canvas>();
        StartText = StartText.GetComponent<Button>();
        ConfigText = ConfigText.GetComponent<Button>();
        ExitText = ExitText.GetComponent<Button>();
        Quitmenu.enabled = false;
        Mainmenu.enabled = true;
	}
	public void ExitPress()
    {
        Mainmenu.enabled = false;
        Quitmenu.enabled = true;
        //StartText.enabled = false;
        ConfigText.enabled = false;
    }
    public void Exit_No_Press()
    {
        Mainmenu.enabled = true;
        Quitmenu.enabled = false;
        
    }
    public void PausePress()
    {
        Pausemenu.enabled=true;

    }
    public void ResumePress()
    {
        Pausemenu.enabled = false;
    }
    public void StartPress()
    {
        Mainmenu.enabled = false;
      
    }
    public void Exit_Yes_Press()
    {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
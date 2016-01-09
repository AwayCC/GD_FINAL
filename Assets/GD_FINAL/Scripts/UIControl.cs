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
    private bool FromMain=true;
    private bool playing = false;
    // Use this for initialization
	void Start () {
        Mainmenu = Mainmenu.GetComponent<Canvas>();
        Pausemenu = Pausemenu.GetComponent<Canvas>();
        Quitmenu = Quitmenu.GetComponent<Canvas>();
     //   StartText = StartText.GetComponent<Button>();
    //    ConfigText = ConfigText.GetComponent<Button>();
     //   ExitText = ExitText.GetComponent<Button>();
        Quitmenu.enabled = false;
        Mainmenu.enabled = true;
	}
    public void UIDebugging()
    {
        Debug.Log("The Action is Detected");
    }
    public void ExitPress()
    {
        Mainmenu.enabled = false;
        Quitmenu.enabled = true;
        FromMain = true;
        //StartText.enabled = false;
       // ConfigText.enabled = false;
    }
    public void Exit_No_Press()
    {
        if(FromMain)
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
        playing = true;
      
    }
    public void Exit_Yes_Press()
    {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
    
        if (Input.GetKey(KeyCode.Escape)&&playing)
        {
            FromMain = false;
            Quitmenu.enabled = true;
        }
    }
}
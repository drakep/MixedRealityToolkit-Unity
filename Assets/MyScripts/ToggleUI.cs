using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleUI : MonoBehaviour {
    public GameObject toggleButton;
    public GameObject text;
    public int[] Pos = new int[] { 25, -100 };
    bool buttonState = true;
    // Use this for initialization
    void Start () {
        //StartCoroutine(flipButton());
    }
    
    // Update is called once per frame
    void Update () {
        //StartCoroutine(slideRight());
    }
    bool inTransit = false;
    public void onPress()
    {
        if (!inTransit) {
            inTransit = true;
            if (buttonState) {
                sOff ();
            } else {
                sOn ();

            }
            buttonState = !buttonState;
        }
    }
    public void sOn()
    {
        StartCoroutine(flipButton());
        StartCoroutine(slideOn());
    }
    private IEnumerator slideOff()
    {
        Debug.Log("Slide Off");
        while (toggleButton.transform.position.y > Pos[1])
        {
            yield return new WaitForSeconds(.01f);
            Vector3 pos = toggleButton.transform.position;
            pos.y = pos.y - 5;
            toggleButton.transform.position = pos;
        }
        inTransit = false;
    }

    public void sOff()
    {
        StartCoroutine(flipButton());
        StartCoroutine(slideOff());
    }
    private IEnumerator slideOn()
    {
        Debug.Log("Slide On");
        while (toggleButton.transform.position.x < Pos[0])
        {
            Debug.Log (Pos [0] + " " + toggleButton.transform.position.y);
            yield return new WaitForSeconds(.001f);
            Vector3 pos = toggleButton.transform.position;
            pos.y = pos.y + 5;
            toggleButton.transform.position = pos;
        }
        inTransit = false;
    }
    private IEnumerator flipButton()
    {
        int counter = 0; 
        while (counter < 180)
        {
            yield return new WaitForSeconds(.001f);
            Vector3 rot = text.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z - 4);
            counter += 4;
            //Debug.Log("Counter " + counter + " " + rot);
            text.transform.rotation = Quaternion.Euler(rot);
        }
    }
    public void restartScene(){
        //atring currentSceneName = SceneManager.GetActiveScene ().name;
        //SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
}

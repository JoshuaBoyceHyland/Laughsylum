using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class PickUpText : MonoBehaviour
{


    public bool GuiOn;


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("PickUp");
        GuiOn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GuiOn = false;
    }

    public string Text = "[PickUp]";

    public Rect BoxSize = new Rect(0, 0, Screen.width, Screen.height);

    public GUISkin customSkin;

    private void OnGUI()
    {
        if(customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if(GuiOn == true)
        {
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));

            GUI.Label(BoxSize, Text);

            GUI.EndGroup();
        }
    }

}



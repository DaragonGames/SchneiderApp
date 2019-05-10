using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationButton : MonoBehaviour
{

    public int NumberOfFormsLeft;
    GameObject myForm;
    public string FormName;

    private void Start()
    {
        // Preload Prefabs
        myForm = Resources.Load(FormName, typeof(GameObject)) as GameObject;
    }

    void OnMouseOver()
    {
        // On Click on the Button create a new Form and use the create function
        if (NumberOfFormsLeft >0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                    int ID;
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        ID = touch.fingerId;
                    }
                    else
                    {
                        ID = -1;
                    }
                    GameObject g = Instantiate(myForm, transform.position, Quaternion.identity);
                    g.GetComponent<Form>().GetSelected(this);
                    NumberOfFormsLeft--;                
            }
        }        
    }
}

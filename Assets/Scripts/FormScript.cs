using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormScript : MonoBehaviour
{

    // Object Variables
    public float xOffset;
    public float yOffset;
    public float Size;
    
    // Movement and Input Variables
    private int touchId = -1;
    private static GameObject lastSelected;
    private bool move;
    private bool CanBePlacedHere;

    // Variables for Collection
    CreationButton owner;

    // Get Input to move the form
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchId = touch.fingerId;
            }
            else
            {
                move = true;
            }
            GetSelected();
        }
    }

    // The Form was created
    public void Create(CreationButton Owner, int ID)
    {
        owner = Owner;

        // Get touchID or move 
        if (ID < 0)
        {
            move = true;
        }
        else
        {
            touchId = ID;
        }
        GetSelected();
    }

    void GetSelected()
    {
        if (lastSelected == gameObject)
        {
            // Deactivate Options and Stuff
        }
        else
        {
            if (lastSelected == null)
            {
                lastSelected = gameObject;
            }
            else
            {
                lastSelected.GetComponent<FormScript>().PlaceForm();
                lastSelected = gameObject;
            }           
        }
    }

    void Update()
    {
        // Just for Debugging
        if (move)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 9;

            if (Input.GetMouseButtonUp(0))
            {
                move = false;
                GetInPlace();
            }
        }


        if (touchId >= 0)
        {
            // Get the right touch Input
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == touchId)
                {
                    // Move the Form along to the Input
                    transform.position = Camera.main.ScreenToWorldPoint(touch.position) + Vector3.forward * 9;

                    // Place the form and end the moving phase
                    if (touch.phase == TouchPhase.Ended)
                    {
                        touchId = -1;
                        GetInPlace();
                    }
                }
            }
        }
    }

    void GetInPlace()
    {

        CanBePlacedHere = true;

        // 1. Correct the Position on the Grid
        float newX = transform.position.x - xOffset;
        float newY = transform.position.y - yOffset;
        newX = Mathf.Round(newX);
        newY = Mathf.Round(newY);
        newX += xOffset;
        newY += yOffset;
        transform.position = new Vector3(newX, newY, transform.position.z);

        // 2. Check Position on the Map
        if (transform.position.x < GameDevSettings.GridXStart || transform.position.x > GameDevSettings.GridXStart + GameDevSettings.GridWidth)
        {
            CanBePlacedHere = false;
        }

        if (transform.position.y < GameDevSettings.GridYStart || transform.position.y > GameDevSettings.GridYStart + GameDevSettings.GridHeight)
        {
            CanBePlacedHere = false;
        }


        // 4. Add Options for Rotation & for placing
        // 5. Show whether it can be placed here or not 

        return;
    }

    void OnTriggerEnter(Collider other)
    {
        if (lastSelected == gameObject)
        {
            ReturnToCollection();
        }
    }

    // Use this function to finnaly place the Form
    void PlaceForm()
    {
        // 1. Correct the Position on the Grid
        float newX = transform.position.x - xOffset;
        float newY = transform.position.y - yOffset;
        newX = Mathf.Round(newX);
        newY = Mathf.Round(newY);
        newX += xOffset;
        newY += yOffset;
        transform.position = new Vector3(newX, newY, transform.position.z);

        // 2. Check Position on the Map
        float GridX = GameDevSettings.GridXStart;
        float GridY = GameDevSettings.GridYStart;
        int GridW = GameDevSettings.GridWidth;
        int GridH = GameDevSettings.GridHeight;

        if (newX < GridX || newX > GridX + GridW)
        {
            ReturnToCollection();
        }

        if (newY < GridY || newY > GridY + GridH)
        {
            ReturnToCollection();
        }

        // 3. Place the Form and check for Collision
        transform.Translate(Vector3.forward);

    }

    // Use this function, when a form is not placed or returned to the Collection
    void ReturnToCollection()
    {
        owner.NumberOfFormsLeft++;
        Destroy(gameObject);
    }
































































    public void RotateOffset()
    {
        float saveX = xOffset;
        xOffset = yOffset;
        yOffset = saveX;
    }

}

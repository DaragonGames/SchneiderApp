using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form : MonoBehaviour
{
    // Variable for Returning the Part to the Collection
    private CreationButton owner;

    // Movement Variables
    private bool selected;

    // Variable for Placement
    private static Form lastSelected;
    public float xOffset;
    public float yOffset;

    // Variables for the Shadow
    private Shadow myShadow;
    public GameObject myShadowPrefab;


    // Check if there is a Click on the Form
    void OnMouseOver()
    {
        // Check if it is the first Input in case of Touch
        if (Input.GetMouseButtonDown(0))
        {
            // There was a Click above the Form, therefore it is getting Selected
            GetSelected(owner);
        }
    }

    // Function gets called whenever the Form is Selected
    public void GetSelected(CreationButton Owner)
    {
        // Set selection for movement
        selected = true;

        // Change Owner if it is created, nothing happens in case of movement
        owner = Owner;

        // Place the last Selected Form and becomme the last Selected Form
        if (lastSelected != null && lastSelected != this)
        {
            lastSelected.PlaceForm();
        }
        lastSelected = this;

        // Create a Shadow if there is no one yet
        if (myShadow == null)
        {
            GameObject g = Instantiate(myShadowPrefab, transform.position, Quaternion.identity);
            myShadow = g.GetComponent<Shadow>();
            myShadow.GetCreated(gameObject, xOffset, yOffset);
        }
    }

    void Update()
    {
        if (selected)
        {
            // Move the Form with the Mouse or Touch Input
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 5;

            // Stop being Selected when the Touch has ended
            if (Input.GetMouseButtonUp(0))
            {
                UnselectForm();
            }

            //
            // To Do add return to collection if its not even touching the grid
            //
        }
    }

    void UnselectForm()
    {
        selected = false;
        GhostUI.ghostUI.gameObject.SetActive(true);
        GhostUI.owner = this;
    }

    public void PlaceForm()
    {
        // 1. Set last Selected to null
        lastSelected = null;

        // 2. Deactivate the GhostUI
        GhostUI.ghostUI.gameObject.SetActive(false);
        GhostUI.owner = null;

        // 3. Check if it can be placed here and return to collection if not
        if (myShadow.Overlapping)
        {
            owner.NumberOfFormsLeft++;
            Destroy(myShadow.gameObject);
            Destroy(gameObject);
            return;
        }

        // 4. Destroy my Shadow and reset some Values 
        Destroy(myShadow.gameObject);
        myShadow = null;

        // 5. Positionate in the Grid properly
        float newX = transform.position.x - xOffset;
        float newY = transform.position.y - yOffset;
        newX = Mathf.Round(newX);
        newY = Mathf.Round(newY);
        newX += xOffset;
        newY += yOffset;
        transform.position = new Vector3(newX, newY, 0);
        return;

    }

    public void Rotate(int direction)
    {
        // Rotate the Form
        transform.Rotate(0, 0, 90*direction);

        // Rotate the Offset aswell
        float saveX = xOffset;
        xOffset = yOffset;
        yOffset = saveX;

        // Rotate the Shadow aswell
        myShadow.Rotate(direction);
        return;
    }

}

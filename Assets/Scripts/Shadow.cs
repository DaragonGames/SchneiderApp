using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Get these Values from the Owner
    GameObject owner;
    float xOffset;
    float yOffset;

    // Return these Values to the Owner
    public bool Overlapping;

    // Start is called before the first frame update
    public void GetCreated(GameObject Owner, float xOff, float yOff)
    {
        owner = Owner;
        xOffset = xOff;
        yOffset = yOff;
        transform.rotation = owner.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Move with your owner but never leave the grid
        float newX = owner.transform.position.x - xOffset;
        float newY = owner.transform.position.y - yOffset;
        newX = Mathf.Round(newX);
        newY = Mathf.Round(newY);
        newX += xOffset;
        newY += yOffset;
        if (newX != transform.position.x || newY != transform.position.y)
        {
            Overlapping = false;
            transform.position = new Vector3(newX, newY, 0);            
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Overlapping = true;
    }

    void OnTriggerStay(Collider other)
    {
        Overlapping = true;
    }

    public void Rotate(int direction)
    {
        // Rotate the Form
        transform.Rotate(0, 0, 90 * direction);

        // Rotate the Offset aswell
        float saveX = xOffset;
        xOffset = yOffset;
        yOffset = saveX;
        return;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostUI : MonoBehaviour
{
    public static Form owner;
    public string typ;
    public static GhostUI ghostUI;

    void Start()
    {
        if (typ == "ParentObject")
        {
            ghostUI = this;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (owner == null)
        {
            return;
        }

        if (typ == "ParentObject")
        {
            transform.position = owner.gameObject.transform.position + Vector3.back*2;
        }
    }

    void OnMouseOver()
    {
        if (owner == null)
        {
            return;
        }
        // Check if there is a Click Input
        if (Input.GetMouseButtonDown(0))
        {
            switch (typ)
            {
                case "Right":
                    owner.Rotate(1);
                    break;
                case "Left":
                    owner.Rotate(-1);
                    break;
                case "Confirm":
                    owner.PlaceForm();
                    break;
            }
        }
    }
}

using UnityEngine;

public class Arrow : MonoBehaviour
{

    public GameObject myForm;
    public int myDirection;
    public static Arrow leftArrow;
    public static Arrow rightArrow;

    // Start is called before the first frame update
    void Start()
    {
        if (myDirection == 1)
        {
            rightArrow = this;
        }
        else
        {
            leftArrow = this;
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the arrows with the Form
        if (myForm != null)
        {
            transform.position = myForm.transform.position;
            transform.localScale = myForm.GetComponent<FormScript>().Size * Vector3.one;
        }


        // If there is more than one touch happening
        if (Input.touchCount > 1)
        {
            // Check each touch if it is in its starting phase
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Check with a raycast wheter the touch is happening on this object
                    Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D ray = Physics2D.Raycast(position, Vector3.forward);
                    if (ray)
                    {
                        if (ray.transform.gameObject == gameObject)
                        {
                            myForm.transform.Rotate(0, 0, 90 * myDirection);
                            myForm.GetComponent<FormScript>().RotateOffset();
                        }
                    }
                }
            }
        }
    }
}
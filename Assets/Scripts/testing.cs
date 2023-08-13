using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    public void OnClick()
    {
        Debug.Log("yo");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 20);

        if (hit && hit.transform.name == "Enemies")
        {
            Debug.Log("enemy selected");
        }
    }
}

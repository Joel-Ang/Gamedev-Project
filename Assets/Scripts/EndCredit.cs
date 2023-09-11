using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredit : MonoBehaviour
{
    public GameObject endCredit;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenbuttonClicked()
    {
        if (endCredit.activeInHierarchy == false)
            endCredit.SetActive(true);
        else
            endCredit.SetActive(false);
            

    }
}

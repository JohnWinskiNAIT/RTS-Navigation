using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [SerializeField] GameObject selectionCircle;

    bool isSelected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected && selectionCircle.activeSelf == false)
        {
            selectionCircle.SetActive(true);
        }
        if (!isSelected && selectionCircle.activeSelf == true)
        {
            selectionCircle.SetActive(false);
        }
    }

    public void SetSelected(bool value)
    {
        isSelected = value;
    }
}

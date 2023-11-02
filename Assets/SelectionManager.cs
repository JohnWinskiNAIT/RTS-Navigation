using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField] Camera cam;
    [SerializeField] NavMeshAgent selectedObject;

    [SerializeField] InputAction selectionAction, clearAction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (selectionAction.WasPressedThisFrame())
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    if (selectedObject != null)
                    {
                        selectedObject.gameObject.GetComponent<SelectableObject>().SetSelected(false);
                    }
                    selectedObject = hit.collider.gameObject.GetComponentInParent<NavMeshAgent>();
                    selectedObject.gameObject.GetComponent<SelectableObject>().SetSelected(true);
                }
                else
                {
                    if (selectedObject != null)
                    {
                        selectedObject.destination = hit.point;
                    }
                }
            }
        }
        if (clearAction.WasPressedThisFrame())
        {
            selectedObject.gameObject.GetComponent<SelectableObject>().SetSelected(false);
            selectedObject = null;
        }
    }

    private void OnEnable()
    {
        selectionAction.Enable();
        clearAction.Enable();
    }

    private void OnDisable()
    {
        selectionAction.Disable();
        clearAction.Disable();
    }
}

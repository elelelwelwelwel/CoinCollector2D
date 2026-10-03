using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    private InputAction inventoryAction;
    [SerializeField] private GameObject InventoryCanvas;
    private bool InventoryActive = false;

    void Start()
    {
        InventoryCanvas.SetActive(false);
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        InventoryActive = false;
    }

    void Update()
    {
        if (inventoryAction.triggered)
        {
            if (!InventoryActive)
            {
                InventoryActive = true;
                InventoryCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (InventoryActive)
            {
                InventoryActive = false;
                InventoryCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
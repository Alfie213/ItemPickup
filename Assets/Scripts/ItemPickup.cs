using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    private PlayerInputActions _playerControls;
    
    private Camera _playerCamera;
    private InputAction _pickupOrPlaceItem;

    private GameObject _heldItem;

    private void Awake()
    {
        _playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void OnEnable()
    {
        _pickupOrPlaceItem = _playerControls.Player.PickupOrPlaceItem;
        _pickupOrPlaceItem.Enable();
        _pickupOrPlaceItem.performed += PickupOrPlaceItem;
    }

    private void OnDisable()
    {
        _pickupOrPlaceItem.Disable();
        _pickupOrPlaceItem.performed -= PickupOrPlaceItem;
    }

    public void PickupOrPlaceItem(InputAction.CallbackContext context)
    {
        if (_heldItem == null)
            TryPickupItem();
        else
            PlaceItemInPickup();
    }

    private void TryPickupItem()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("PickupItem"))
            {
                _heldItem = hit.transform.gameObject;
                _heldItem.GetComponent<Rigidbody>().isKinematic = true;
                _heldItem.transform.SetParent(_playerCamera.transform);
                _heldItem.transform.localPosition = new Vector3(0, 0, 1);
            }
        }
    }

    private void PlaceItemInPickup()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("PickupTruck"))
            {
                _heldItem.transform.SetParent(hit.transform);
                _heldItem.transform.localPosition = new Vector3(0, 1, 0);
                _heldItem.GetComponent<Rigidbody>().isKinematic = false;
                _heldItem = null;
            }
        }
    }
}

using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Camera _playerCamera;

    private GameObject _heldItem;

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    public void PickupOrPlaceItem()
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
                _heldItem.transform.localPosition = new Vector3(0, 0, 2);
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
                _heldItem.transform.SetParent(null);
                _heldItem.GetComponent<Rigidbody>().isKinematic = false;
                _heldItem = null;
            }
        }
    }
}

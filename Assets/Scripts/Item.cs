using UnityEngine;

public class Item : MonoBehaviour
{
    ItemDetails itemDetails;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the item");
            GrabItem();
        }
    }

    public void GrabItem()
    {
        Debug.Log("Item has been grabbed");
        Destroy(gameObject);
    }
}

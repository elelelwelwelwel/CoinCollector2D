using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System

public class PlayerMovement : MonoBehaviour
{
    public int skor = 0;
    public float kecepatan = 5f;
    private Vector2 arahGerak;

    [Header("Pengaturan Lampu")]
    // Drag objek Spot Light 2D Anda ke slot ini di Inspector
    public Transform spotLight2D; 

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        // 1. Gerakkan pemain
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;

        // 2. Putar Spot Light 2D sesuai arah gerak
        AturArahLampu();
    }

    void AturArahLampu()
    {
        // Pastikan pemain sedang bergerak (vektor input tidak nol)
        if (arahGerak != Vector2.zero && spotLight2D != null)
        {
            // Hitung sudut rotasi berdasarkan vektor input (x, y)
            float sudut = Mathf.Atan2(arahGerak.y, arahGerak.x) * Mathf.Rad2Deg;

            // Jika lampu Anda secara default mengarah ke atas (sumbu Y positif),
            // kurangi sudut sebesar 90 derajat agar arahnya pas:
            sudut -= 90f; 

            // Terapkan rotasi pada sumbu Z
            spotLight2D.rotation = Quaternion.Euler(0, 0, sudut);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            skor++;
            Debug.Log("Skor: " + skor);

            GameManager gamemanager = Object.FindFirstObjectByType<GameManager>();
            if (gamemanager != null)
            {
                gamemanager.AmbilKoin();
            }
        }
    }
}
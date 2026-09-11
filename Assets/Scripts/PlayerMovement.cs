using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System
public class PlayerMovement : MonoBehaviour
{
    public int skor = 0;
    public float kecepatan = 5f;
    private Vector2 arahGerak;

    // nilai dari action "Move"
    // Dipanggil OTOMATIS oleh komponen Player Input
    // saat action "Move" pada asset InputSystem_Actions aktif.
    // Nama method WAJIB: On + nama action -> OnMove
    void OnMove(InputValue value)
    {
        // TODO: ambil nilai Vector2 dari input, simpan ke arahGerak
        arahGerak = value.Get<Vector2>();
    }
    void Update()
    {
        // TODO: gerakkan objek memakai arahGerak.
        // Ingat kalikan kecepatan DAN Time.deltaTime!
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            // TODO: tambah skor sebanyak 1
            skor++;
            // TODO: tampilkan skor ke Console
            Debug.Log("Skor: " + skor);
            GameManager gamemanager = Object.FindFirstObjectByType<GameManager>();
            if (gamemanager != null)
            {
                gamemanager.AmbilKoin();
            }
        }
    }

}
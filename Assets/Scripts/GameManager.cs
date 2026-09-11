using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;
    public TextMeshProUGUI teksSkor;
    public GameObject panelMenang;

    [SerializeField] private int skor = 0;


    void OnEnable()
    {
        Enemy.OnZombieMati += TambahSkorSaatZombieMati;
    }

    void OnDisable()
    {
        Enemy.OnZombieMati -= TambahSkorSaatZombieMati;
    }
    void TambahSkorSaatZombieMati(Enemy zombie)
    {
        skor += 10; // Tambahkan skor sesuai kebutuhan
        Debug.Log("Skor: " + skor);
    }

    void Start()
    {
        // TODO: hitung jumlah koin di scene saat mulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Jumlah Koin: " + totalKoin);

        UpdateSkorUI();
    }
    public void AmbilKoin()
    {
        koinTerkumpul++;
        // TODO: jika koinTerkumpul == totalKoin, panggil Menang()
        UpdateSkorUI();
        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }
    void Menang()
    {
        Debug.Log("KAMU MENANG!");
        if (panelMenang != null)
        {
            panelMenang.SetActive(true);
            Time.timeScale = 0f; // Hentikan waktu saat menang
        }
    }

    void UpdateSkorUI()
    {
        if (teksSkor != null)
        {
            teksSkor.text = "Skor: " + koinTerkumpul;
        }
    }

    public void RestartGame()
    {
        // SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Kembalikan waktu ke normal saat restart
    }
}
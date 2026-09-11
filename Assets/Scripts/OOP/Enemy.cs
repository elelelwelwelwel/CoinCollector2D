using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public int hp = 100;
    public static event Action<Enemy> OnZombieMati;
    public float ms = 2f;
    [SerializeField] private int damageSaatTabrakan = 20;
    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f; // masuk CHASE
    [SerializeField] private float jarakSerang = 1.2f; // masuk ATTACK
    [SerializeField] private float jedaSerang = 1f; // detik antar serang
    // state sekarang -- mulai dari IDLE
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;
    [SerializeField] private float radiusPatrol = 3f;
    private Vector2 titikAwal; // pusat area keliling
    private Vector2 tujuanPatrol; // titik yang sedang dituju

    protected Transform player;


    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        titikAwal = transform.position;
        PilihTujuanPatrolBaru();
    }


    void PilihTujuanPatrolBaru()
    {
        Vector2 acak = UnityEngine.Random.insideUnitCircle * radiusPatrol;
        tujuanPatrol = titikAwal + acak;
    }

    void Update()
    {
        // LANGKAH A: tentukan state (aturan pindah)
        PeriksaTransisi();

        // LANGKAH B: jalankan perilaku sesuai state sekarang
        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

    void PerilakuIdle() { }
    void PerilakuPatrol()
    {
        transform.position = Vector2.MoveTowards(
        transform.position, tujuanPatrol, ms * 0.5f * Time.deltaTime);
        if (Vector2.Distance(transform.position, tujuanPatrol) < 0.1f)
            PilihTujuanPatrolBaru();
    }
    void PerilakuChase() { Kejar(); Debug.Log(name + ": CHASE"); }
    void PerilakuAttack()
    {
        // menyerang berkala, tidak tiap frame
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang(); // method dari OOP
            waktuSerangTerakhir = Time.time;
        }
    }
    float JarakKePlayer()
    {
        if (player == null) return float.MaxValue; // Jika player tidak ada, anggap jarak sangat jauh
        return Vector2.Distance(transform.position, player.position);
    }
    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer(); // sudah ada dari materi OOP!
        if (jarak <= jarakSerang)
            state = StateZombie.ATTACK; // sangat dekat -> serang
        else if (jarak <= jarakDeteksi)
            state = StateZombie.CHASE; // terlihat -> kejar
        else
            state = StateZombie.PATROL; // jauh -> keliling
    }


    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position, player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable playerScript = collision.GetComponent<IDamageable>();
            if (playerScript != null)
            {
                playerScript.KenaDamage(damageSaatTabrakan);
            }
        }
    }

    public void KenaDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Enemy Kena damage: " + damage + ", HP sekarang:" + hp);

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log("Enemy mati");
        OnZombieMati?.Invoke(this);
        Destroy(gameObject);
    }
}
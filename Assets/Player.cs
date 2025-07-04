using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Di chuyển")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Tự động bắn")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float fireTimer;

    [Header("Xoay máy bay (trục Y khi trái/phải)")]
    public float maxTiltAngle = 25f;
    public float tiltSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       
        float inputX = Input.GetAxisRaw("Horizontal"); 
        float inputY = Input.GetAxisRaw("Vertical");   

        moveInput = new Vector2(inputX, inputY).normalized;

        // Tự động bắn
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

        // Xoay máy bay theo trục Y khi nhấn trái/phải
        float targetYAngle = -inputX * maxTiltAngle;
        Quaternion targetRotation = Quaternion.Euler(0f, targetYAngle, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    void FixedUpdate()
    {
       //di chuyen theo 4 phuong
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
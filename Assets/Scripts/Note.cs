using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector3 targetPosition;
    public float hitTime;
    public KeyCode inputKey;
    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private float spawnTime;
    private SpriteRenderer sr;
    private Color originalColor;
    public Color pressedColor = Color.white;

    void Start()
    {
        startPosition = transform.position;
        spawnTime = Time.time;

        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            originalColor = sr.color;
    }

    void Update()
    {
        float duration = hitTime - spawnTime;
        if (duration <= 0f)
            return;

        float t = (Time.time - spawnTime) / duration;
        t = Mathf.Clamp01(t);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);

        // Detecta si se presiona la tecla correcta
        if (Input.GetKeyDown(inputKey) && sr != null)
            sr.color = pressedColor;

        if (Input.GetKeyUp(inputKey) && sr != null)
            sr.color = originalColor;

        // Destruye la nota cuando llega al destino
        if (t >= 1f)
            Destroy(gameObject);
    }
}
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Prefabs de notas")]
    public GameObject leftNotePrefab;
    public GameObject rightNotePrefab;
    public GameObject upNotePrefab;
    public GameObject downNotePrefab;

    [Header("Posiciones objetivo")]
    public Transform leftTarget;
    public Transform rightTarget;
    public Transform upTarget;
    public Transform downTarget;

    [Header("Configuración")]
    public float noteSpeed = 2f;
    public float spawnInterval = 1.0f;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRandomNote();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnRandomNote()
    {
        int randomIndex = Random.Range(0, 4);
        GameObject prefabToSpawn = null;
        Transform target = null;
        KeyCode key = KeyCode.LeftArrow;

        switch (randomIndex)
        {
            case 0:
                prefabToSpawn = leftNotePrefab;
                target = leftTarget;
                key = KeyCode.LeftArrow;
                break;
            case 1:
                prefabToSpawn = rightNotePrefab;
                target = rightTarget;
                key = KeyCode.RightArrow;
                break;
            case 2:
                prefabToSpawn = upNotePrefab;
                target = upTarget;
                key = KeyCode.UpArrow;
                break;
            case 3:
                prefabToSpawn = downNotePrefab;
                target = downTarget;
                key = KeyCode.DownArrow;
                break;
        }

        if (prefabToSpawn == null || target == null)
        {
            Debug.LogWarning("Falta asignar algún prefab o target.");
            return;
        }

        GameObject noteObj = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        Note note = noteObj.GetComponent<Note>();

        note.targetPosition = target.position;
        note.inputKey = key;
        note.hitTime = Time.time + noteSpeed;
    }
}
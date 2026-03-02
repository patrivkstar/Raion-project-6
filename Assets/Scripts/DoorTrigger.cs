using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    public Vector2Int moveDirection; // Isi di Inspector (Atas=0,1 | Bawah=0,-1 | Kiri=-1,0 | Kanan=1,0)

    private DungeonGenerator dungeon;

    void Start() {
        dungeon = FindObjectOfType<DungeonGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            dungeon.MoveToRoom(moveDirection, collision.transform);
        }
    }
}

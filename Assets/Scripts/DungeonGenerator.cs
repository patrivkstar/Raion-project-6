using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {
    public GameObject roomPrefab;
    public int totalRooms = 10;
    
    // Jarak antar ruang sesuai Scale Lantai (18x10)
    public float roomWidth = 18f;
    public float roomHeight = 10f;

    private Dictionary<Vector2Int, RoomNode> rooms = new Dictionary<Vector2Int, RoomNode>();
    private Dictionary<Vector2Int, GameObject> spawnedRooms = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentPos = Vector2Int.zero;

    void Start() {
        GenerateLayout();
        SpawnRooms();
        UpdateCameraAndPlayer();
    }

    void GenerateLayout() {
        rooms.Add(Vector2Int.zero, new RoomNode(Vector2Int.zero));
        List<Vector2Int> nodes = new List<Vector2Int> { Vector2Int.zero };
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        int count = 1;
        while (count < totalRooms) {
            Vector2Int randomNode = nodes[Random.Range(0, nodes.Count)];
            Vector2Int dir = dirs[Random.Range(0, dirs.Length)];
            Vector2Int newPos = randomNode + dir;

            if (!rooms.ContainsKey(newPos)) {
                rooms.Add(newPos, new RoomNode(newPos));
                nodes.Add(newPos);
                count++;
            }
        }
    }

    void SpawnRooms() {
        foreach (var room in rooms) {
            Vector2Int pos = room.Key;
            Vector3 spawnPos = new Vector3(pos.x * roomWidth, pos.y * roomHeight, 0);
            GameObject newRoom = Instantiate(roomPrefab, spawnPos, Quaternion.identity);
            newRoom.name = $"Room_{pos.x}_{pos.y}";
            spawnedRooms.Add(pos, newRoom);

            // LOGIKA PINTU PINTAR: Cek tetangga
            SetupDoors(newRoom, pos);

            // OPTIMASI: Matikan "Contents" di semua ruangan
            newRoom.transform.Find("Contents").gameObject.SetActive(false);
        }
        // Nyalakan "Contents" hanya di ruang awal (0,0)
        spawnedRooms[Vector2Int.zero].transform.Find("Contents").gameObject.SetActive(true);
    }

    void SetupDoors(GameObject room, Vector2Int pos) {
        Transform doors = room.transform.Find("Doors");
        
        // Pintu Atas
        bool hasTop = rooms.ContainsKey(pos + Vector2Int.up);
        doors.Find("Top/Trigger").gameObject.SetActive(hasTop);
        doors.Find("Top/Blocker").gameObject.SetActive(!hasTop);

        // Pintu Bawah
        bool hasBottom = rooms.ContainsKey(pos + Vector2Int.down);
        doors.Find("Bottom/Trigger").gameObject.SetActive(hasBottom);
        doors.Find("Bottom/Blocker").gameObject.SetActive(!hasBottom);

        // Pintu Kiri
        bool hasLeft = rooms.ContainsKey(pos + Vector2Int.left);
        doors.Find("Left/Trigger").gameObject.SetActive(hasLeft);
        doors.Find("Left/Blocker").gameObject.SetActive(!hasLeft);

        // Pintu Kanan
        bool hasRight = rooms.ContainsKey(pos + Vector2Int.right);
        doors.Find("Right/Trigger").gameObject.SetActive(hasRight);
        doors.Find("Right/Blocker").gameObject.SetActive(!hasRight);
    }

    public void MoveToRoom(Vector2Int direction, Transform playerTransform) {
        Vector2Int nextPos = currentPos + direction;

        if (spawnedRooms.ContainsKey(nextPos)) {
            // 1. Matikan ruang lama
            spawnedRooms[currentPos].transform.Find("Contents").gameObject.SetActive(false);
            
            // 2. Update posisi statis saat ini
            currentPos = nextPos;
            
            // 3. Nyalakan ruang baru
            spawnedRooms[currentPos].transform.Find("Contents").gameObject.SetActive(true);

            // 4. Pindahkan Kamera (Snap)
            UpdateCameraAndPlayer();

            // 5. Pindahkan Player agar tidak nyangkut di pintu (muncul di sisi seberangnya)
            float offset = 2.5f; // Jarak player dari pintu setelah pindah ruang
            Vector3 newPlayerPos = Camera.main.transform.position;
            newPlayerPos.z = 0; // Kembalikan Z ke 0 karena kamera ada di -10
            
            if (direction == Vector2Int.up) newPlayerPos.y -= (roomHeight / 2) - offset;
            if (direction == Vector2Int.down) newPlayerPos.y += (roomHeight / 2) - offset;
            if (direction == Vector2Int.left) newPlayerPos.x += (roomWidth / 2) - offset;
            if (direction == Vector2Int.right) newPlayerPos.x -= (roomWidth / 2) - offset;

            playerTransform.position = newPlayerPos;
        }
    }

    void UpdateCameraAndPlayer() {
        Vector3 newCameraPos = spawnedRooms[currentPos].transform.position;
        newCameraPos.z = -10f; // Kamera 2D harus mundur di Z = -10
        Camera.main.transform.position = newCameraPos;
    }
}


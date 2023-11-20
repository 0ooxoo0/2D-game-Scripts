using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] UpRooms;
    public GameObject[] DownRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;

    public GameObject key;

    [HideInInspector] public List<GameObject> rooms;

    private void Start()
    {
        StartCoroutine(RandomSpawner());
    }

    void Update()
    {
        
    }
    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int rand = Random.Range(1, rooms.Count - 2);

        Instantiate(key, rooms[rand].transform.position, Quaternion.identity);

        lastRoom.door.SetActive(true);
        lastRoom.Boss.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }
    public void Spawn()
    {
        if (!spawned)
        {
            if (direction == Direction.Up)
            {
                rand = Random.Range(0, variants.UpRooms.Length);
                Instantiate(variants.UpRooms[rand], transform.position, variants.UpRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Down)
            {
                rand = Random.Range(0, variants.DownRooms.Length);
                Instantiate(variants.DownRooms[rand], transform.position, variants.DownRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.RightRooms.Length);
                Instantiate(variants.RightRooms[rand], transform.position, variants.RightRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.LeftRooms.Length);
                Instantiate(variants.LeftRooms[rand], transform.position, variants.LeftRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
}

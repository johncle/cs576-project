using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap_level2 : MonoBehaviour
{
    public RectTransform marker; //player pointer image
    public RectTransform keycard; //keycard pointer image
    public RectTransform mapImage;//Map screenshot used in canvas
    public Transform playerReference;//player
    private Transform[] mapEdges;//4 edges, make sure its in rectangle.
    public Vector2 offset;//Adjust the value to match you map
    public Vector2 keycard_offset;
    public Transform keycard_position;//Adjust the value to match you map

    private Vector2 mapDimentions;
    private Vector2 areaDimentions;

    private void Start()
    {
        mapEdges = new Transform[4];
        // index=0: x = 12, z = -30
        // index=1: x = 12, z = 0
        // index=2: x = 50, z = -30
        // index=3: x = 50, z = 0
        mapEdges[0] = new GameObject().transform;
        mapEdges[0].position = new Vector3(50, 0, -30);
        mapEdges[1] = new GameObject().transform;
        mapEdges[1].position = new Vector3(12, 0, -30);
        mapEdges[2] = new GameObject().transform;
        mapEdges[2].position = new Vector3(50, 0, 0);
        mapEdges[3] = new GameObject().transform;
        mapEdges[3].position = new Vector3(12, 0, 0);
        for (int i = 0; i < mapEdges.Length; i++)
            for (int j = i + 1; j < mapEdges.Length; j++)
            {
                if (mapEdges[j].position.x < mapEdges[i].position.x || mapEdges[j].position.z < mapEdges[i].position.z)
                {
                    Transform temp = mapEdges[j];
                    mapEdges[j] = mapEdges[i];
                    mapEdges[i] = temp;
                }
            }
        mapDimentions = new Vector2(mapImage.sizeDelta.x, mapImage.sizeDelta.y);
        areaDimentions.x = 50 - 12;
        areaDimentions.y = 30;
        offset = new Vector2(-460, 230);
        keycard_offset = new Vector2(-10, 20);
    }

    private void Update()
    {
        SetMarketPosition();
    }

    private void SetMarketPosition()
    {
        Vector3 distance = playerReference.position - mapEdges[0].position;
        // Debug.Log("distance: " + distance);
        // Debug.Log("areaDimentions: " + areaDimentions);
        Vector2 coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        // Debug.Log("playerReference: " + playerReference.position);
        // rotate coordinates by -90 degrees
        // coordinates = new Vector2(coordinates.y, 1 - coordinates.x);
        // Debug.Log("coordinates: "  + coordinates);
        marker.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + offset;
        marker.rotation = Quaternion.Euler(new Vector3(0, 0, -playerReference.eulerAngles.y));
        

        distance = keycard_position.position - mapEdges[0].position;
        coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        keycard.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + keycard_offset + offset;

    }

}

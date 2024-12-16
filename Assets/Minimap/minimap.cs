using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public RectTransform marker; //player pointer image
    public RectTransform keycard; //keycard pointer image
    public RectTransform mapImage;//Map screenshot used in canvas
    public Transform playerReference;//player
    private Transform[] mapEdges;//4 edges, make sure its in rectangle.
    public Vector2 offset;//Adjust the value to match you map
    public Vector2 keycard_position;//Adjust the value to match you map

    private Vector2 mapDimentions;
    private Vector2 areaDimentions;

    private void Start()
    {
        mapEdges = new Transform[4];
        // index=0: x = 54, z = -12
        // index=1: x = 54, z = 0
        // index=2: x = 66, z = -12
        // index=3: x = 66, z = 0
        mapEdges[0] = new GameObject().transform;
        mapEdges[0].position = new Vector3(54, 0, -12);
        mapEdges[1] = new GameObject().transform;
        mapEdges[1].position = new Vector3(54, 0, 1);
        mapEdges[2] = new GameObject().transform;
        mapEdges[2].position = new Vector3(66, 0, -12);
        mapEdges[3] = new GameObject().transform;
        mapEdges[3].position = new Vector3(66, 0, 1);
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
        areaDimentions.x = mapEdges[1].position.x - mapEdges[0].position.x;
        areaDimentions.y = mapEdges[2].position.z - mapEdges[0].position.z;
        // offset = new Vector2(-510, 190);
        offset = new Vector2(-550, 80);
    }

    private void Update()
    {
        SetMarketPosition();
    }

    private void SetMarketPosition()
    {
        Vector3 distance = playerReference.position - mapEdges[0].position;
        Vector2 coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        Debug.Log("coordipopositionsitionnates: " + playerReference.position);
        // rotate coordinates by -90 degrees
        // coordinates = new Vector2(coordinates.y, 1 - coordinates.x);
        marker.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + offset;
        marker.rotation = Quaternion.Euler(new Vector3(0, 0, -playerReference.eulerAngles.y));
        keycard.anchoredPosition = keycard_position;
    }

}

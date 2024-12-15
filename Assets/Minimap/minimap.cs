using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public RectTransform marker; //player pointer image
    public RectTransform mapImage;//Map screenshot used in canvas
    public Transform playerReference;//player
    public Transform[] mapEdges;//4 edges, make sure its in rectangle.
    public Vector2 offset;//Adjust the value to match you map

    private Vector2 mapDimentions;
    private Vector2 areaDimentions;

    private void Start()
    {
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
        offset = new Vector2(-354, 90);
    }

    private void Update()
    {
        SetMarketPosition();
    }

    private void SetMarketPosition()
    {
        Vector3 distance = playerReference.position - mapEdges[0].position;
        Vector2 coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        // rotate coordinates by -90 degrees
        coordinates = new Vector2(coordinates.y, 1 - coordinates.x);
        marker.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + offset;
        marker.rotation = Quaternion.Euler(new Vector3(0, 0, -playerReference.eulerAngles.y));
    }

}

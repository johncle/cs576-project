using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap_level4 : MonoBehaviour
{
    public RectTransform marker; //player pointer image
    public RectTransform keycard; //keycard pointer image
    public RectTransform green_keycard; //green keycard pointer image
    public RectTransform mapImage;//Map screenshot used in canvas
    public RectTransform bot1;
    public RectTransform bot2;
    public Transform playerReference;//player
    private Transform[] mapEdges;//4 edges, make sure its in rectangle.
    public Vector2 offset;//Adjust the value to match you map
    public Vector2 keycard_offset;
    public Vector2 green_keycard_offset;
    public Vector2 bot1_offset;
    public Vector2 bot2_offset;
    public Transform keycard_position;//Adjust the value to match you map
    public Transform green_keycard_position;//Adjust the value to match you map
    public Transform bot1_position;
    public Transform bot2_position;

    private Vector2 mapDimentions;
    private Vector2 areaDimentions;

    private void Start()
    {
        mapEdges = new Transform[4];
        // index=0: x = 0, z = 24
        // index=1: x = 0, z = 0
        // index=2: x = 20, z = 24
        // index=3: x = 20, z = 0
        mapEdges[0] = new GameObject().transform;
        mapEdges[0].position = new Vector3(0, 0, 0);
        mapEdges[1] = new GameObject().transform;
        mapEdges[1].position = new Vector3(0, 0, 0);
        mapEdges[2] = new GameObject().transform;
        mapEdges[2].position = new Vector3(22, 0, 26);
        mapEdges[3] = new GameObject().transform;
        mapEdges[3].position = new Vector3(22, 0, 26);
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
        areaDimentions.x = 22;
        areaDimentions.y = 26;
        offset = new Vector2(-500, 215);
        keycard_offset = new Vector2(0, 10);
        green_keycard_offset = new Vector2(0, 0);
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

        distance = green_keycard_position.position - mapEdges[0].position;
        coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        green_keycard.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + green_keycard_offset + offset;

        distance = bot1_position.position - mapEdges[0].position;
        Debug.Log("bot1_position: " + bot1_position.position);
        coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        bot1.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + bot1_offset + offset;
        bot1.rotation = Quaternion.Euler(new Vector3(0, 0, -bot1_position.eulerAngles.y));

        distance = bot2_position.position - mapEdges[0].position;
        Debug.Log("bot2_position: " + bot2_position.position);
        coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        bot2.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + bot2_offset + offset;
        bot2.rotation = Quaternion.Euler(new Vector3(0, 0, -bot2_position.eulerAngles.y));

    }

}

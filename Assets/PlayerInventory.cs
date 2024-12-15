using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private HashSet<string> keycards = new HashSet<string>();

    public void AddKeycard(string keycardID)
    {
        keycards.Add(keycardID);
    }

    public bool HasKeycard(string keycardID)
    {
        return keycards.Contains(keycardID);
    }
}

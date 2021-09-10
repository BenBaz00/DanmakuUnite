using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePage
{
    public List<Dialogue> Dialogues;
}

[System.Serializable]
public class Dialogue
{
    // Trigger flags for dialogue
    public int wave; // Wave that dialogue will trigger
    public bool EnemyInactive; // Flag to wait for enemies to be destroyed/despawn

    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}

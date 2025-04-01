using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public Sprite characterPortrait;
    [TextArea(3, 10)]
    public string[] sentences;
}

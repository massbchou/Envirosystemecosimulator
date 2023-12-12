using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name = "David Attenborough";

    [TextArea(1, 10)]
    public string[] sentences;
    public AudioClip[] audioClips;
}

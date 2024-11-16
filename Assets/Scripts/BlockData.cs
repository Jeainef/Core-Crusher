using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Block", menuName = "New Block", order = 0)]
public class BlockData : ScriptableObject {
    public byte ID;
    public bool isSolid;

}


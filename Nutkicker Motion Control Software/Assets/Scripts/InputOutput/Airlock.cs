using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    public static BlockingCollection<string> Container = new BlockingCollection<string>(1);
}



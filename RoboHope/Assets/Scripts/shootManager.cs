using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootManager : MonoBehaviour {
    public void AnimSpawnShot()
    {
        RobotManager.instance.SpawnShot();
    }
}

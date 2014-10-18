using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HS_Object : MonoBehaviour {

    public List<Score> Scores { get; set; }


}

public class Score
{
    public string score { get; set; }
    public string name { get; set; }
    public string uid { get; set; }
}

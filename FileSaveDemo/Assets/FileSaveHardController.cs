using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSaveHardController : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        System.Text.StringBuilder contentBuilder = new System.Text.StringBuilder();

        contentBuilder.AppendLine("E E E E E E E E");
        contentBuilder.AppendLine("E F F E E F F E");
        contentBuilder.AppendLine("E F F E E F F E");
        contentBuilder.AppendLine("E F F C C F F E");
        contentBuilder.AppendLine("E F F C C F F E");
        contentBuilder.AppendLine("E F F E E F F E");
        contentBuilder.AppendLine("E F F E E F F E");
        contentBuilder.AppendLine("E E E E E E E E");

        string content = contentBuilder.ToString();

        // Replace any print/Monobehavior.Debug calls with this to write to file
        File.WriteAllText("C:\\temp\\sample.bspd", content);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

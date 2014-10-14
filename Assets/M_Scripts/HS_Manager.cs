using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Net;
using System.IO;

public class HS_Manager : MonoBehaviour {

	public HS_Object geths () {
        WebRequest request = WebRequest.Create("http://motes.at/hrunnerhs.php?action=geths");
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();

        HS_Object hslist = JsonConvert.DeserializeObject<HS_Object>(responseFromServer);
        
        reader.Close();
        response.Close();
        return hslist;
	}
	
	public void seths (string name, int score) {
        WebRequest request = WebRequest.Create("http://motes.at/hrunnerhs.php?action=seths&name="+name+"&score="+score.ToString());
        WebResponse response = request.GetResponse();
        response.Close();
	}
}

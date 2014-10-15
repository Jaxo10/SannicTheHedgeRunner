using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

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
	
	public void seths (string name, int score, string UID) {
        
        string code = UID + score.ToString().Substring(1);

        StringBuilder sb = new StringBuilder();
        MD5 md5Hasher = MD5.Create();
        byte[] tempSource = Encoding.ASCII.GetBytes(code);
            foreach (Byte b in md5Hasher.ComputeHash(tempSource))
                sb.Append(b.ToString("x2").ToLower());

        code = sb.ToString();


        WebRequest request = WebRequest.Create("http://motes.at/hrunnerhs.php?action=seths&name="+name+"&score="+score.ToString()+"&uid="+UID+"&code="+code);
        WebResponse response = request.GetResponse();
        response.Close();
	}
}

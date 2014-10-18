using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
//using System.Net;
//using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

#if  UNITY_METRO
using UnityEngine.Windows;
#endif

public class HS_Manager : MonoBehaviour {

	public HS_Object geths () {
        /*WebRequest request = WebRequest.Create("http://motes.at/hrunnerhs.php?action=geths");
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        reader.Dispose();
        response.Close();*/

        WWW WWrequest = new WWW("http://motes.at/hrunnerhs.php?action=geths");
        HS_Object hslist = null;
        while (!WWrequest.isDone)
        {
        }
        if (WWrequest.error != null)
        {
            Debug.Log("There was an error getting the high score: " + WWrequest.error);
        }
        else if (WWrequest.isDone)
        {

            hslist = JsonConvert.DeserializeObject<HS_Object>(WWrequest.text);

            
        }
        return hslist;

        

	}
	
	public void seths (string name, int score, string UID) {
        
        string code = UID + score.ToString().Substring(1);
        code = ComputeHash(code);

        new WWW("http://motes.at/hrunnerhs.php?action=seths&name=" + name + "&score=" + score.ToString() + "&uid=" + UID + "&code=" + code);

        /*WebRequest request = WebRequest.Create("http://motes.at/hrunnerhs.php?action=seths&name="+name+"&score="+score.ToString()+"&uid="+UID+"&code="+code);
        WebResponse response = request.GetResponse();
        response.Close();*/
	}

    public string ComputeHash(string s)
    {

    #if  UNITY_METRO
        // Form hash
        byte[] hashBytes = Crypto.ComputeMD5Hash(System.Text.Encoding.UTF8.GetBytes(s));
        
        StringBuilder sb = new StringBuilder();
        foreach (Byte b in hashBytes)
            sb.Append(b.ToString("x2").ToLower());
        return sb.ToString();

    #else
        StringBuilder sb = new StringBuilder();
        MD5 md5Hasher = MD5.Create();
        byte[] tempSource = Encoding.ASCII.GetBytes(code);
            foreach (Byte b in md5Hasher.ComputeHash(tempSource))
                sb.Append(b.ToString("x2").ToLower());

        return sb.ToString();
    #endif
    }
}

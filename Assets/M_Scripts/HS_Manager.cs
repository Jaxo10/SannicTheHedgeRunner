using UnityEngine;
using System.Collections;
//using Newtonsoft.Json;
//using System.Net;
//using System.IO;
using System.Text;
using MiniJSON;
using System.Collections.Generic;
//using System;

//#if !UNITY_EDITOR && UNITY_METRO
using UnityEngine.Windows;
/*#else
using System.Security.Cryptography;
#endif*/

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
        if (WWrequest.isDone)
        {
            
/*#if !UNITY_EDITOR && UNITY_METRO
            hslist = JsonConvert.DeserializeObject<HS_Object>(WWrequest.text);
#else
            hslist = new HS_Object();
            Score sitem = new Score();
            sitem.name = "Fuck Unity";
            sitem.score = "404";
            
            hslist.Scores = new List<Score>();
            hslist.Scores.Add(sitem);
#endif*/

            var deserialized = Json.Deserialize(WWrequest.text) as Dictionary<string, object>;
            List<object> TheScores = (List<object>) deserialized["Scores"];
            List<Score> MyScores = new List<Score>();

            for (int i = 0; i < TheScores.Count; i++)
            {
                Dictionary<string,object> TheScoreObject = TheScores[i] as Dictionary<string,object>;
                var MyScoreObject = new Score();

                MyScoreObject.name = (string) TheScoreObject["name"];
                MyScoreObject.score = (string) TheScoreObject["score"];
                MyScoreObject.uid = (string)TheScoreObject["uid"];
                MyScores.Add(MyScoreObject);
            }

            hslist = new HS_Object();
            hslist.Scores = MyScores;
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

//#if !UNITY_EDITOR && UNITY_METRO
        // Form hash
        byte[] hashBytes = Crypto.ComputeMD5Hash(System.Text.Encoding.UTF8.GetBytes(s));
        
        StringBuilder sb = new StringBuilder();
        foreach (System.Byte b in hashBytes)
            sb.Append(b.ToString("x2").ToLower());
        return sb.ToString();

/*#else
        StringBuilder sb = new StringBuilder();
        MD5 md5Hasher = MD5.Create();
        byte[] tempSource = Encoding.ASCII.GetBytes(s);

            foreach (System.Byte b in md5Hasher.ComputeHash(tempSource))
                sb.Append(b.ToString("x2").ToLower());

            
        return sb.ToString();
#endif*/
    }
}

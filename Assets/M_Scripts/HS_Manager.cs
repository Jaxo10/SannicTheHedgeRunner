using UnityEngine;
using System.Collections;
using System.Text;
using MiniJSON;
using System.Collections.Generic;
#if UNITY_WINRT
using UnityEngine.Windows;
#else
using System.IO;
#endif


public class HS_Manager : MonoBehaviour {

	public HS_Object geths () {

        WWW WWrequest = new WWW("http://motes.at/hrunnerhs.php?action=geths");
        HS_Object hslist = null;
        while (!WWrequest.isDone)
        {
        }
        if (WWrequest.isDone)
        {

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
	}

    public string ComputeHash(string s)
    {

#if UNITY_WINRT
            byte[] hashBytes = Crypto.ComputeMD5Hash(Encoding.UTF8.GetBytes(s));
#else
            MD5 md5Hasher = MD5.Create();
            byte[] hashBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(s));
#endif
            StringBuilder sb = new StringBuilder();
            foreach (System.Byte b in hashBytes)
                sb.Append(b.ToString("x2").ToLower());
            return sb.ToString();

    }
}

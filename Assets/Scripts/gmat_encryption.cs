using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public class gmat_encryption : MonoBehaviour {

	public static string Encrypt(string sText) {
		MD5 md5 = new MD5CryptoServiceProvider();  

		//compute hash from the bytes of text  
		md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(sText));  

		//get hash result after compute it  
		byte[] result = md5.Hash;  

		StringBuilder strBuilder = new StringBuilder();  
		for (int i = 0; i < result.Length; i++)  
		{  
			//change it into 2 hexadecimal digits  
			//for each byte  
			strBuilder.Append(result[i].ToString("x2"));  
		}  

		return strBuilder.ToString();  
	}

	public static bool Compare(string NotEncrypted, string Encrypted) {
		string sText = Encrypt (NotEncrypted);
		return (sText == Encrypted);
	}
}

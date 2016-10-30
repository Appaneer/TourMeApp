using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;

public class FBScript : MonoBehaviour {

	public GameObject dialogLoggedIn;
	public GameObject dialogLoggedOut;
	public GameObject dialogUsername;
	public GameObject profPic;
	public string ID;
	void Awake(){

		FB.Init (SetInit, OnHideUnity);
	
	}

	void Update(){

		StartCoroutine (UserImage (ID));
	}

	void SetInit()
	{
		if (FB.IsLoggedIn) {
			Debug.Log ("FB is logged in");
		} else {
			Debug.Log ("FB is NOT logged in");
		}
		DealWithFBMenus (FB.IsLoggedIn);
	}

	void OnHideUnity(bool IsGameShown)
	{
		if (!IsGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void FBLogin()
	{
		List<string> permissions = new List<string> ();
		permissions.Add ("public_profile");

		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	void AuthCallBack(IResult result)
	{
		if (result.Error != null) {
			Debug.Log (result.Error);
		} else {
			if (FB.IsLoggedIn) {
				ID = AccessToken.CurrentAccessToken.UserId;
				//ID = aToken.UserId;
				Debug.Log(ID);
				Debug.Log ("FB is logged in");
			} else {
				Debug.Log ("FB is NOT logged in");
			}

			DealWithFBMenus (FB.IsLoggedIn);
		}
	}

	void DealWithFBMenus(bool isLoggedIn)
	{
		if (isLoggedIn) {
			dialogLoggedIn.SetActive (true);
			dialogLoggedOut.SetActive (false);

			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			//FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfPic);

		} else {
			dialogLoggedIn.SetActive (false);
			dialogLoggedOut.SetActive (true);
		}
	}

	void DisplayUsername(IResult result)
	{
		Text Username = dialogUsername.GetComponent<Text> ();

		if (result.Error == null) {
			Username.text = "Hi there, " + result.ResultDictionary ["first_name"];
		} else {

		}
	}
	 
	IEnumerator UserImage(string userID)
	{
		Image profilePic = profPic.GetComponent<Image> ();
		WWW url = new WWW("https" + "://graph.facebook.com/" + userID+ "/picture?type=square&height=128&width=128"); 
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		yield return url;
		url.LoadImageIntoTexture(textFb2);
		profilePic.sprite = Sprite.Create (textFb2, new Rect (0, 0, 128, 128), new Vector2 ());
		Debug.Log ("loaded");
	}

	void DisplayProfPic(IGraphResult result)
	{

		if (result.Texture != null) {
			Image profilePic = profPic.GetComponent<Image> ();

			profilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		} else {

		}
	}


}

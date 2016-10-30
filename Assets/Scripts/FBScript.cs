using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;

public class FBScript : MonoBehaviour {

	public GameObject dialogLoggedIn;
	public GameObject dialogLoggedOut;
	public GameObject dialogUsername;
	public static Image profPic;
	public static string ID;

	static FBScript instance;

	void Start(){instance = this;
		profPic = GameObject.Find ("fb profile pic").GetComponent<Image> ();
		print (profPic.GetInstanceID());

		FB.Init (SetInit, OnHideUnity);
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

	public static void GetFBProfilPic(Sprite s){
		profPic.sprite = s;
	}

	static IEnumerator UserImage(string userID)
	{
		WWW url = new WWW("https" + "://graph.facebook.com/" + userID+ "/picture?type=large"); 
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		yield return url;
		url.LoadImageIntoTexture(textFb2);
		profPic.sprite = Sprite.Create(textFb2, new Rect(0,0,textFb2.width, textFb2.height), new Vector2(0.5f, 0.5f));
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

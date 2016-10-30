﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Manager : MonoBehaviour {
	public InputField fullNameIF;
	public InputField emailIF;
	public InputField phoneNumberIF;
	public InputField cityTourGuideIF;
	public InputField blurIF;
	public InputField cityIF;

	public Canvas mainCanvas;
	public Canvas registrationCanvas;
	public Canvas loginCanvas;
	public Canvas landingCanvas;
	public Canvas tourGuideRegistrationCanvas;
	public Canvas profileCanvas;

	public Text cityTitleText;
	public Text[] guidesText;

	public Text profileName;
	public Text profileEmail;
	public Text profilePhone;
	public Text profileBlurb;

	// Use this for initialization
	void Start () {
		//OpenLoginPage ();
	}

	public void OpenLoginPage(){
	//	mainCanvas.enabled = false;
		//registrationCanvas.enabled = false;
		//loginCanvas.enabled = true;
		//landingCanvas.enabled = false;
		//tourGuideRegistrationCanvas.enabled = false;
		//profileCanvas.enabled = false;
	}

	public void OpenRegisterCanvas(){
		//mainCanvas.enabled = false;
		//registrationCanvas.enabled = true;
		//loginCanvas.enabled = false;
		//landingCanvas.enabled = false;
		//tourGuideRegistrationCanvas.enabled = false;
		//profileCanvas.enabled = false;
	}

	public void OpenLandingCanvas(){
		//mainCanvas.enabled = false;
		//registrationCanvas.enabled = false;
		//loginCanvas.enabled = false;
		//landingCanvas.enabled = true;
		//tourGuideRegistrationCanvas.enabled = false;
		//profileCanvas.enabled = false;
	}

	public void OpenMainCanvas(){
		cityTitleText.text = cityIF.text;
		StartCoroutine (GetGuides (cityTitleText.text));
		//mainCanvas.enabled = true;
		//registrationCanvas.enabled = false;
		//loginCanvas.enabled = false;
		//landingCanvas.enabled = false;
		//tourGuideRegistrationCanvas.enabled = false;
		//profileCanvas.enabled = false;
	}

	public void OpenTourGuideRegisterCanvas(){
		//mainCanvas.enabled = false;
		//registrationCanvas.enabled = false;
		//loginCanvas.enabled = false;
		//landingCanvas.enabled = false;
		//tourGuideRegistrationCanvas.enabled = true;
		//profileCanvas.enabled = false;
	}

	public void OpenProfileCanvas(){
	//	mainCanvas.enabled = false;
		//registrationCanvas.enabled = false;
		//loginCanvas.enabled = false;
		//landingCanvas.enabled = false;
		//tourGuideRegistrationCanvas.enabled = false;
		//profileCanvas.enabled = true;
	}

	public void PostAsTourGuide(){
		StartCoroutine (AddGuide(fullNameIF.text, emailIF.text, phoneNumberIF.text, cityTourGuideIF.text, blurIF.text));
		//mainCanvas.enabled = false;
		//registrationCanvas.enabled = false;
		//loginCanvas.enabled = false;
		//landingCanvas.enabled = true;
		//ourGuideRegistrationCanvas.enabled = false;
		//profileCanvas.enabled = false;
	}

	public void GetProfile(Text name){
		profileName.text = name.text.Trim();
		StartCoroutine (GetProfileFromDB(name.text.Trim()));
	}

	public void register()
	{	
		OpenLoginPage ();
	}

	IEnumerator AddGuide(string name, string email, string phone, string city, string blur){
		string url = "http://xavieriscool.web44.net/post.php?" + "name=" + WWW.EscapeURL (name.Trim ())
			+ "&email=" + WWW.EscapeURL (email.Trim ())
			+ "&phone=" + WWW.EscapeURL (phone.Trim ())
			+ "&city=" + WWW.EscapeURL (city.Trim ())
			+ "&blur=" + WWW.EscapeURL (blur.Trim ());

		WWW postGuide = new WWW (url);
		yield return postGuide;

		if (postGuide.error != null) {
			Debug.LogError ("Error: " + postGuide.error);
		} 
	}

	IEnumerator GetGuides(string cityName){
		string url = "http://xavieriscool.web44.net/getGuides.php?" + "city=" + WWW.EscapeURL (cityName.Trim ());
		WWW getGuides = new WWW (url);
		yield return getGuides;

		if (getGuides.error != null) {
			Debug.LogError ("Error: " + getGuides.error);
		} 
		else {
			string[] guides = getGuides.text.Substring(0, getGuides.text.LastIndexOf('%') - 1).Split('=');
			for(int i = 0; i < Mathf.Min(guides.Length, guidesText.Length); i++){
				guidesText [i].text = guides [i];
				guidesText [i].GetComponentInChildren<Image> ().enabled = true;
			}
		}
	}

	IEnumerator GetProfileFromDB(string guideName){
		string url = "http://xavieriscool.web44.net/getProfile.php?" + "name=" + WWW.EscapeURL (guideName.Trim ());
		WWW getProfile = new WWW (url);
		yield return getProfile;

		if (getProfile.error != null) {
			Debug.LogError ("Error: " + getProfile.error);
		} 
		else {
			print (getProfile.text);
			string[] guides = getProfile.text.Substring (0, getProfile.text.LastIndexOf ('%') - 1).Split ('=');
			profileEmail.text = "Email: " + guides [0].Trim ();
			profilePhone.text = "Phone #: " + guides [1].Trim ();
			profileBlurb.text = guides [2].Trim ();
		}
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Manager : MonoBehaviour {
	public InputField fullNameIF;
	public InputField emailIF;
	public InputField phoneNumberIF;
	public InputField cityIF;

	public string fullName;
	public string email;
	public string phoneNumber;
	public string getTourGuideURL="http://www.xavieriscool.web44.net/get.php?";

	public Canvas mainCanvas;
	public Canvas registrationCanvas;
	public Canvas loginCanvas;
	public Canvas landingCanvas;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenRegisterCanvas(){
		mainCanvas.enabled = false;
		registrationCanvas.enabled = true;
		loginCanvas.enabled = false;
		landingCanvas.enabled = false;
	}

	public void OpenLandingCanvas(){
		mainCanvas.enabled = false;
		registrationCanvas.enabled = false;
		loginCanvas.enabled = false;
		landingCanvas.enabled = true;
	}

	public void register()
	{
		fullName = fullNameIF.text;
		email = emailIF.text;
		phoneNumber = phoneNumberIF.text;
		mainCanvas.enabled = false;
		registrationCanvas.enabled = false;
		landingCanvas.enabled = false;
		loginCanvas.enabled = true;
	}


	IEnumerator GetStudentsInClass(string cityName){
		string getURL = getTourGuideURL+"city="+cityName;

		WWW getGuides = new WWW (getURL);
		yield return getGuides;

		if (getGuides.error != null) {
			Debug.LogError ("Error: " + getGuides.error);
		} 
		else{
			//getGuides.text;
		}
	}


}

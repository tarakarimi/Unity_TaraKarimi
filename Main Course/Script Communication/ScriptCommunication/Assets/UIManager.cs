using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text name, age, location, gender, favoriteCompany;
	private Name _name;
	private AGL _agl;
	private Company _company;

	private void Start()
	{
		_name = GetComponent<Name>();
		_agl = GameObject.Find("Main Camera").GetComponent<AGL>();
		_company = GameObject.FindWithTag("Company").GetComponent<Company>();
		name.text = "Name: " + _name.name;
		age.text = "Age: " + _agl.age;
		location.text = "location: " + _agl.location;
		gender.text = "gender: " + _agl.gender;
		favoriteCompany.text = "Favorite Company: " + _company.favoriteCompany;
	}
}

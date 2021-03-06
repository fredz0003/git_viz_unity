﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeBehavior : MonoBehaviour {

	private float swayTime = 1;
	private	float speed = 0.8f;
	private	float rotationSpeed = 1;
	private	int rotDir = 1;
	private	LineRenderer line;
	
	public bool	leaf = false;
	public string myPath = null;
	public string parentName = null;
	public GameObject parent = null;
	public List<Transform> myKids;
	public Vector3 centerOfChildren;
	public Vector3 desiredPos;
	



	public string GetNodeFilepath(){
		return this.myPath;
	}




	public void Start(){
		if(!leaf){
			StartCoroutine(Sway());
		} 
		centerOfChildren = Random.onUnitSphere;
		centerOfChildren = new Vector3(centerOfChildren.x, Mathf.Abs(centerOfChildren.y), centerOfChildren.z);
		centerOfChildren.Normalize();
		centerOfChildren *= 4;
	}




	public void Awake(){
		desiredPos = transform.localPosition;
		line = GetComponent<LineRenderer>();
	}

	


	private IEnumerator Sway(){
		float swayTimer = 0;
		while(Application.isPlaying){
			while(swayTimer < swayTime){
				transform.RotateAround(transform.position, transform.forward, rotationSpeed*Time.deltaTime);
				swayTimer += Time.deltaTime;
				yield return null;
			}
			while(swayTimer > 0){
				transform.RotateAround(transform.position, transform.forward, -rotationSpeed*Time.deltaTime);
				swayTimer -= Time.deltaTime;
				yield return null;
			}
		}
	}
		



	// update function moves node towards desired pos - creates sway/wind effect
	public void Update(){
		if(parent != null){
			line.SetPosition(0, parent.transform.position);
			line.SetPosition(1, transform.position);
		}

		Vector3 dir = (desiredPos - transform.localPosition);
		if(dir.sqrMagnitude < 0.1) return;
		dir.Normalize();
		if(Vector3.Distance(desiredPos, transform.localPosition) < speed * Time.deltaTime){
			transform.localPosition = desiredPos;
		}
		else{
			transform.localPosition += dir * speed * Time.deltaTime;
		}
	}

}
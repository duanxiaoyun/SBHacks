﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HighlightChildrenScript), typeof(Rigidbody), typeof(Collider))]
public class ViveTurn : MonoBehaviour {

  public ParticleSystem system;

  public bool isFireKnob = true;

  private float rotVal = 0;
  public float scale = 0.3f;

  public Vector3 turnAxes;

  Vector3 storedRotation;

  public bool createDialogue;
  private int numTimesPickedUp = 0;
  public string firstTouchDialogue;

  public bool isTool;
  public bool isActive;

  public float soundThreshold = .5f;

  public void onHover() {
    GetComponent<HighlightChildrenScript>().makeTransparent();
  }

  public void onHoverLeave() {
    GetComponent<HighlightChildrenScript>().makeOpaque();

  }

  public void onGrab() {
    numTimesPickedUp++;
    GetComponent<HighlightChildrenScript>().makeOpaque();

    storedRotation = this.transform.eulerAngles;

  }

  public void UpdateRotation(Vector3 delta) {

    delta.x *= turnAxes.x;
    delta.y *= turnAxes.y;
    delta.z *= turnAxes.z;

    Vector3 rotChange = delta;

    this.transform.eulerAngles += rotChange;

    float prevRotVal = rotVal;

    if(delta.x > 0 || delta.y > 0 || delta.z > 0) {

      rotVal += scale;
    }

    else if(delta.x < 0 || delta.x < 0 || delta.z < 0) {

      rotVal -= scale;

    }

    if (isFireKnob)
      system.startSpeed = rotVal;
    if(!isFireKnob) {

      system.emissionRate = rotVal;

    }

    if (rotVal <= 0) {

      rotVal = 0;
      system.Stop();

    }

    if (prevRotVal == 0 && rotVal != 0) {

      Debug.Log("HELLO");
      system.Play();

    }

  }

  public void OnCollisionEnter(Collision other) {
    //Debug.Log(other.gameObject.name);
    /*
    if (Vector3.Magnitude(GetComponent<Rigidbody>().velocity) > soundThreshold)
    {
        GetComponent<AudioSource>().Play();
    }*/
  }

  public void onRelease() {

  }

}

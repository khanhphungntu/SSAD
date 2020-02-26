﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public Text subject;
    public GameObject[] images;
    private GameObject currentImg;
    private GameObject lastImg;
    public int currentPage;
    public string[] worlds = { "Mathemtics", "Physics", "Programming" };
    private float speed = 20.0f;
    private bool moveCurrent = false;
    private bool moveLast = false;
    private bool right = false;
    private Vector3 lastImgDest;

    private Vector3 currentImgDest = new Vector3(2.1f, 0, 0);
    private Vector3 scaleFactor = new Vector3(0.025f, 0.025f, 0.025f);
    private Vector3 leftEnd = new Vector3(-5f, 0, 0);
    private Vector3 rightEnd = new Vector3(7.5f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        currentPage = 0;
        currentImg = Instantiate(images[0]) as GameObject;
        currentImg.SetActive(true);
        Transform t = currentImg.transform;
        t.SetParent(transform);
        t.localPosition = currentImgDest;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastImg && (Mathf.Abs(lastImg.transform.localPosition.x - lastImgDest.x) == 0))
        {
            Destroy(lastImg);
            moveLast = false;
        }

        if (Mathf.Abs(currentImg.transform.localPosition.x - currentImgDest.x) == 0)
        {
            moveCurrent = false;
        }

        if (moveCurrent)
        {
            currentImg.transform.localPosition = Vector3.MoveTowards(currentImg.transform.localPosition, currentImgDest, Time.deltaTime * speed);
        }

        if (moveLast)
        {
            lastImg.transform.localScale -= scaleFactor;
            lastImg.transform.localPosition = Vector3.MoveTowards(lastImg.transform.localPosition, lastImgDest, Time.deltaTime * speed);
        }

    }

    void SetCurrentPage()
    {
        subject.text = worlds[currentPage];
        lastImg = currentImg;
        currentImg = Instantiate(images[currentPage]) as GameObject;
        currentImg.SetActive(true);

        Transform t = currentImg.transform;
        t.SetParent(transform);

        Vector3 pos = rightEnd;

        if (right)
        {
            pos = leftEnd;
        }

        t.localPosition = pos;
        moveCurrent = true;
        moveLast = true;
    }
    public void OnClickBack() {

        if (moveLast || moveCurrent)
        {
            return;
        }

        if (this.currentPage == 0)
        {
            this.currentPage = 2;
        }
        else
        {
            this.currentPage = this.currentPage - 1;
        }
        lastImgDest = leftEnd;
        right = false;
        SetCurrentPage();
    }

    public void OnClickForward()
    {
        if (moveLast || moveCurrent)
        {
            return;
        }

        this.currentPage = (this.currentPage + 1)%3;
        right = true;
        lastImgDest = rightEnd;
        SetCurrentPage();
    }

    public void OnClickContinue()
    {

    }
}
﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.IO;
//using SkillBridge.Message;
//using ProtoBuf;
//using UnityEngine.Analytics;
using Services;
using Managers;

public class LoadingManager : MonoBehaviour {

	public GameObject UiTips;
	public GameObject UIStart;
	public GameObject UILoading;
	public GameObject UILogin;

	public Slider progressBar;
	public Text progressText;

	// Use this for initialization
	IEnumerator Start()
	{
		log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.xml"));
		UnityLogger.Init();
		Common.Log.Init("Unity");
		Common.Log.Info("LoadingManager start");

		UiTips.SetActive(true);
        UIStart.SetActive(true);
        UILoading.SetActive(false);
        UILogin.SetActive(false);
        yield return new WaitForSeconds(3f);
        UiTips.SetActive(false);
        yield return new WaitForSeconds(1f);
        UILoading.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIStart.SetActive(false);
        yield return DataManager.Instance.LoadData();

        //Init basic services
        MapService.Instance.Init();
        UserService.Instance.Init();

		//其它系统初始化
		//TestManager.Instance.Init();
		ShopManager.Instance.Init();


        //Fake loading Simulate
        for (float i = 10; i < 100;)
		{
			i += Random.Range(0.1f, 1.0f);
			progressBar.value = i;
			progressText.text = ((int)progressBar.value).ToString() + "%"; 
            yield return new WaitForEndOfFrame();
		}

		UILoading.SetActive(false);
		UILogin.SetActive(true);
		yield return null;
    }
	
}

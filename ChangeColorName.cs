using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using MiniJSON;
using System.Runtime.InteropServices; //WebGL jsからcsを呼び出す

public class ChangeColorName : MonoBehaviour{

	private Color smr;
	private SkinnedMeshRenderer Mesh0;
	private Color color;
	private Color colors;
	private int custom;

	[DllImport("__Internal")]
	static extern Inptr Fuga();

	[DllImport("__Internal")]
	static extern Inptr Init(Action<string, int> piyo);

	[MonoPInvokeCallback(typeof(Action<string, int>))]
	static void Piyo(string str, int n){
		Debug.Log("called Piyo:'" + str + "'," + n);
	} 

	void Start(){
		Fuga();
	}

	//髪色変更
	void ChangeColor(string custom_color){

		//変更例
		// string custom_color = "1,yellow";

		//★★★色変更のコード完成
		print("start ready[色変更]");

		//引数を分割
		string[] a = Regex.Split(custom_color,",");

		//変更部位
		custom = int.Parse(a[0]);
		print("set custom"+custom);

		//変更色
		ColorUtility.TryParseHtmlString(a[1].ToString(), out colors);
		print("set color"+colors);

		Mesh0 = GetComponent<SkinnedMeshRenderer>();

		//3Dモデルのマテリアル取得
		Material[] mats = Mesh0.materials;

		//変更クラス
		SetMatColor(mats[custom],colors);
	}

	//色変更クラス
	void SetMatColor(Material mesh,Color col ){
		mesh.color = col;
		
		//拡散色
		// mesh.SetColor("_Color",col);
		// //環境色
		// mesh.SetColor("_AmbColor",col);
		// //反射色
		// mesh.SetColor("_SpecularColor",col);
	}

	void Update(){
	}
}
/* mats[0]---ネクタイ
   mats[1]---髪
   mats[2]---肌
   mats[3]---ソックスと腕の袖周辺
   mats[4]---トップス
   mats[5]---トップスの下のギザギザ部分
   mats[6]---眼球
   mats[7]---ネクタイピンとヘッドホン
   mats[11]---ヘッドホン
   mats[16]---眉毛とギザギザの下側
 */
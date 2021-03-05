using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 會員類型
/// </summary>
public enum Type
{
    尚未加入 = -2,
    直銷商 = 1,
    生活會員 = 2,
}

public enum Source
{
    陌生開發 = 1,
    親友好友介紹 = 2,
}

public class Photos
{
	public List<string> url { get; set; }
}

public class Review
{
	public string author_name { get; set; }
	public string rating { get; set; }
	public string date { get; set; }
	public string text { get; set; }
}

public class POI
{
    public string lat { get; set; }
    public string lng { get; set; }
    public string title { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string url { get; set; }
    public string img { get; set; }
    public string icon { get; set; }
    public JArray tags { get; set; }
}

public class POI2
{
    public string lat { get; set; }
    public string lng { get; set; }
    public string name { get; set; }
    public string formatted_address { get; set; }
    public string formatted_phone_number { get; set; }
    public string website { get; set; }
    public Photos photos { get; set; }
    public List<Review> reviews { get; set; }

    public string sciname { get; set; }
    public string speciesname { get; set; }
    public string introduction { get; set; }
}
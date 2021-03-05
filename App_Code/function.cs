using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;

/// <summary>
/// function 的摘要描述
/// </summary>
public class Function
{
    private static List<double> Pos = new List<double>();

    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);

    /// <summary>
    /// 權限
    /// </summary>
    public enum Privlege
    {
        /// <summary>
        /// 新增權限
        /// </summary>
        新增 = 1,
        /// <summary>
        /// 修改權限
        /// </summary>
        修改 = 2,
        /// <summary>
        /// 刪除權限
        /// </summary>
        刪除 = 4
    }

    /// <summary>
    /// 取得 DataDataContext
    /// </summary>
    /// <returns>DataDataContext</returns>
    public static AmwayDataContext GetDB(bool enableDeferredLoading = true)
    {
        return new AmwayDataContext
        {
            DeferredLoadingEnabled = enableDeferredLoading
        };
    }

    /// <summary>
    /// 產生 SHA-512 的 byte[]
    /// </summary>
    /// <param name="value">輸入的值</param>
    /// <returns>byte[]</returns>
    public static byte[] GenSha512Hash(string value)
    {
        using (SHA512 sHA512 = new SHA512Managed())
        {
            return sHA512.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }

    /// <summary>
    /// 產生密碼的雜湊值字串
    /// </summary>
    /// <param name="password">密碼字串</param>
    /// <returns>密碼的雜湊值字串</returns>
    public static string GenPwdHash(string password)
    {
        return Convert.ToBase64String(GenSha512Hash(password));
    }

    /// <summary>
    /// 驗證密碼是否有效
    /// </summary>
    /// <param name="password">輸入密碼字串</param>
    /// <param name="hashValue">儲存於資料庫內的雜湊值字串</param>
    /// <returns>布林值，True 表示驗證成功；False 則反之</returns>
    public static bool IsPwdValid(string password, string hashValue)
    {
        return GenSha512Hash(password).SequenceEqual(Convert.FromBase64String(hashValue));
    }

    /// <summary>
    /// 產生 HttpResponseMessage
    /// </summary>
    /// <param name="statusCode">HttpStatusCode</param>
    /// <param name="content">文字字串</param>
    /// <returns>HttpResponseMessage</returns>
    public static HttpResponseMessage GetHttpResponseMessage(HttpStatusCode statusCode, string content)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, @"text/plain"),
        };
    }

    /// <summary>
    /// 產生版權宣告的年份字串
    /// </summary>
    /// <param name="startYear">數值，產生版權宣告啟使西元年份</param>
    /// <returns>產生的版權宣告的年份字串</returns>
    public static string GenerateCopyrightYear(int startYear)
    {
        int serverYear = DateTime.Now.Year;
        string result;

        if (startYear > serverYear)
        {
            result = $"{serverYear}-{startYear}";
        }
        else if (startYear == serverYear)
        {
            result = $"{startYear}";
        }
        else
        {
            result = $"{startYear}-{serverYear}";
        }

        return result;
    }

    /// <summary>
    /// 切割逗號分隔到陣列
    /// </summary>
    /// <param name="value">字串</param>
    /// <returns>字串陣列</returns>
    public static string[] SplitToArray(string value)
    {
        return value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// 顯示訊息對話框
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="message">訊息字串</param>
    public static void MsgBox(Page page, string message)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", $"alert('{message}');", true);
    }

    /// <summary>
    /// 顯示訊息對話框並在點選確認按鈕後轉頁至指定網址
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="message">訊息字串</param>
    /// <param name="url">網址字串</param>
    public static void MsgBoxRedirect(Page page, string message, string url)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Alert", $"alert('{message}');window.location='{url}';", true);
    }

    /// <summary>
    /// 將 CheckBoxList 勾選的項目轉換成字串
    /// </summary>
    /// <param name="checkBoxList">CheckBoxList</param>
    /// <returns>字串</returns>
    public static string GetCheckListItem(CheckBoxList checkBoxList)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (ListItem listItem in checkBoxList.Items)
        {
            if (listItem.Selected == true)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append(listItem.Value);
            }
        }

        return stringBuilder.ToString();
    }


    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="page">Page</param>
    public static void Logout(Page page)
    {
        FormsAuthentication.SignOut();

        page.Session.Clear();
        page.Response.Redirect(page.ResolveClientUrl("~/Admin/Login.aspx"));
    }

    /// <summary>
    /// 判斷是否為日期格式
    /// </summary>
    /// <param name="value">字串</param>
    /// <returns>布林值，True 表示為日期格式；False 則反之</returns>
    public static bool IsDate(string value)
    {
        return DateTime.TryParse(value, out _);
    }

    /// <summary>
    /// 判斷是否為數字格式 (double)
    /// </summary>
    /// <param name="value">字串</param>
    /// <returns>布林值，True 表示為數字格式；False 則反之</returns>
    public static bool IsNumeric(string value)
    {
        return double.TryParse(value, out _);
    }

    /// <summary>
    /// 確認檔案是否存在
    /// </summary>
    /// <param name="filePath">檔案路徑</param>
    /// <returns>布林值，True 表示檔案存在；False 則反之</returns>
    public static bool IsFileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// 取得年份
    /// </summary>
    public static int GetYear()
    {
        DateTime DT = DateTime.Now;
        return DT.Year;
    }

    /// <summary>
    /// 轉換英文月份
    /// </summary>
    /// <param name="filePath">檔案路徑</param>
    /// <returns>布林值，True 表示檔案存在；False 則反之</returns>
    public static string ChangMonthTWtoENG(string Month)
    {
        string month = string.Empty;
        switch (Month)
        {
            case "1":
                month = "Mon";
                break;
            case "2":
                month = "Tue";
                break;
            case "3":
                month = "Wed";
                break;
            case "4":
                month = "Thu";
                break;
            case "5":
                month = "Fri";
                break;
            case "6":
                month = "Sat";
                break;
            case "7":
                month = "Sun";
                break;
        }
        return month;
    }

    /// <summary>
    /// 自動轉正圖片
    /// </summary>
    /// <param name="img">image</param>
    /// <param name="imgpath">存檔path與檔名</param>
    public static void RotateImage(System.Drawing.Image img, string imgpath)
    {
        PropertyItem[] exif = img.PropertyItems;
        byte orientation = 0;
        foreach (PropertyItem i in exif)
        {
            if (i.Id == 274)
            {
                orientation = i.Value[0];
                i.Value[0] = 1;
                img.SetPropertyItem(i);
            }
        }

        switch (orientation)
        {
            case 2:
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                break;
            case 3:
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                break;
            case 4:
                img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                break;
            case 5:
                img.RotateFlip(RotateFlipType.Rotate90FlipX);
                break;
            case 6:
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                break;
            case 7:
                img.RotateFlip(RotateFlipType.Rotate270FlipX);
                break;
            case 8:
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                break;
            default:
                return;
        }

        foreach (PropertyItem i in exif)
        {
            if (i.Id == 40962)
            {
                i.Value = BitConverter.GetBytes(img.Width);
            }
            else if (i.Id == 40963)
            {
                i.Value = BitConverter.GetBytes(img.Height);
            }
            img.SetPropertyItem(i);
        }

        #region Configure JPEG Compression Engine

        System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();

        long[] quality = new long[1];

        quality[0] = 100;

        System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

        encoderParams.Param[0] = encoderParam;

        System.Drawing.Imaging.ImageCodecInfo[] arrayICI = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

        System.Drawing.Imaging.ImageCodecInfo jpegICI = null;

        for (int x = 0; x < arrayICI.Length; x++)
        {

            if (arrayICI[x].FormatDescription.Equals("JPEG"))
            {

                jpegICI = arrayICI[x];

                break;

            }

        }
        #endregion

        img.Save(imgpath, jpegICI, encoderParams);
    }

    /// <summary>
    /// 設定抬頭名稱
    /// </summary>
    /// <param name="Url">網頁路徑</param>
    /// <returns name=""
    public static string SetTitle(string Url)
    {
        if (Url.Contains("_Edit"))
            Url = Url.Replace("_Edit", "");

        var db = GetDB();
        string Template = @"</asp:Literal> <i class='[ICON]'></i>&nbsp;[NAME]",
               Result = string.Empty;

        var Data = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.URL.Contains(Url));
        if (Data != null)
        {
            Result = Template.Replace("[ICON]", Data.Icon).Replace("[NAME]", Data.Name);
        }
        return Result;
    }

    /// <summary>
    /// 取得網頁名稱
    /// </summary>
    /// <param name="pageFilePath">網頁路徑</param>
    /// <returns name=""
    public static string GetPageName(string pageFilePath)
    {
        using (var db = GetDB())
        {
            string PageName = string.Empty;

            if (pageFilePath.Contains("_Edit"))
            {
                pageFilePath = pageFilePath.Replace("_Edit", "");
            }

            var vBackendMenu = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.URL.Contains(pageFilePath));

            if (vBackendMenu != null)
                PageName = vBackendMenu.Name;
            else
                PageName = "";

            return PageName;
        }
    }


    /// <summary>
    /// 寫入操作紀錄。
    /// </summary>
    /// <param name="mS_Action">行為代號。</param>
    /// <param name="mS_TableName">資料表名稱。</param>
    /// <param name="mS_Memo">備註。</param>
    /// <param name="mG_SystemUserID">執行操作的使用者的 ID。</param>
    public static void InsertLog(string mS_Action, string mS_PageName, string mS_TableName, string mS_Memo, int mG_SystemUserID)
    {
        using (var db = GetDB())
        {
            SystemLog _SystemLog = new SystemLog()
            {
                Action = mS_Action,
                PageName = mS_PageName,
                TableName = mS_TableName,
                Memo = mS_Memo,
                SystemUserID = mG_SystemUserID,
                CreateDate = DateTime.Now,
            };
            db.SystemLog.InsertOnSubmit(_SystemLog);
            db.SubmitChanges();
        }
    }

    /// <summary>
    /// 取得兩個字串的相似度
    /// </summary>
    /// <param name=”sourceString”>第一個字串</param>
    /// <param name=”str”>第二個字串</param>
    /// <returns></returns>
    public static decimal GetSimilarityWith(string sourceString, string str)
    {

        decimal Kq = 2;
        decimal Kr = 1;
        decimal Ks = 1;

        char[] ss = sourceString.ToCharArray();
        char[] st = str.ToCharArray();

        //或許交集數量
        int q = ss.Intersect(st).Count();
        int s = ss.Length - q;
        int r = st.Length - q;

        return Kq * q / (Kq * q + Kr * r + Ks * s);
    }


    /// <summary>
    /// 編輯距離（Levenshtein Distance）
    /// </summary>
    /// <param name="source">源串</param>
    /// <param name="target">目標串</param>
    /// <param name="similarity">輸出：相似度，值在0～１</param>
    /// <param name="isCaseSensitive">是否大小寫敏感</param>
    /// <returns>源串和目標串之間的編輯距離</returns>
    public static Int32 LevenshteinDistance(String source, String target, out Double similarity, Boolean isCaseSensitive = false)
    {
        if (String.IsNullOrEmpty(source))
        {
            if (String.IsNullOrEmpty(target))
            {
                similarity = 1;
                return 0;
            }
            else
            {
                similarity = 0;
                return target.Length;
            }
        }
        else if (String.IsNullOrEmpty(target))
        {
            similarity = 0;
            return source.Length;
        }

        String From, To;
        if (isCaseSensitive)
        {   // 大小寫敏感
            From = source;
            To = target;
        }
        else
        {   // 大小寫無關
            From = source.ToLower();
            To = target.ToLower();
        }

        // 初始化
        Int32 m = From.Length;
        Int32 n = To.Length;
        Int32[,] H = new Int32[m + 1, n + 1];
        for (Int32 i = 0; i <= m; i++) H[i, 0] = i;  // 注意：初始化[0,0]
        for (Int32 j = 1; j <= n; j++) H[0, j] = j;

        // 迭代
        for (Int32 i = 1; i <= m; i++)
        {
            Char SI = From[i - 1];
            for (Int32 j = 1; j <= n; j++)
            {   // 刪除（deletion） 插入（insertion） 替換（substitution）
                if (SI == To[j - 1])
                    H[i, j] = H[i - 1, j - 1];
                else
                    H[i, j] = Math.Min(H[i - 1, j - 1], Math.Min(H[i - 1, j], H[i, j - 1])) + 1;
            }
        }

        // 計算相似度
        Int32 MaxLength = Math.Max(m, n);   // 兩字串的最大長度
        similarity = ((Double)(MaxLength - H[m, n])) / MaxLength;

        return H[m, n];    // 編輯距離
    }

    /// <summary>
    /// 回傳的數據和個人使用接近，但回傳單位是"公里"
    /// <para>出處：http://windperson.wordpress.com/2011/11/01/由兩點經緯度數值計算實際距離的方法/ </para>
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lng1"></param>
    /// <param name="lat2"></param>
    /// <param name="lng2"></param>
    /// <returns>回傳距離公里</returns>
    public static double DistanceOfTwoPoints(double lat1, double lng1, double lat2, double lng2)
    {
        double radLng1 = lng1 * Math.PI / 180.0;
        double radLng2 = lng2 * Math.PI / 180.0;
        double a = radLng1 - radLng2;
        double b = (lat1 - lat2) * Math.PI / 180.0;
        double Km = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLng1) * Math.Cos(radLng2) * Math.Pow(Math.Sin(b / 2), 2))) * 6378.137;

        Km = Math.Round(Km * 10000) / 10000;

        return Km;
    }

    /// <summary>
    /// 獲取客戶端IP及MAC地址
    /// </summary>
    /// <param name="IP">IP位址</param>
    /// <param name="MAC">MAC地址</param>
    public static void GetIPandMac(out string IP, out string MAC)
    {
        try
        {
            string UserIP = HttpContext.Current.Request.UserHostAddress;
            string strClientIP = HttpContext.Current.Request.UserHostAddress.ToString().Trim();
            Int32 ldest = inet_addr(strClientIP); //目的地的ip
            Int32 lhost = inet_addr("");   //本地伺服器的ip
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");
            if (mac_src == "0")
            {
                IP = UserIP;
                MAC = "0";
            }

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string Mac_dest = "";
            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        Mac_dest = Mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        Mac_dest = "-" + Mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }
            IP = UserIP;
            MAC = Mac_dest;
        }
        catch
        {
            IP = "";
            MAC = "";
        }
    }

    /// <summary>
    /// 轉換成弧度 
    /// <para>來源：https://stackoverflow.com/a/29883398 </para>
    /// </summary>
    /// <param name="angle">角度</param>
    /// <returns>弧度數值</returns>
    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }

    //度分秒轉經緯度 
    public static string GPSToLatLng(string Value)
    {
        string result = "";
        Value = Value.Trim();
        //必須有(度/分/秒)才可進行轉換     
        if (Value.IndexOf('°') != -1 && Value.IndexOf('\'') != -1 && Value.IndexOf('\"') != -1)
        {
            double degree, minute, second;
            //取得(度/分/秒)         
            if (double.TryParse(Value.Split('°')[0], out degree) &&
                double.TryParse(Value.Split('°')[1].Split('\'')[0], out minute) &&
                double.TryParse(Value.Split('°')[1].Split('\'')[1].Split('\"')[0], out second)
            )
            {
                //x度 y分 z秒 = x + y/60 + z/3600 度              
                result = (degree + (minute / 60) + (second / 3600)).ToString();
            }
        }
        return result;
    }

    public static string GetCellValue(ICell iCell)
    {
        string resultStr = string.Empty;

        switch (((XSSFCell)iCell).CellType)
        {
            case CellType.Blank:
                resultStr = ((XSSFCell)iCell).GetRawValue();
                break;
            case CellType.Boolean:
                resultStr = ((XSSFCell)iCell).BooleanCellValue.ToString();
                break;
            case CellType.Error:
                resultStr = ((XSSFCell)iCell).ErrorCellString;
                break;
            case CellType.Formula:
                resultStr = ((XSSFCell)iCell).CellFormula;
                break;
            case CellType.Numeric:
                resultStr = ((XSSFCell)iCell).NumericCellValue.ToString();
                break;
            case CellType.String:
                resultStr = ((XSSFCell)iCell).StringCellValue;
                break;
            case CellType.Unknown:
                resultStr = ((XSSFCell)iCell).GetRawValue();
                break;
            default:
                resultStr = ((XSSFCell)iCell).GetRawValue();
                break;
        }

        return resultStr;
    }

    private string CheckValue(string str)
    {
        if (str != "無")
            return str;
        else
            return "";
    }

    /// <summary>
    /// 過濾特殊字符，字母，數字，和-
    /// </summary>
    /// <param name="inputValue">輸入字符串</param>
    /// <remarks>發件和收件詳細地址有這種情況：“倉場路40-73號迎園新村四坊69號202室”，這種帶有-的特殊字符不需要過濾掉</remarks>
    /// <returns></returns>
    public static string FilterChar(string inputValue)
    {
        // return Regex.Replace(inputValue, "[`~!@#$^&*()=|{}‘:;‘,\\[\\].<>/?~！@#￥……&*（）&mdash;|{}【】；‘’，。/*-+]+", "", RegexOptions.IgnoreCase);
        if (Regex.IsMatch(inputValue, "[A-Za-z0-9\u4e00-\u9fa5-]+"))
        {
            return Regex.Match(inputValue, "[A-Za-z0-9]+").Value;
        }
        return "";
    }
}
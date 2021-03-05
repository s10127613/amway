using System.Web.UI;

/// <summary>
/// SA2Util
/// <para>需搭配 SweetAlert2 (https://sweetalert2.github.io/) 使用。</para>
/// <para>※僅支援 v8.x 版(含)以上版本的 SweetAlert2。</para>
/// </summary>
public class SA2Util
{
    /// <summary>
    /// 彈出類型的值
    /// </summary>
    public enum PopupType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 錯誤
        /// </summary>
        Error = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 3,
        /// <summary>
        /// 訊息
        /// </summary>
        Info = 4,
        /// <summary>
        /// 問號
        /// </summary>
        Question = 5,
    }

    #region 沒有標題

    /// <summary>
    /// 使用 SweetAlert2 顯示沒有標題但帶有確認按鈕的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="text">文字訊息</param>
    /// <param name="useHtml">判斷是否使用 HTML</param> 
    public static void ShowMsg(Page page, string text, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                {textOrHtml},
                confirmButtonText: '確認',
                confirmButtonAriaLabel: '確認按鈕'
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示沒有標題且自動關閉的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="text">文字訊息</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string text, int milliseconds, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                {textOrHtml},
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    // 不做任何事。
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示沒有標題且自動關閉的訊息並轉頁至指定網址
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="text">文字訊息</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="url">網址</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string text, int milliseconds, string url, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                {textOrHtml},
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    window.location='{url}';
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示沒有標題但自訂彈出類型且帶有確認按鈕的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string text, PopupType popupType, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                {textOrHtml},
                type: '{popupType.ToString().ToLower()}',
                confirmButtonText: '確認',
                confirmButtonAriaLabel: '確認按鈕'
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示沒有標題但自訂彈出類型且自動關閉的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string text, PopupType popupType, int milliseconds, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                {textOrHtml},
                type: '{popupType.ToString().ToLower()}',
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    // 不做任何事。
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示沒有標題但自訂彈出類型且自動關閉的訊息並轉頁至指定網址
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="url">網址</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string text, PopupType popupType, int milliseconds, string url, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                {textOrHtml},
                type: '{popupType.ToString().ToLower()}',
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    window.location='{url}';
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    #endregion

    #region 有標題

    /// <summary>
    /// 使用 SweetAlert2 顯示帶有確認按鈕的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="titleText">標題文字</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    public static void ShowMsg(Page page, string titleText, string text, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                titleText: '{titleText}',
                {textOrHtml},
                confirmButtonText: '確認',
                confirmButtonAriaLabel: '確認按鈕'
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示自動關閉的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="titleText">標題文字</param>
    /// <param name="text">文字訊息</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string titleText, string text, int milliseconds, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                titleText: '{titleText}',
                {textOrHtml},
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    // 不做任何事。
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示自動關閉的訊息並轉頁至指定網址
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="titleText">標題文字</param>
    /// <param name="text">文字訊息</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="url">網址</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string titleText, string text, int milliseconds, string url, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                titleText: '{titleText}',
                {textOrHtml},
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    window.location='{url}';
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示自訂彈出類型且帶有確認按鈕的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="titleText">標題文字</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string titleText, string text, PopupType popupType, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                titleText: '{titleText}',
                {textOrHtml},
                type: '{popupType.ToString().ToLower()}',
                confirmButtonText: '確認',
                confirmButtonAriaLabel: '確認按鈕'
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示自訂彈出類型且自動關閉的訊息
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="titleText">標題文字</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string titleText, string text, PopupType popupType, int milliseconds, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                titleText: '{titleText}',
                {textOrHtml},
                type: '{popupType.ToString().ToLower()}',
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    // 不做任何事。
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    /// <summary>
    /// 使用 SweetAlert2 顯示自訂彈出類型且自動關閉的訊息並轉頁至指定網址
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="titleText">標題文字</param>
    /// <param name="text">文字訊息</param>
    /// <param name="popupType">PopupType 的值</param>
    /// <param name="milliseconds">等待的毫秒數</param>
    /// <param name="url">網址</param>
    /// <param name="useHtml">判斷是否使用 HTML</param>
    public static void ShowMsg(Page page, string titleText, string text, PopupType popupType, int milliseconds, string url, bool useHtml = false)
    {
        string textOrHtml;

        if (useHtml)
        {
            textOrHtml = $"html: '{text}'";
        }
        else
        {
            textOrHtml = $"text: '{text}'";
        }

        string jsTemplate = $@"
            Swal.fire({{
                titleText: '{titleText}',
                {textOrHtml},
                type: '{popupType.ToString().ToLower()}',
                timer: {milliseconds},
                showConfirmButton: false,
                onClose: () => {{
                    window.location='{url}';
                }}
            }});
        ";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "Swal", jsTemplate, true);
    }

    #endregion
}
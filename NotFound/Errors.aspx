<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Errors.aspx.cs" Inherits="Errors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系統錯誤</title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <style>
        body {
            background: #dedede;
        }

        .page-wrap {
            min-height: 100vh;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-wrap d-flex flex-row align-items-center">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-12 text-center">
                        <span class="display-1 d-block">系統錯誤</span>
                        <div class="mb-4 lead">請聯絡該網站的系統管理員</div>
<%--                        <a href="<%= ResolveClientUrl("~/Default/Index.aspx") %>" class="btn btn-link">回首頁</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

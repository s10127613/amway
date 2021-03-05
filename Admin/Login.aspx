<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html>
<html lang="zh-Hant-TW">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="keyword" content="">
    <meta name="author" content="" />
    <!-- Page Title -->
    <title>Amway | 個人行動寶典</title>
    <!-- Main CSS -->
    <link type="text/css" rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="assets/plugins/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="assets/plugins/simple-line-icons/css/simple-line-icons.css">
    <link type="text/css" rel="stylesheet" href="assets/plugins/ionicons/css/ionicons.css">
    <link type="text/css" rel="stylesheet" href="assets/css/app.min.css" />
    <link type="text/css" rel="stylesheet" href="assets/css/style.min.css" />
    <!-- Favicon -->
    <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon">
</head>
<body>
    <form method="post" action="Login.aspx" runat="server" autocomplete="off">
        <!--================================-->
        <!-- User Singup Start -->
        <!--================================-->
        <div class="ht-100v d-flex">
            <div class="card shadow-none pd-20 mx-auto wd-280 text-center bd-transparent align-self-center">
                <h4 class="card-title mt-3 text-center">登入</h4>
                <p class="text-center">請輸入登入資訊</p>
                <div class="form-group input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"><i class="fa fa-user"></i></span>
                    </div>
                    <asp:TextBox ID="TbAccount" CssClass="form-control form-control-sm" autocomplete="off" runat="server" placeholder="請輸入帳號"></asp:TextBox>
                </div>
                <div class="form-group input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"><i class="fa fa-lock"></i></span>
                    </div>
                    <asp:TextBox ID="TbPassword" CssClass="form-control form-control-sm" TextMode="Password" autocomplete="off" runat="server" placeholder="請輸入密碼"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:LinkButton ID="LBtnLogin" class="btn btn-custom-primary btn-block tx-13" OnClick="LBtnLogin_Click" runat="server">       登入 </asp:LinkButton>
                </div>
            </div>
        </div>
        <!--/ User Singup End -->
    </form>
    <!--================================-->
    <!-- Footer Script -->
    <!--================================-->
    <script src="assets/plugins/jquery/jquery.min.js"></script>
    <script src="assets/plugins/jquery-ui/jquery-ui.js"></script>
    <script src="assets/plugins/popper/popper.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/plugins/pace/pace.min.js"></script>
    <script src="assets/js/jquery.slimscroll.min.js"></script>
    <script src="assets/js/custom.js"></script>
    <!-- Footer Script End -->
</body>
</html>

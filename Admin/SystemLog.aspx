<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="SystemLog.aspx.cs" Inherits="Admin_SystemLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        th {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">

    <!--================================-->
    <!-- dataTable Start -->
    <!--================================-->
    <div class="col-md-12 col-lg-12">
        <div class="card mg-b-20">
            <div class="card-header">
                <h4 class="card-header-title">
                    <asp:Literal ID="LTitle" runat="server"></asp:Literal>
                </h4>
                <div class="card-header-btn">
                    <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse5" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                </div>
            </div>
            <div class="card-body collapse show" id="collapse5">
                <div class="row">
                    <div class="col-md-3">
                        <p>動作</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddl_Action" runat="server" CssClass="form-control">
                                <asp:ListItem Value="-1" Selected="True">請選擇…</asp:ListItem>
                                <asp:ListItem Value="Create">新增</asp:ListItem>
                                <asp:ListItem Value="Update">修改</asp:ListItem>
                                <asp:ListItem Value="Delete">刪除</asp:ListItem>
                                <asp:ListItem Value="Login">登入</asp:ListItem>
                                <asp:ListItem Value="Logout">登出</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>位置</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddl_PageName" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>使用者</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddl_User" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>說明</p>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="mTB_Memo" runat="server" CssClass="form-control" placeholder="請輸入中文名稱"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-6 mg-b-30">
                        <p>篩選</p>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:LinkButton ID="LBSearch" runat="server" CssClass="btn btn-primary btn-block mg-b-10" OnClick="LBSearch_Click"><i class="fa fa-search"></i>&nbsp;查詢</asp:LinkButton>
                            </div>
                            <div class="col-md-6">
                                <asp:LinkButton ID="LBClean" runat="server" CssClass="btn btn-danger btn-block mg-b-10" OnClick="LBClean_Click"><i class="fa fa-trash-o"></i>&nbsp;清除</asp:LinkButton>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-12 mg-b-30">
                        <p>系統日誌資料列表</p>
                        <asp:ListView ID="LVLog" runat="server" OnItemDataBound="LVLog_ItemDataBound" OnPagePropertiesChanging="LVLog_PagePropertiesChanging">
                            <LayoutTemplate>
                                <table id="hoverTable" class="table hover responsive nowrap">
                                    <thead>
                                        <tr>
                                            <th>動作</th>
                                            <th>位置</th>
                                            <th>說明</th>
                                            <th>使用者名稱</th>
                                            <th>建立時間</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </tbody>
                                </table>
                                <div style="text-align: center">
                                    <asp:DataPager ID="DataPager1" PagedControlID="LVLog" PageSize="10" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ShowNextPageButton="false" ButtonCssClass="fg-button ui-button ui-state-default" />
                                            <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="fg-button ui-button ui-state-default" CurrentPageLabelCssClass="fg-button ui-button ui-state-primary next" NextPreviousButtonCssClass="fg-button ui-button ui-state-default" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <table id="hoverTable" class="table hover responsive nowrap">
                                    <thead>
                                        <tr>
                                            <th>動作</th>
                                            <th>位置</th>
                                            <th>說明</th>
                                            <th>使用者名稱</th>
                                            <th>建立時間</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-center" colspan="5">無資料</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="text-center">
                                        <asp:Label ID="LAction" Text='<%# Eval("Action") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LPageName" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-left">
                                        <asp:Label ID="LMemo" Text='<%# Eval("Memo") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LSystemUser" Text='<%# Eval("SystemUserID") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LCreateDate" Text='<%# Eval("CreateDate") %>' runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/ dataTable End -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>


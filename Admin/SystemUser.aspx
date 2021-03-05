<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="SystemUser.aspx.cs" Inherits="Admin_SystemUser" %>

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
                        <p>群組</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddl_Role" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>使用者名稱</p>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="mTB_Name" runat="server" CssClass="form-control" placeholder="請輸入使用者名稱"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>帳號</p>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="mTB_Account" runat="server" CssClass="form-control" placeholder="請輸入使用者帳號"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>帳號狀態</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddl_Enable" runat="server" CssClass="form-control">
                                <asp:ListItem Value="-1" Selected="True">請選擇…</asp:ListItem>
                                <asp:ListItem Value="true">啟用</asp:ListItem>
                                <asp:ListItem Value="false">未啟用</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-6 mg-b-30">
                        <p>篩選</p>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:LinkButton ID="Lbtn_Search" runat="server" CssClass="btn btn-primary btn-block mg-b-10" OnClick="Lbtn_Search_Click"><i class="fa fa-search"></i>&nbsp;查詢</asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="Lbtn_SearchClear" runat="server" CssClass="btn btn-danger btn-block mg-b-10" OnClick="Lbtn_SearchClear_Click"><i class="fa fa-trash-o"></i>&nbsp;清除</asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="LBtn_AddData" runat="server" CssClass="btn btn-success btn-block mg-b-10" OnClick="LBtn_AddData_Click"><i class="fa fa-plus"></i>&nbsp;新增</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 mg-b-30">
                        <p>顧客列表</p>

                        <asp:ListView ID="LvAccount" runat="server" OnItemDataBound="LvAccount_ItemDataBound" OnPagePropertiesChanging="LvAccount_PagePropertiesChanging">
                            <LayoutTemplate>
                                <table id="hoverTable" class="table hover responsive nowrap">
                                    <thead>
                                        <tr>
                                            <th>是否啟用</th>
                                            <th>使用者名稱</th>
                                            <th>帳號</th>
                                            <th>描述</th>
                                            <th>操作</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </tbody>
                                </table>
                                <div style="text-align: center">
                                    <asp:DataPager ID="DataPager1" PagedControlID="LvAccount" PageSize="10" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ShowNextPageButton="false" ButtonCssClass="fg-button ui-button ui-state-default" />
                                            <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="fg-button ui-button ui-state-default" CurrentPageLabelCssClass="fg-button ui-button ui-state-primary next" NextPreviousButtonCssClass="fg-button ui-button ui-state-default" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <table id="hoverTable" class="table hover responsive nowrap">
                                    <thead class="bg-gray">
                                        <tr>
                                            <th>是否啟用</th>
                                            <th>使用者名稱</th>
                                            <th>帳號</th>
                                            <th>描述</th>
                                            <th>操作</th>
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
                                        <asp:Label ID="LEnable" runat="server" ForeColor="Red" Text='<%# Eval("Enable") %>'></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LAccount" Text='<%# Eval("Account") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="LDecscription" Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:LinkButton ID='LBtn_Edit' CommandArgument='<%# Eval("ID") %>' OnCommand="LBtn_Edit_Command" CssClass="btn btn-primary  mg-b-10" runat="server" ToolTip="編輯"><i class="fa fa-edit"></i>&nbsp;編輯</asp:LinkButton>
                                        <asp:LinkButton ID='LBtn_Delete' CommandArgument='<%# Eval("ID") %>' OnCommand="LBtn_Delete_Command" CssClass="btn btn-danger  mg-b-10" OnClientClick="return confirm('您確定要刪除此項目嗎?');" runat="server" ToolTip="刪除"><i class="fa fa-trash-o"></i>&nbsp;刪除</asp:LinkButton>
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


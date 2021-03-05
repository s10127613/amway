<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Admin_Customer" %>

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
                <h4 class="card-header-title">顧客列表
                </h4>
                <div class="card-header-btn">
                    <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse5" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                </div>
            </div>
            <div class="card-body collapse show" id="collapse5">
                <div class="row">
                    <div class="col-md-3">
                        <p>加入類型</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="DDLType" runat="server" CssClass="form-control select2 select2-hidden-accessible">
                                <asp:ListItem Selected="True" Value="-1">請選擇…</asp:ListItem>
                                <asp:ListItem Value="-2">尚未加入</asp:ListItem>
                                <asp:ListItem Value="1">直銷商</asp:ListItem>
                                <asp:ListItem Value="2">生活會員</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>編號</p>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="TBNo" runat="server" CssClass="form-control" placeholder="請輸入編號"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>來源</p>
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="DDLSource" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="-1" Selected="True">請選擇…</asp:ListItem>
                                <asp:ListItem Value="0">陌生開發</asp:ListItem>
                                <asp:ListItem Value="1">親友好友介紹</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>姓名</p>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="TBName" runat="server" CssClass="form-control" placeholder="請輸入姓名"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mg-b-30">
                        <asp:LinkButton ID="Btn_Add" runat="server" CssClass="btn btn-success" ToolTip="新增資料" PostBackUrl="~/Admin/Customer_Edit.aspx"><i class="fa fa-plus"></i></asp:LinkButton>
                        <asp:LinkButton ID="Btn_Search" runat="server" CssClass="btn btn-primary" ToolTip="搜尋" OnClick="Btn_Search_Click"><i class="fa fa-search"></i></asp:LinkButton>
                        <asp:LinkButton ID="Btn_Clear" runat="server" CssClass="btn btn-warning" ToolTip="清除搜尋" OnClick="Btn_Clear_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                    </div>
                    <div class="col-md-12 mg-b-30">
                        <p>顧客列表</p>
                        <asp:ListView ID="LVCustomer" runat="server" OnItemDataBound="LVCustomer_ItemDataBound" OnPagePropertiesChanging="LVCustomer_PagePropertiesChanging">
                            <LayoutTemplate>
                                <table id="hoverTable" class="table hover responsive nowrap">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">排序</th>
                                            <th style="text-align: center">加入類型</th>
                                            <th style="text-align: center">編號</th>
                                            <th style="text-align: center">來源</th>
                                            <th style="text-align: center">姓名</th>
                                            <th style="text-align: center">加入日期</th>
                                            <th style="text-align: center">功能</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </tbody>
                                </table>
                                <div style="text-align: center">
                                    <asp:DataPager ID="DataPager1" PagedControlID="LVCustomer" PageSize="10" runat="server">
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
                                            <th style="text-align: center">排序</th>
                                            <th style="text-align: center">加入類型</th>
                                            <th style="text-align: center">編號</th>
                                            <th style="text-align: center">來源</th>
                                            <th style="text-align: center">姓名</th>
                                            <th style="text-align: center">加入日期</th>
                                            <th style="text-align: center">功能</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-center" colspan="6">無資料</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <td style="text-align: center">
                                    <asp:Label ID="LNum" Text='<%# Eval("Num") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="LType" Text='<%# Eval("Type") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="LNo" Text='<%# Eval("No") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="LSource" Text='<%# Eval("Source") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="LName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="LSDate" Text='<%# Eval("SDate") %>' runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:LinkButton ID='LBEdit' CommandArgument='<%# Eval("ID") %>' OnCommand="LBEdit_Command" CssClass="btn btn-primary  mg-b-10" runat="server" ToolTip="編輯"><i class="fa fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID='LBDel' CommandArgument='<%# Eval("ID") %>' OnCommand="LBDel_Command" CssClass="btn btn-danger  mg-b-10" OnClientClick="return confirm('您確定要刪除此項目嗎?');" runat="server" ToolTip="刪除"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </td>
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


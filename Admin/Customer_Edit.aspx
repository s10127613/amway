<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Customer_Edit.aspx.cs" Inherits="Admin_Customer_Edit" %>

<%@ Register Src="~/UserControl/Address.ascx" TagName="Address" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <!--================================-->
    <!-- Top Label Layout Start -->
    <!--================================-->
    <div class="col-md-12 col-lg-12">
        <div class="card mg-b-20">
            <div class="card-header">
                <h4 class="card-header-title">基本資訊<asp:Label ID="LMode" runat="server" ForeColor="Red" Font-Size="Smaller"></asp:Label></h4>
                <div class="card-header-btn">
                    <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse1" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                </div>
            </div>
            <div class="card-body collapse show" id="collapse1">
                <div class="form-layout form-layout-1">
                    <div class="row mg-b-25">
                        <!-- col-3 -->
                        <div class="col-lg-3">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">顧客來源<span class="tx-danger">*</span></label>
                                <asp:DropDownList ID="DDLSource" runat="server" CssClass="form-control select2 select2-hidden-accessible">
                                    <asp:ListItem Selected="True" Value="-1">請選擇…</asp:ListItem>
                                    <asp:ListItem Value="1">陌生開發</asp:ListItem>
                                    <asp:ListItem Value="2">親友好友介紹</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" InitialValue="-1" ControlToValidate="DDLSource" ValidationGroup="Group1" ErrorMessage="尚未選擇來源" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <!-- col-3 -->
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="form-control-label">類型<span class="tx-danger">*</span></label>
                                <asp:DropDownList ID="DDLType" runat="server" CssClass="form-control select2 select2-hidden-accessible">
                                    <asp:ListItem Selected="True" Value="-1">請選擇…</asp:ListItem>
                                    <asp:ListItem Value="-2">尚未加入</asp:ListItem>
                                    <asp:ListItem Value="1">直銷商</asp:ListItem>
                                    <asp:ListItem Value="2">生活會員</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" InitialValue="-1" ControlToValidate="DDLType" ValidationGroup="Group1" ErrorMessage="尚未選擇類型" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <!-- col-3 -->
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="form-control-label">編號</label>
                                <asp:TextBox ID="TBNo" runat="server" CssClass="form-control" placeholder="請輸入直銷商編號"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-3 -->
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="form-control-label">加入日期</label>
                                <asp:TextBox ID="TBDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-6 -->
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label class="form-control-label active">姓名<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TBName" runat="server" CssClass="form-control" placeholder="請輸入姓名"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="text-danger" InitialValue="" ControlToValidate="TBName" ValidationGroup="Group1" ErrorMessage="尚未輸入姓名" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <!-- col-6 -->
                        <div class="col-lg-6">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">性別<span class="tx-danger">*</span></label>
                                <asp:DropDownList ID="DDLGender" runat="server" CssClass="form-control select2 select2-hidden-accessible">
                                    <asp:ListItem Selected="True" Value="-1">請選擇…</asp:ListItem>
                                    <asp:ListItem Value="0">男性</asp:ListItem>
                                    <asp:ListItem Value="1">女性</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" InitialValue="-1" ControlToValidate="DDLGender" ValidationGroup="Group1" ErrorMessage="尚未選擇性別" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <!-- col-6 -->
                        <div class="col-lg-12">
                            <uc1:Address ID="app_Address" runat="server" />
                        </div>
                        <!-- col-12 -->
                        <div class="col-lg-6">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label active">連絡電話(1):</label>
                                <asp:TextBox ID="TBTel" runat="server" CssClass="form-control" placeholder="請輸入連絡電話"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-6 -->
                        <div class="col-lg-6">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label active">連絡電話(2):</label>
                                <asp:TextBox ID="TBTel2" runat="server" CssClass="form-control" placeholder="請輸入連絡電話"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-6 -->
                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label active">Email:</label>
                                <asp:TextBox ID="TBEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="請輸入電子郵件地址"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-6 -->
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label class="form-control-label active">備註</label>
                                <asp:TextBox ID="TBRemark" TextMode="MultiLine" Height="150px" runat="server" CssClass="form-control" placeholder="請輸入備註內容"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-12 -->
                    </div>
                    <!-- row -->
                    <div class="form-layout-footer">
                        <asp:LinkButton ID="BtnSubmit" runat="server" CssClass="btn btn-custom-primary" ValidationGroup="Group1" OnClick="BtnSubmit_Click"><i class="fa fa-save"></i>&nbsp;儲存</asp:LinkButton>
                        <asp:LinkButton ID="BtnCancel" runat="server" CssClass="btn btn-info" PostBackUrl="~/Admin/Customer.aspx"><i class="fa fa-mail-reply"></i>&nbsp;回上一頁</asp:LinkButton>
                    </div>
                    <!-- form-layout-footer -->
                </div>
            </div>
        </div>
    </div>
    <!--/ Top Label Layout End -->
    <div class="col-md-12 col-lg-12">
        <div class="card mg-b-20">
            <div class="card-header">
                <h4 class="card-header-title">發票資訊</h4>
                <div class="card-header-btn">
                    <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse1" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                </div>
            </div>
            <div class="card-body collapse show" id="collapse2">
                <div class="col-md-3 mg-b-30">
                    <asp:LinkButton ID="Btn_Add" runat="server" CssClass="btn btn-success" PostBackUrl="~/Admin/Invoice.aspx"><i class="fa fa-plus"></i></asp:LinkButton>
                </div>
                <div class="col-md-12 mg-b-30">
                    <asp:ListView ID="LVInvoice" runat="server" OnItemDataBound="LVInvoice_ItemDataBound">
                        <LayoutTemplate>
                            <table id="hoverTable" class="table hover responsive nowrap">
                                <thead>
                                    <tr>
                                        <th style="text-align: center">發票編號</th>
                                        <th style="text-align: center">發票日期</th>
                                        <th style="text-align: center">描述</th>
                                        <th style="text-align: center">總計</th>
                                        <th style="text-align: center">功能</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </tbody>
                            </table>
                            <div style="text-align: center">
                                <asp:DataPager ID="DataPager1" PagedControlID="LVInvoice" PageSize="10" runat="server">
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
                                        <th style="text-align: center">發票編號</th>
                                        <th style="text-align: center">發票日期</th>
                                        <th style="text-align: center">描述</th>
                                        <th style="text-align: center">總計</th>
                                        <th style="text-align: center">功能</th>
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
                            <td style="text-align: center">
                                <asp:Label ID="LNo" Text='<%# Eval("No") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="LIDate" Text='<%# Eval("IDate") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="LDescription" Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="LGrandTotal"  Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:LinkButton ID='LBEdit' CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" OnCommand="LBEdit_Command" runat="server" ToolTip="編輯"><i class="fa fa-edit"></i></asp:LinkButton>
                                <asp:LinkButton ID='LBDel' CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" OnCommand="LBDel_Command" OnClientClick="return confirm('您確定要刪除此項目嗎?');" runat="server" ToolTip="刪除"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>


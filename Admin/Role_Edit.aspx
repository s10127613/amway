<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Role_Edit.aspx.cs" Inherits="Admin_Role_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <!--================================-->
    <!-- Top Label Layout Start -->
    <!--================================-->
    <div class="col-md-12 col-lg-12">
        <div class="card mg-b-20">
            <div class="card-header">
                <h4 class="card-header-title">
                    <asp:Literal ID="mL_Title" runat="server"></asp:Literal><asp:Label ID="LMode" runat="server" ForeColor="Red" Font-Size="Smaller"></asp:Label></h4>
                <div class="card-header-btn">
                    <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse1" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                </div>
            </div>
            <div class="card-body collapse show" id="collapse1">
                <div class="form-layout form-layout-1">
                    <div class="row mg-b-25">

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">角色名稱<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" InitialValue="" ControlToValidate="TB_Name" ValidationGroup="Group1" ErrorMessage="尚未輸入名稱" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">
                                <label class="form-control-label">角色說明<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Description" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="custom-control custom-checkbox">
                                    <input id="CB_Enable" runat="server" type="checkbox" value="checked" class="custom-control-input input-mini" checked="">
                                    <label class="custom-control-label" for="<%=CB_Enable.ClientID %>">啟用此權限</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="card mg-b-20">
                                <div class="card-header">
                                    <h4 class="card-header-title">權限樹狀結構
                                    </h4>
                                    <div class="card-header-btn">
                                        <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse2" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                                    </div>
                                </div>
                                <div class="card-body collapse show" id="collapse2">
                                    <div id="jstree-checkbox">
                                        <asp:Literal ID="LPermission" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                    <div class="form-layout-footer">
                        <asp:LinkButton ID="LBtn_Send" runat="server" CssClass="btn btn-custom-primary" OnClick="Btn_Send_Click">儲存</asp:LinkButton>
                        <asp:LinkButton ID="LBtn_Cancel" runat="server" CssClass="btn btn-secondary" PostBackUrl="~/Admin/Role.aspx">取消</asp:LinkButton>
                    </div>
                    <!-- form-layout-footer -->
                </div>
            </div>
        </div>
    </div>
    <!--/ Top Label Layout End -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>


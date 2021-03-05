<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="SystemUser_Edit.aspx.cs" Inherits="Admin_SystemUser_Edit" %>

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
                            <div class="form-group">
                                <div class="custom-control custom-checkbox">
                                    <input id="CB_Enable" runat="server" type="checkbox" value="checked" class="custom-control-input input-mini" checked="">
                                    <label class="custom-control-label" for="<%=CB_Enable.ClientID %>">啟用此帳號</label>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">使用者名稱<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Name" runat="server" MaxLength="20" CssClass="form-control" placeholder="請輸入使用者名稱"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="mRFV_01" Display="Dynamic" runat="server" CssClass="text-danger" ValidationGroup="mVG_MemberData" ControlToValidate="TB_Name" ErrorMessage="使用者名稱尚未填寫"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">帳號<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Account" runat="server" MaxLength="20" CssClass="form-control" placeholder="請輸入帳號"></asp:TextBox>
                                <asp:HiddenField ID="HF_Account" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" CssClass="text-danger" ValidationGroup="mVG_MemberData" ControlToValidate="TB_Name" ErrorMessage="使用者名稱尚未填寫"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="mRFV_02" CssClass="text-danger" runat="server" ValidationGroup="mVG_MemberData" ControlToValidate="TB_Account" Display="Dynamic" ErrorMessage="尚未輸入帳號"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="mREV_01" CssClass="text-danger" ControlToValidate="TB_Account" ValidationGroup="mVG_MemberData" runat="server" ErrorMessage="格式錯誤，帳號請勿輸入中文" Display="Dynamic" ValidationExpression="^\w+$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">密碼<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Password" TextMode="Password" runat="server" CssClass="form-control" placeholder="長度6~12個字元英數字(需至少含一英文字元)"></asp:TextBox>
                                <asp:Label ID="LBL_Password" runat="server" Text="密碼如果不需要修改請勿填寫" Visible="false" CssClass="PasswordStyle"></asp:Label>
                                <asp:RequiredFieldValidator ID="Rfv_03" CssClass="text-danger" ControlToValidate="TB_Password" ValidationGroup="Form" ErrorMessage="尚未輸入密碼" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">電子郵件<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Email" runat="server" TextMode="Email" CssClass="form-control" placeholder="請輸入電子郵件" ValidationGroup="mVG_MemberData"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="mRFV_04" runat="server" CssClass="text-danger" ValidationGroup="mVG_MemberData" ControlToValidate="TB_Email" ErrorMessage="電子郵件尚未填寫" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">連絡電話<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Phone" runat="server" TextMode="Number" CssClass="form-control" placeholder="請輸入連絡電話"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="mRFV_05" runat="server" CssClass="text-danger" ValidationGroup="mVG_MemberData" ControlToValidate="TB_Phone" ErrorMessage="連絡電話尚未填寫" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">權限群組<span class="tx-danger">*</span></label>
                                <asp:DropDownList ID="DDL_Role" runat="server" CssClass="form-control form-control-primary">
                                    <asp:ListItem Text="請選擇" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">身分別<span class="tx-danger">*</span></label>
                                <asp:DropDownList ID="DDL_Position" runat="server" CssClass="form-control form-control-primary">
                                    <asp:ListItem Text="請選擇身分" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label">帳號說明<span class="tx-danger">*</span></label>
                                <asp:TextBox ID="TB_Description" runat="server" CssClass="form-control" TextMode="MultiLine" Height="250px" placeholder="請輸入該使用者說明"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                    <div class="form-layout-footer">
                        <asp:LinkButton ID="LBtn_Basic_Save" runat="server" CssClass="btn btn-custom-primary" OnClick="LBtn_Save_Click">儲存</asp:LinkButton>
                        <asp:LinkButton ID="LBtn_Basic_Back" runat="server" CssClass="btn btn-secondary" PostBackUrl="~/Admin/SystemUser.aspx">取消</asp:LinkButton>
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


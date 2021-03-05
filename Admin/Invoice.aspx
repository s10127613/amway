<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <!--================================-->
    <!-- Top Label Layout Start -->
    <!--================================-->
    <div class="col-md-12 col-lg-12">
        <div class="card mg-b-20">
            <div class="card-header">
                <h4 class="card-header-title">發票資訊<asp:Label ID="LMode" runat="server" ForeColor="Red" Font-Size="Smaller"></asp:Label></h4>
                <div class="card-header-btn">
                    <a href="#" data-toggle="collapse" class="btn card-collapse" data-target="#collapse1" aria-expanded="true"><i class="ion-ios-arrow-down"></i></a>
                </div>
            </div>
            <div class="card-body collapse show" id="collapse1">
                <div class="form-layout form-layout-1">
                    <div class="row mg-b-25">
                        <!-- col-12 -->
                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label active">發票編號:</label>
                                <asp:TextBox ID="TBNo" Enabled="false" runat="server" CssClass="form-control" placeholder="發票編號由系統產生"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-12 -->
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label class="form-control-label active">發票日期</label>
                                <asp:TextBox ID="TBIDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-12 -->
                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label active">發票描述:</label>
                                <asp:TextBox ID="TBDescription" runat="server" CssClass="form-control" placeholder="請輸入發票描述"></asp:TextBox>
                            </div>
                        </div>
                        <!-- col-12 -->
                        <div class="col-lg-12">
                            <div class="form-group mg-b-10-force">
                                <label class="form-control-label active">產品項目:</label>
                                <asp:ListView ID="LVItem" runat="server" OnItemEditing="LVItem_ItemEditing" OnItemDataBound="LVItem_ItemDataBound" OnItemCanceling="LVItem_ItemCanceling" OnItemDeleting="LVItem_ItemDeleting" OnItemUpdating="LVItem_ItemUpdating" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                    <LayoutTemplate>
                                        <table id="hoverTable" class="table hover responsive nowrap">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center">產品名稱</th>
                                                    <th style="text-align: center">數量</th>
                                                    <th style="text-align: center">單價</th>
                                                    <th style="text-align: center">功能</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="TBName" CssClass="form-control" runat="server" placeholder="請輸入產品名稱"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" ControlToValidate="TBName" ValidationGroup="Form" ErrorMessage="尚未產品名稱" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="TBQuantity" TextMode="Number" CssClass="form-control" runat="server" placeholder="請輸入數量"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" ControlToValidate="TBQuantity" ValidationGroup="Form" ErrorMessage="尚未產品名稱" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="TBUnitCost" TextMode="Number" CssClass="form-control" runat="server" placeholder="請輸入單價"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" ControlToValidate="TBUnitCost" ValidationGroup="Form" ErrorMessage="尚未產品名稱" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:LinkButton ID='LBtnInsert' CssClass="btn btn-success" OnClick="LBtnInsert_Click" ValidationGroup="Form" runat="server">新增</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tfoot>
                                                <td style="text-align: end" colspan="3">共計
                                                </td>
                                                <td style="text-align: center" colspan="1">
                                                    <asp:Label ID="LGrandTotal" Text="0" runat="server"></asp:Label>
                                                </td>
                                            </tfoot>
                                        </table>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <tr>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                        </tr>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <td style="text-align: center">
                                            <asp:Label ID="LName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="LQuantity" Text='<%# Eval("Quantity") %>' runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="LUnitCost" Text='<%# Eval("UnitCost") %>' runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:LinkButton ID='LBtnEdit' CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" runat="server">編輯</asp:LinkButton>
                                            <asp:LinkButton ID='LBtnDel' CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" runat="server">刪除</asp:LinkButton>
                                        </td>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <td style="text-align: center">
                                            <asp:TextBox ID="TBName" CssClass="form-control is-valid" Text='<%# Eval("Name") %>' runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:TextBox ID="TBQuantity" CssClass="form-control is-valid" TextMode="Number" Text='<%# Eval("Quantity") %>' runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:TextBox ID="TBUnitCost" CssClass="form-control is-valid" TextMode="Number" Text='<%# Eval("UnitCost") %>' runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:LinkButton ID='LBtnUpdate' CommandName="Update" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-success" runat="server">儲存</asp:LinkButton>
                                            <asp:LinkButton ID='LBtnCancel' CommandName="Cancel" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-info" runat="server">取消</asp:LinkButton>
                                        </td>
                                    </EditItemTemplate>
                                </asp:ListView>
                            </div>
                            <!-- col-12 -->
                        </div>
                    </div>
                    <!-- row -->
                    <div class="form-layout-footer">
                        <asp:LinkButton ID="BtnSubmit" runat="server" CssClass="btn btn-custom-primary" ValidationGroup="Group1" OnClick="BtnSubmit_Click"><i class="fa fa-save"></i>&nbsp;儲存</asp:LinkButton>
                        <asp:LinkButton ID="BtnCancel" runat="server" CssClass="btn btn-info" ValidationGroup="Group1" OnClick="BtnCancel_Click"><i class="fa fa-mail-reply"></i>&nbsp;回上一頁</asp:LinkButton>
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


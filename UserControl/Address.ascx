<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Address.ascx.cs" Inherits="UserControl_Address" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="form-row">
            <div class="col-md-2 mb-3">
                <label for="validationCustom03">郵遞區號</label>
                <asp:TextBox ID="AreaNum" runat="server" Columns="3" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 mb-3">
                <label for="validationCustom04">縣 / 市</label>
                <asp:DropDownList ID="DDL_Add1" runat="server" DataTextField="Name" DataValueField="ID" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDL_Add1_SelectedIndexChanged">
                    <asp:ListItem Value="-2">請選擇…</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-3">
                <label for="validationCustom05">鄉 / 鎮</label>
                <asp:DropDownList ID="DDL_Add2" runat="server" DataTextField="Name" DataValueField="ID" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDL_Add2_SelectedIndexChanged">
                    <asp:ListItem Value="-2">請選擇…</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2 mb-3">
                <label for="validationCustom05">村 / 里</label>
                <asp:DropDownList ID="DDL_Add3" runat="server" DataTextField="Name" DataValueField="ID" AppendDataBoundItems="True" CssClass="form-control">
                    <asp:ListItem Value="-2">請選擇…</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3">
                <label for="validationCustom05">地址</label>
                <asp:TextBox ID="TB_Add4" runat="server" CssClass="form-control" placeholder="請輸入地址" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        資料載入中，請稍後.....
    </ProgressTemplate>
</asp:UpdateProgress>

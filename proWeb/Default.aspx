<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Products management</h1>
        <p>
            Code &nbsp;<asp:TextBox ID="text_code" runat="server" Height="20px" style="margin-top: 5px; margin-left: 25px;" Width="200px"></asp:TextBox>
        </p>
        <p>
            Name &nbsp;<asp:TextBox ID="text_name" runat="server" Height="20px" style="margin-top: 5px; margin-left: 25px;" Width="200px"></asp:TextBox>
        </p>
        <p>
            Amount &nbsp;<asp:TextBox ID="text_amount" runat="server" Height="20px" style="margin-top: 5px; margin-left: 25px;" Width="200px"></asp:TextBox>
        </p>
        <p>
            Category &nbsp;
            <asp:DropDownList ID="categorys" runat="server" OnSelectedIndexChanged="category">
                <asp:ListItem Text="Computing" Value=0></asp:ListItem>
                <asp:ListItem Text="Telephony" Value=1></asp:ListItem>
                <asp:ListItem Text="Gaming" Value=2></asp:ListItem>
                <asp:ListItem Text="Home appliances" Value=3></asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            Price &nbsp;<asp:TextBox ID="text_price" runat="server" Height="20px" style="margin-top: 5px; margin-left: 25px;" Width="200px"></asp:TextBox>
        </p>
        <p>
            Creation Date &nbsp;<asp:TextBox ID="text_date" runat="server" Height="20px" style="margin-top: 5px; margin-left: 25px;" Width="200px"></asp:TextBox>
        </p>
        
    </div>
  
    <asp:Label ID="outputMsg" runat="server"></asp:Label><br />

    
    <asp:Button text="Create" onClick="_Create" ID="buttom_Leer" runat="server"  style="margin: 5px" />
    <asp:Button text="Update" onClick="_Update" ID="buttom_Primero" runat="server" style="margin: 5px" />
    <asp:Button text="Delete" onClick="_Delete" ID="buttom_Anterior" runat="server" style="margin: 5px" />
    <asp:Button text="Read" onClick="_Read" ID="buttom_Siguiente" runat="server" style="margin: 5px" />
    <asp:Button text="Read First" onClick="_Read_F" ID="buttom_Crear" runat="server" style="margin: 5px" />
    <asp:Button text="Read Prev" onClick="_Read_P" ID="buttom_Actualizar" runat="server" style="margin: 5px" />
    <asp:Button text="Read Next" onClick="_Read_N" ID="buttom_Borrar" runat="server" style="margin: 5px" />
    
</asp:Content>

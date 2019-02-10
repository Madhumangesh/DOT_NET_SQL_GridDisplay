<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export_To_DB_Display.aspx.cs" Inherits="Test_For_GIT.Export_To_DB_Display" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>This is a  title</title>
</head> 
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server"/><br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Add to Table" OnClick="btnSubmit_Click" /><br />

               <asp:Label ID="lblMessage" runat="server"></asp:Label>  
			<br />
            <asp:Button ID="btnShowgv" runat="server" Text="Show Table Content" OnClick="btnShowgv_Click"  /><br />

			<asp:GridView ID="gvBF" runat="server" AllowPaging="true" PageSize="10" 
                OnPageIndexChanging="gvBF_PageIndexChanging" AutoGenerateColumns="true" >  
                 
            </asp:GridView>  
    
        </div>
    </form>
</body>
</html>

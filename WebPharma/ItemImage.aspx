<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemImage.aspx.cs" Inherits="ItemImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>In Style :: Item Image</title>
     <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ImageButton ID="imgItemImage" Width="300px" Height="300px" runat="server" />
    </div>
    </form>
</body>
</html>

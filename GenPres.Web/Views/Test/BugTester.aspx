﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Test.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Client/GenPres/style/css/VisibleUITests.css") %>" /> 

    <script>
        window.dontLoadApplication = true;
    </script>

    <script type="text/javascript" src="<%= Url.Content("~/Client/GenPres/test/Bugs/SelectBoxStoreWith_extraParams.js")  %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Client/Tests/RunJasmine.js")  %>"></script>
    
</asp:Content>
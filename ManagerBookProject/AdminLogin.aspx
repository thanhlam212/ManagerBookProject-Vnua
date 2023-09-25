<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="ManagerBookProject.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
      <div class="row">
         <div class="col-md-6 mx-auto">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="150px" src="Image/adminuser.png"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h3>Admin Login</h3>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        
                        <div class="form-group mb-3">
                           <asp:TextBox CssClass="form-control" ID="tbAdminId" runat="server" placeholder="Admin ID"></asp:TextBox>
                        </div>
                        
                        <div class="form-group mb-3">
                           <asp:TextBox CssClass="form-control" ID="tbAdminPass" runat="server" placeholder="*********" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group ">
                           <asp:Button class="btn btn-success btn-block" ID="btnAdminLogin" runat="server" Text="Login" Width="600px" OnClick="btnAdminLogin_Click" />
                        </div>
                     </div>
                  </div>
               </div>
            </div>
            <a href="HomePage.aspx"><< Back to Home</a><br><br>
         </div>
      </div>
   </div>
</asp:Content>

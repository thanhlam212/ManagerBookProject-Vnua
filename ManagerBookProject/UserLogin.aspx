<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="ManagerBookProject.UserLogin" %>
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
                           <img width="150px" src="Image/generaluser.png"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h3>Member Login</h3>
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
                           <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Member ID" ></asp:TextBox>
                        </div>
                        
                        <div class="form-group mb-3">
                           <asp:TextBox class="form-control" ID="TextBox2" runat="server" placeholder="*********" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group  mb-3">
                           <asp:Button class="btn btn-success btn-block " ID="btnLogin" runat="server" Text="Login" Width="603px" />
                        </div>
                        <div class="form-group">
                         <p>You are not member ? <a href="UserSignUp.aspx" id="btnSignUp">Sign Up</a></p> 
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

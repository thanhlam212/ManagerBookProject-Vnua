<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UserSignUp.aspx.cs" Inherits="ManagerBookProject.UserSignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
      <div class="row">
         <div class="col-md-7 mx-auto">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="100px" src="../Image/generaluser.png"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Member Sign Up</h4>                       
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label>Full Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="tbFName" runat="server" placeholder="Full Name"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Date of Birth</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="tbDOB" runat="server" placeholder="Password" TextMode="Date"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label>Contact No</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="tbContactNo" runat="server" placeholder="Contact No" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Email ID</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="tbEmail" runat="server" placeholder="Email ID" TextMode="Email"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        <label>State</label>
                        <div class="form-group">
                           <asp:DropDownList class="form-control" ID="ddListState" runat="server">
                              <asp:ListItem Text="Moscow" Value="Moscow" />
                              <asp:ListItem Text="Andhra Pradesh" Value="Andhra Pradesh" />
                              <asp:ListItem Text="Berlin" Value="Berlin" />
                              <asp:ListItem Text="Ba Đình" Value="Ba Đình" />
                              <asp:ListItem Text="Hoàn Kiếm" Value="Hoàn Kiếm" />
                              <asp:ListItem Text="Tây Hồ" Value="Tây Hồ" />
                              <asp:ListItem Text="Long Biên" Value="Long Biên" />
                              <asp:ListItem Text="Cầu Giấy" Value="Cầu Giấy" />
                              <asp:ListItem Text="Đống Đa" Value="Đống Đa" />
                              <asp:ListItem Text="Hai Bà Trưng" Value="Hai Bà Trưng" />
                              <asp:ListItem Text="Hoàng Mai" Value="Hoàng Mai" />
                              <asp:ListItem Text="Hà Đông" Value="Hà Đông" />
                              <asp:ListItem Text="Bắc Từ Liêm" Value="Bắc Từ Liêm" />
                              <asp:ListItem Text="Nam Từ Liêm" Value="Nam Từ Liêm" />
                           </asp:DropDownList>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>City</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="tbCity" runat="server" placeholder="City"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>Pincode</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="tbPincode" runat="server" placeholder="Pincode" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <label>Full Address</label>
                        <div class="form-group mb-3">
                           <asp:TextBox CssClass="form-control" ID="tbFAdress" runat="server" placeholder="Full Address" TextMode="MultiLine" Rows="2" Height="192px"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col mb-3">
                        <center>
                           <span class="badge text-bg-success">Login Credentials</span>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <label>User ID</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="tbUserID" runat="server" placeholder="User ID"></asp:TextBox>
                        </div>
                     </div>
                     
                     <div class="col">
                        <label>Password</label>
                        <div class="form-group mb-3">
                           <asp:TextBox class="form-control" ID="tbPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-8">
                        <center>
                           <div class="form-group ">
                              <asp:Button class="btn btn-success btn-block btn-sm" ID="btnSignUp" runat="server" Text="Sign Up" Width="714px" OnClick="Button1_Click" />
                           </div>
                        </center>
                     </div>
                  </div>
               </div>
            </div>
            <a href="../HomePage.aspx"><< Back to Home</a><br><br>
         </div>
      </div>
   </div>
</asp:Content>


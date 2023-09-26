<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdminPublisherManagement.aspx.cs" Inherits="ManagerBookProject.AdminPublisherManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
      $(document).ready(function () {
          $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
      });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
      <div class="row">
         <div class="col-md-5">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Publisher Details</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="100px" src="Image/publisher.png" />
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        <label>Publisher ID</label>
                        <div class="form-group mb-3">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="tbPublisherID" runat="server" placeholder="ID"></asp:TextBox>
                              <asp:Button class="btn btn-primary" ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
                           </div>
                        </div>
                     </div>
                     <div class="col-md-8">
                        <label>Publisher Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="tbPublisherName" runat="server" placeholder="Publisher Name"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-4">
                        <asp:Button ID="btnAddPublisher" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="btnAddPublisher_Click" />
                     </div>
                     <div class="col-4">
                        <asp:Button ID="btnUpdatePublisher" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="btnUpdatePublisher_Click" />
                     </div>
                     <div class="col-4">
                        <asp:Button ID="btnDeletePublisher" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="btnDeletePublisher_Click" />
                     </div>
                  </div>
               </div>
            </div>
            <a href="HomePage.aspx"><< Back to Home</a><br>
            <br>
         </div>
         <div class="col-md-7">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Publisher List</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                      <asp:SqlDataSource ID="PublisherDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ebookDBConnectionString %>" SelectCommand="SELECT * FROM [publisher_master_tbl]"></asp:SqlDataSource>
                     <div class="col">
                        <asp:GridView class="table table-striped table-bordered" ID="PublisherDataTable" runat="server" AutoGenerateColumns="False" DataKeyNames="publisher_id" DataSourceID="PublisherDataSource">
                            <Columns>
                                <asp:BoundField DataField="publisher_id" HeaderText="publisher_id" ReadOnly="True" SortExpression="publisher_id" />
                                <asp:BoundField DataField="publisher_name" HeaderText="publisher_name" SortExpression="publisher_name" />
                            </Columns>
                         </asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>

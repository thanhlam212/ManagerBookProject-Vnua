<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdminAuthorManagement.aspx.cs" Inherits="ManagerBookProject.AdminAuthor" %>
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
                                        <h4>Author Details</h4>
                                    </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                        <img width="100px" src="Image/writer.png" />
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
                                <label>Author ID</label>
                                <div class="form-group mb-3">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="tbAuthorId" runat="server" placeholder="ID"></asp:TextBox>
                                        <asp:Button class="btn btn-primary" ID="GoBtn" runat="server" Text="Go" OnClick="GoBtn_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-8">
                                <label>Author Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="tbAuthorName" runat="server" placeholder="Author Name"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="AddAuthorBtn" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="AddAuthorBtn_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="UpdateAuthorBtn" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="UpdateAuthorBtn_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="DeleteAuthorBtn" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="DeleteAuthorBtn_Click" />
                            </div>
                        </div>

                    </div>
                </div>

                <a href="homepage.aspx"><< Back to Home</a><br>
                <br>
            </div>

            <div class="col-md-7">

                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                        <h4>Author List</h4>
                                    </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="AuthorDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ebookDBConnectionString %>" ProviderName="<%$ ConnectionStrings:ebookDBConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [author_master_tbl]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered" ID="AuthorDataTable" runat="server" AutoGenerateColumns="False" DataKeyNames="author_id" DataSourceID="AuthorDataSource">
                                    <Columns>
                                        <asp:BoundField DataField="author_id" HeaderText="author_id" ReadOnly="True" SortExpression="author_id" />
                                        <asp:BoundField DataField="author_name" HeaderText="author_name" SortExpression="author_name" />
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

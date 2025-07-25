<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="SubdeptAdd.aspx.cs" Inherits="SubdeptAdd" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Sub Department List</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Sub Department List</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                                      <div class="row mb-3">
                               <div class="col-lg-12 text-center">
                                   
                                     <asp:Button ID="Btn_Add_Dept" runat="server" CssClass="btn btn-light mb-2" 
                                        Text="Add Department" onclick="Btn_Add_Dept_Click"  />
                                        <asp:Button ID="Btn_Add_Test" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Test" onclick="Btn_Add_Test_Click"  />
                                         <asp:Button ID="btnedittest" runat="server" CssClass="btn btn-secondary mb-2" 
                                        Text="Edit Test" onclick="btnedittest_Click"  />
                                         <asp:Button ID="Btn_Add_NR" runat="server" CssClass="btn btn-primary mb-2" 
                                        Text="Add Referance Range" onclick="Btn_Add_NR_Click"  />
                                         <asp:Button ID="Btn_Add_PK" runat="server" CssClass="btn btn-light mb-2" 
                                        Text="Add Package" onclick="Btn_Add_PK_Click"   />
                                          <asp:Button ID="Btn_Add_Sample" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Sample Type" onclick="Btn_Add_Sample_Click"   />
                                        <asp:Button ID="Btn_Add_ShortCut" runat="server" CssClass="btn btn-secondary mb-2" 
                                        Text="Add Short Cut" onclick="Btn_Add_ShortCut_Click"  />
                                         <asp:Button ID="Btn_Add_Formula" runat="server" CssClass="btn btn-primary mb-2" 
                                        Text="Add Formula" onclick="Btn_Add_Formula_Click"    />
                                         <asp:Button ID="Btn_Add_RN" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Report Note" onclick="Btn_Add_RN_Click"  />
                                </div>
                            </div>
                             <div class="row mb-3">
                                <div class="col-lg-12">
                                <asp:Label ID="Label1" runat="server" Text="."  ></asp:Label>
                                </br>
                                </div>
                                  </div>
                    <div class="box" runat="server" id="List" >
                        <div class="box-header with-border">

                            
                             
                            <div class="row " id="DAdd" runat="server" >
                                <div class="col-lg-12">
                                   
                                    <asp:Button ID="btnaddnew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" 
                        onclick="btnaddnew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body" id="DAdd1" runat="server" visible="false">
                           <div class="table-responsive" style="width:100%">
                
                
                    <asp:GridView ID="GVSubdept" runat="server" class="table table-responsive table-sm table-bordered" Width="100%" PageSize="20" 
                                    AutoGenerateColumns="False"  HeaderStyle-ForeColor="Black"
 AlternatingRowStyle-BackColor="White"
                        OnRowEditing="GVSubdept_RowEditing"  
                        OnPageIndexChanging="GVSubdept_PageIndexChanging" AllowPaging="True" 
                                    onrowdeleting="GVSubdept_RowDeleting" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                        <Columns>
                          <asp:BoundField DataField="SDOrderNo" HeaderText="Seq No" />
                            <asp:BoundField DataField="SDCode" HeaderText="Sub Dept Code" />
                            <asp:BoundField DataField="subdeptName" HeaderText="Sub Dept Name" />
                          
                            <asp:BoundField DataField="Remark" HeaderText="Description" />
                            <asp:CommandField ShowEditButton="True" HeaderText="Edit" EditImageUrl="~/Images0/edit.gif"
                                ButtonType="Image" />
                                 <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btndelete" CommandName="Delete" runat="server" ForeColor="Blue" Text="Delete" />                            
                            </ItemTemplate>                            
                            </asp:TemplateField>
                        </Columns>
                       
                    </asp:GridView>
                    </div>
                        </div>
                       
                    </div>
                 <!--   ======================== -->
                   <div class="box" runat="server" id="show">
                        <div class="box-header with-border">
                            <asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="true" ForeColor="Red" ></asp:Label>
                            <span class="red pull-right"></span> 
                        </div>
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                     
                                         <asp:DropDownList ID="ddlmaindept" runat="server" CssClass="form-select form-control">
                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txtsubdeptName" placeholder="Enter Sub dept" runat="server" class="form-control" CausesValidation="True"
                        TabIndex="1"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                       
                                          <input type="text" maxlength="2" id="txtSDCode" runat="server" placeholder="Entersub dept code"    onchange="return CodeLimit()"
                       class="form-control" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       
                                        <asp:TextBox ID="txtSDOrderNo" runat="server" placeholder="Enter sub dept seq no" Width="261px" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                                 <div class="row mb-3">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                 <asp:TextBox ID="txtDescription" placeholder="Enter Description" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    
                                     <asp:Button ID="btnsave" runat="server"  CssClass="btn btn-success"  OnClientClick="return Validate();" Text="Save"
                        OnClick="btnSave_Click"  />
                                    
                                     <asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger"  Text="Cancel" 
                        onclick="btncancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </section>
                <!-- /.content -->


</asp:Content>


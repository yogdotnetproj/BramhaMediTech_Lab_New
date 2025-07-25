
 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Newtestparameter.aspx.cs" Inherits="Newtestparameter" %> 

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Add / Edit Parameter</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Add / Edit Parameter</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-header with-border">
                            <div class="row pull-right">
                                <div class="col-lg-12 red">
                                    Fields marked with * are mandatory
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                        <h4> <asp:Label ID="lblTestname" runat="server"  Text="Test Name" Font-Bold="True"></asp:Label></h4>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                      
                                     
                                           <asp:TextBox ID="txtparaName" runat="server"  placeholder="Parameter Name"  class="form-control" AutoPostBack="True" OnTextChanged="txtparaName_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtparaName"
                        Display="Dynamic" ErrorMessage="This is required field." SetFocusOnError="True"
                        ValidationGroup="form" Width="156px" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                                        <asp:TextBox ID="txtparaCode" runat="server"  placeholder="Parameter Code" class="form-control"
                    MaxLength="4" ontextchanged="txtCode_TextChanged" AutoPostBack="true" 
                    ></asp:TextBox>
              
                 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtparaCode"
                        Display="Dynamic" ErrorMessage="This is required field." SetFocusOnError="True"
                        ValidationGroup="form" Width="277px" Font-Bold="True"></asp:RequiredFieldValidator>
                         <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtparaCode"
                    FilterMode="ValidChars" ValidChars="A,B,C,D,E,F,E,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9">
                </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                      

                                        <asp:TextBox ID="txtparaOrder" runat="server" class="form-control"  placeholder="Parameter Order" ReadOnly="True"></asp:TextBox>                                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtparaOrder"
                        Display="Dynamic" ErrorMessage="This is required field." SetFocusOnError="True"
                        ValidationGroup="form" Width="158px" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                                         <asp:TextBox ID="txtparaMeth" class="form-control" placeholder="Parameter Method" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                
                               
                               
                              

                                 <div class="col-lg-12">
                                    <div class="form-group">
                                      
                                        <asp:TextBox ID="txtshortform" runat="server" class="form-control"  placeholder="Short Form"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-lg-12">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtdefaultres" runat="server" class="form-control"  placeholder="Default result"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-5">
                                    <div class="form-group">
                                       
                                      
                                                 
                                    </div>
                                </div>
                                <div class="col-lg-7">
                                    <div class="form-group">
                                       
                                      
                                                 <h4><strong>Normal Range</strong></h4>
                                    </div>
                                </div>
                                 <div class="row">
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                      
                                                 <h4><strong>Male</strong></h4>
                                    </div>
                                </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtMalelowerrange" runat="server" class="form-control"  placeholder="Lower Range" AutoPostBack="True" OnTextChanged="txtMalelowerrange_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtMaleupperrange" runat="server" class="form-control"  placeholder="Upper Range" AutoPostBack="True" OnTextChanged="txtMalelowerrange_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtMaleunit" runat="server" class="form-control"  placeholder="Unit"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtMalenormalRange" runat="server" class="form-control"  placeholder="General Normal Range"></asp:TextBox>
                                    </div>
                                </div>
                                     </div>
                                 <div class="row">
                                  <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                      
                                                 <h4><strong>Female</strong></h4>
                                    </div>
                                </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtFemaleLowerrange" runat="server" class="form-control"  placeholder="Lower Range" AutoPostBack="true" OnTextChanged="txtFemaleLowerrange_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtFemaleupperrange" runat="server" class="form-control"  placeholder="Upper Range"  AutoPostBack="true" OnTextChanged="txtFemaleLowerrange_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtFemaleUnit" runat="server" class="form-control"  placeholder="Unit"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="txtfemaleNormalRange" runat="server" class="form-control"  placeholder="General Normal Range"></asp:TextBox>
                                    </div>
                                </div>
                                     </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                       
                                        <div class="radio">
                                            <div class="row">
                                                <div class="col-lg-4 col-xs-4">
                                                    <label>
                                                        <!--     <asp:RadioButton ID="RblDescriptive" runat="server" Text="Descriptive" GroupName="Value1" OnCheckedChanged="RblDescriptive_CheckedChanged" AutoPostBack="True" />-->
                                                    </label>
                                                </div>    
                                                <div class="col-lg-4 col-xs-4">
                                                    <label>
                                                           <!--  <asp:RadioButton ID="RblNonDescriptive" runat="server" Text="Non Descriptive" Width="124px" GroupName="Value1" OnCheckedChanged="RblNonDescriptive_CheckedChanged" AutoPostBack="True" />        -->

                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                 
                                    <asp:Button ID="btnsave" runat="server" Text="Save" class="btn btn-primary" OnClick="Button1_Click" ValidationGroup="form" />
                                    
                                       <asp:Button ID="Button2" runat="server" Text="Back" class="btn btn-primary" OnClick="Button2_Click"  />
                                         <asp:Label ID="Label8" runat="server" ></asp:Label>

                                    <asp:Button ID="btnAddRange" runat="server" Text="Add Range" class="btn btn-primary" OnClick="btnAddRange_Click"  />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
           
        <!-- ./wrapper -->
       </asp:Content>